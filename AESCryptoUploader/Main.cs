using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Events;
using Implementation;
using Interfaces;
using log4net;
using Languages.Implementation;
using Languages.Interfaces;
using Models;
using UiThreadInvoke;

public partial class Main : Form
{
    private readonly IConfigLoader _configLoader = new ConfigLoader();
    private readonly IErrorHandler _errorHandler = new ErrorHandler();
    private readonly IFileCryptor _fileCryptor = new FileCryptor();
    private readonly IFileWriter _fileWriter = new FileWriter();
    private readonly ILanguageManager _lm = new LanguageManager();
    private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    private readonly BackgroundWorker _worker = new BackgroundWorker();
    private Config _config;
    private Language _lang;
    private UploadProgress _uploadProgress;

    public Main()
    {
        TryInitialize();
    }

    private void TryInitialize()
    {
        try
        {
            Initialize();
            _log.Info(_lang.GetWord("StartupSuccessfull"));
        }
        catch (Exception ex)
        {
            _errorHandler.Show(ex);
            _log.Error(ex);
        }
    }

    private void Initialize()
    {
        InitializeComponent();
        InitializeCaption();
        InitializeBackgroundWorker();
        InitializeLanguageManager();
        LoadLanguagesToCombo();
        InitializeUploadProgress();
        ImportConfig();
    }

    private void InitializeUploadProgress()
    {
        _uploadProgress = new UploadProgress(_lang) {Text = _lang.GetWord("UploadProgressTitle")};
    }

    private void InitializeLanguageManager()
    {
        _lm.SetCurrentLanguage("de-DE");
        _lm.OnLanguageChanged += OnLanguageChanged;
    }

    private void LoadLanguagesToCombo()
    {
        foreach (var lang in _lm.GetLanguages())
            comboBoxLanguage.Items.Add(lang.Name);
        comboBoxLanguage.SelectedIndex = 0;
    }

    private void OnLanguageChanged(object sender, EventArgs eventArgs)
    {
        _lang = _lm.GetCurrentLanguage();
        labelLanguage.Text = _lang.GetWord("SelectLanguage");
        AccountInfosToolStripMenuItem.Text = _lang.GetWord("AccountInfoToolStripMenuItem");
        buttonChooseFiles.Text = _lang.GetWord("ChooseFiles");
        buttonChooseOutputFolder.Text = _lang.GetWord("ChooseOutputFolder");
        labelFiles.Text = _lang.GetWord("FilesCaption");
        labelOutputFolder.Text = _lang.GetWord("OutputFolderCaption");
        buttonEncryptAndUpload.Text = _lang.GetWord("EncryptAndUploadText");
    }

    private void ImportConfig()
    {
        _config = _configLoader.LoadConfigFromXmlFile("Config.xml");
    }

    private IFileUploader GetFileUploader(string fileName)
    {
        var fileUploader = new FileUploader(_config, fileName);
        fileUploader.OnUploadSuccessfullGDrive += OnUploadSuccessfullGDrive;
        fileUploader.OnUploadProgessChangedGDrive += OnUploadProgessChangedGDrive;
        fileUploader.OnUploadProgessChangedMega += OnUploadProgessChangedMega;
        return fileUploader;
    }

    private void OnUploadProgessChangedMega(object sender, EventArgs eventArgs)
    {
        var fileUploader = sender as FileUploader;
        if (fileUploader == null) return;
        var eventArgsNew = eventArgs as UploadProgessChangedEventArgsMega;
        if (eventArgsNew == null) return;
        var percentage = eventArgsNew.GetPercentage();
        _uploadProgress.ShowProgressPublic("Mega", fileUploader.FileName, percentage);
    }

    private void OnUploadProgessChangedGDrive(object sender, EventArgs eventArgs)
    {
        var fileUploader = sender as FileUploader;
        if (fileUploader == null) return;
        var eventArgsNew = eventArgs as UploadProgessChangedEventArgs;
        if (eventArgsNew == null) return;
        var sentBytes = eventArgsNew.GetSentBytes();
        var fileSize = GetFileSize(fileUploader.FileName);
        ShowProgressGDrive(sentBytes, fileSize, fileUploader);
    }

    private void ShowProgressGDrive(long sentBytes, long fileSize, IFileUploader fileUploader)
    {
        if (sentBytes == fileSize)
        {
            // ReSharper disable once RedundantCast
            _uploadProgress.ShowProgressPublic("GDrive", fileUploader.FileName, (double)100.0);
        }
        else
        {
            var percentage = (double)sentBytes / fileSize;
            _uploadProgress.ShowProgressPublic("GDrive", fileUploader.FileName, percentage);
        }
    }

    private long GetFileSize(string fileName)
    {
        return new FileInfo(fileName).Length;
    }

    private void OnUploadSuccessfullGDrive(object sender, EventArgs eventArgs)
    {
        var fileUploader = sender as FileUploader;
        if (fileUploader == null) return;
        // ReSharper disable once RedundantCast
        _uploadProgress.ShowProgressPublic("GDrive", fileUploader.FileName, (double)100);
    }

    private void InitializeCaption()
    {
        Text = Application.ProductName + @" " + Application.ProductVersion;
    }

    private void InitializeBackgroundWorker()
    {
        _worker.WorkerReportsProgress = true;
        _worker.WorkerSupportsCancellation = true;
        _worker.DoWork += EncryptionAndUploadWork;
    }

    private void EncryptionAndUploadWork(object sender, DoWorkEventArgs e)
    {
        EncryptAndUploadFiles(listBoxFiles.Items);
    }

    private void ButtonChooseFiles_Click(object sender, EventArgs e)
    {
        listBoxFiles.Items.Clear();
        var fileBrowser = new OpenFileDialog {Multiselect = true};
        var result = fileBrowser.ShowDialog();
        if (result == DialogResult.OK)
            AddFileNamesTorRichTextBox(fileBrowser.FileNames);
    }

    private void AddFileNamesTorRichTextBox(string[] fileNames)
    {
        try
        {
            if (fileNames == null) throw new ArgumentNullException(nameof(fileNames));
            foreach (var file in fileNames)
                listBoxFiles.Items.Add(file);
        }
        catch (Exception ex)
        {
            _errorHandler.Show(ex);
            _log.Error(ex);
        }
    }

    private void ButtonChooseFolder_Click(object sender, EventArgs e)
    {
        listBoxOutputFolder.Items.Clear();
        var folderBrowser = new FolderBrowserDialog();
        var result = folderBrowser.ShowDialog();
        if (result == DialogResult.OK)
            listBoxOutputFolder.Items.Add(folderBrowser.SelectedPath);
    }

    private void EncryptAndUploadFiles(ListBox.ObjectCollection fileNames)
    {
        if (fileNames == null) throw new ArgumentNullException(nameof(fileNames));
        var outputFolder = string.Empty;
        this.UiThreadInvoke(() => { outputFolder = listBoxOutputFolder.Items[0].ToString(); });
        var tasks = StartNewTasks(outputFolder, fileNames);
        WaitIfNotFinishedAndFinish(tasks);
    }

    private void WaitIfNotFinishedAndFinish(IReadOnlyCollection<Task> tasks)
    {
        while ((_uploadProgress.GetRowsCount() != tasks.Count * 2) || (_uploadProgress.GetLastRowValue()<99)) //Todo set to 3 for filehorst, too.
        {
            Thread.Sleep(1);
        }
        UploadFinished();
    }

    private List<Task> StartNewTasks(string outputFolder, ListBox.ObjectCollection fileNames)
    {
        return (from object file in fileNames
            select Task.Factory.StartNew(() =>
                    EncryptAndUploadFile(file.ToString(), outputFolder),
                TaskCreationOptions.LongRunning | TaskCreationOptions.PreferFairness)).ToList();
    }

    private void UploadFinished()
    {
        LockGui(false);
        MessageBox.Show(_lang.GetWord("UploadFinishedText"), _lang.GetWord("UploadFinishedCaption"),
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private async void EncryptAndUploadFile(string fileName, string outputFolder)
    {
        try
        {
            var item = _fileCryptor.EncryptFile(fileName, outputFolder);
            _log.Info(string.Format(_lang.GetWord("EncryptionFinished"), fileName));
            var fileUploader = GetFileUploader(item.NewFileName);
            var uploadedFile = await UploadFile(fileUploader);
            Wait(uploadedFile);
            AddLinksToItem(item, uploadedFile);
            _fileWriter.WriteToFile(item);
            _log.Info(string.Format(_lang.GetWord("UploadFinished"), fileName));
        }
        catch (Exception ex)
        {
            _errorHandler.Show(ex);
            _log.Error(ex);
        }
    }

    private void Wait(LinkTriple uploadedFile)
    {
        // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
        while (uploadedFile == null)
        {
            Thread.Sleep(1);
        }
    }

    private void AddLinksToItem(UploadItem item, LinkTriple linkTriple)
    {
        item.MegaLink = linkTriple.MegaLink;
        item.GDriveLink = linkTriple.GDriveLink;
        item.FilehorstLink = linkTriple.FilehorstLink;
    }

    private bool CheckReady()
    {
        return listBoxFiles.Items.Count > 0 && listBoxOutputFolder.Items.Count > 0;
    }

    private void ButtonEncryptAndUpload_Click(object sender, EventArgs e)
    {
        if (CheckReady())
        {
            LockGui(true);
            _worker.RunWorkerAsync();
        }
        else
        {
            MessageBox.Show(_lang.GetWord("SelectFolderAndFilesText"), _lang.GetWord("SelectFolderAndFilesCaption"),
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void LockGui(bool value)
    {
        this.UiThreadInvoke(() => {
            buttonChooseFiles.Enabled = !value;
            buttonChooseOutputFolder.Enabled = !value;
            buttonEncryptAndUpload.Enabled = !value;
            if (string.IsNullOrWhiteSpace(_uploadProgress.Text))
            {
                _uploadProgress = new UploadProgress(_lang) {Text = _lang.GetWord("UploadProgressTitle")};
            }
            _uploadProgress.Show();
        });
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);
        Environment.Exit(0);
    }

    private async Task<LinkTriple> UploadFile(IFileUploader fileUploader)
    {
        var megaLink = await fileUploader.UploadToMega();
        return new LinkTriple
        {
            MegaLink = megaLink,
            GDriveLink = fileUploader.UploadToGDrive(),
            FilehorstLink = fileUploader.UploadToFilehorst()
        };
    }

    private void AccountInfosToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var accountInfos = new AccountInfos {Config = _config, CurrentLanguage = _lang, Text = _lang.GetWord("AccountInfoTitle") };
        accountInfos.Show();
        accountInfos.LogAccountsPublic();
    }

    private void ComboBoxLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        var currentLanguage = comboBoxLanguage.SelectedItem.ToString();
        _lm.SetCurrentLanguageFromName(currentLanguage);
        _log.Info(_lang.GetWord("LanguageChanged") + currentLanguage);
    }
}