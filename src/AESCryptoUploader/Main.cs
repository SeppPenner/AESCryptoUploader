// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Main.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The main view.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AESCryptoUploader
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using AESCryptoUploader.Events;
    using AESCryptoUploader.Implementation;
    using AESCryptoUploader.Interfaces;
    using AESCryptoUploader.Models;
    using AESCryptoUploader.UiThreadInvoke;

    using Languages.Implementation;
    using Languages.Interfaces;

    using Serilog;

    /// <summary>
    /// The main view.
    /// </summary>
    public partial class Main : Form
    {
        /// <summary>
        /// The configuration loader.
        /// </summary>
        private readonly IConfigLoader configurationLoader = new ConfigLoader();

        /// <summary>
        /// The error handler.
        /// </summary>
        private readonly IErrorHandler errorHandler = new ErrorHandler();

        /// <summary>
        /// The file cryptor.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        private readonly IFileCryptor fileCryptor = new FileCryptor();

        /// <summary>
        /// The file writer.
        /// </summary>
        private readonly IFileWriter fileWriter = new FileWriter();

        /// <summary>
        /// The language manager.
        /// </summary>
        private readonly ILanguageManager languageManager = new LanguageManager();

        /// <summary>
        /// The background worker.
        /// </summary>
        private readonly BackgroundWorker worker = new BackgroundWorker();

        /// <summary>
        /// The configuration.
        /// </summary>
        private Config configuration;

        /// <summary>
        /// The language.
        /// </summary>
        private ILanguage language;

        /// <summary>
        /// The upload progress.
        /// </summary>
        private UploadProgress uploadProgress;

        /// <summary>
        /// Initializes a new instance of the <see cref="Main"/> class.
        /// </summary>
        public Main()
        {
            this.TryInitialize();
        }

        /// <summary>
        /// Handles the form close event.
        /// </summary>
        /// <param name="e">The event args.</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            Environment.Exit(0);
        }

        /// <summary>
        /// Uploads a file.
        /// </summary>
        /// <param name="fileUploader">The file uploader.</param>
        /// <returns>A <see cref="Task"/> representing any asynchronous operation.</returns>
        private static async Task<LinkTriple> UploadFile(IFileUploader fileUploader)
        {
            var megaLink = await fileUploader.UploadToMega();
            return new LinkTriple
            {
                MegaLink = megaLink,
                GDriveLink = fileUploader.UploadToGDrive(),
                FilehorstLink = fileUploader.UploadToFilehorst()
            };
        }

        /// <summary>
        /// Gets the file size.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <returns>The file size as <see cref="long"/>.</returns>
        private static long GetFileSize(string fileName)
        {
            return new FileInfo(fileName).Length;
        }

        /// <summary>
        /// Waits while the uploaded file is not ready.
        /// </summary>
        /// <param name="uploadedFile">The uploaded file.</param>
        private static void Wait(LinkTriple uploadedFile)
        {
            // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
            while (uploadedFile == null)
            {
                Thread.Sleep(1);
            }
        }

        /// <summary>
        /// Adds the links to the item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="linkTriple">The link triple.</param>
        private static void AddLinksToItem(UploadItem item, LinkTriple linkTriple)
        {
            item.MegaLink = linkTriple.MegaLink;
            item.GDriveLink = linkTriple.GDriveLink;
            item.FilehorstLink = linkTriple.FilehorstLink;
        }

        /// <summary>
        /// Tries to initialize the view.
        /// </summary>
        private void TryInitialize()
        {
            try
            {
                this.Initialize();
                Log.Information(this.language.GetWord("StartupSuccessful"));
            }
            catch (Exception ex)
            {
                this.errorHandler.Show(ex);
                Log.Error("An exception occurred: {@Exception}", ex);
            }
        }

        /// <summary>
        /// Initializes the view.
        /// </summary>
        private void Initialize()
        {
            this.InitializeComponent();
            this.InitializeCaption();
            this.InitializeBackgroundWorker();
            this.InitializeLanguageManager();
            this.LoadLanguagesToCombo();
            this.InitializeUploadProgress();
            this.ImportConfig();
        }

        /// <summary>
        /// Initializes the upload progress.
        /// </summary>
        private void InitializeUploadProgress()
        {
            this.uploadProgress =
                new UploadProgress(this.language) { Text = this.language.GetWord("UploadProgressTitle") };
        }

        /// <summary>
        /// Initializes the language manager.
        /// </summary>
        private void InitializeLanguageManager()
        {
            this.languageManager.SetCurrentLanguage("de-DE");
            this.languageManager.OnLanguageChanged += this.OnLanguageChanged;
        }

        /// <summary>
        /// Loads all languages to the combo box.
        /// </summary>
        private void LoadLanguagesToCombo()
        {
            foreach (var lang in this.languageManager.GetLanguages())
            {
                this.comboBoxLanguage.Items.Add(lang.Name);
            }

            this.comboBoxLanguage.SelectedIndex = 0;
        }

        /// <summary>
        /// Handles the language changed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        private void OnLanguageChanged(object sender, EventArgs eventArgs)
        {
            this.language = this.languageManager.GetCurrentLanguage();
            this.labelLanguage.Text = this.language.GetWord("SelectLanguage");
            this.AccountInfosToolStripMenuItem.Text = this.language.GetWord("AccountInfoToolStripMenuItem");
            this.buttonChooseFiles.Text = this.language.GetWord("ChooseFiles");
            this.buttonChooseOutputFolder.Text = this.language.GetWord("ChooseOutputFolder");
            this.labelFiles.Text = this.language.GetWord("FilesCaption");
            this.labelOutputFolder.Text = this.language.GetWord("OutputFolderCaption");
            this.buttonEncryptAndUpload.Text = this.language.GetWord("EncryptAndUploadText");
        }

        /// <summary>
        /// Imports the configuration.
        /// </summary>
        private void ImportConfig()
        {
            this.configuration = this.configurationLoader.LoadConfigFromXmlFile("Config.xml");
        }

        /// <summary>
        /// Gets the file uploader.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <returns>A new <see cref="IFileUploader"/>.</returns>
        private IFileUploader GetFileUploader(string fileName)
        {
            var fileUploader = new FileUploader(this.configuration, fileName);
            fileUploader.OnUploadSuccessfulGDrive += this.OnUploadSuccessfulGDrive;
            fileUploader.OnUploadProgressChangedGDrive += this.OnUploadProgressChangedGDrive;
            fileUploader.OnUploadProgressChangedMega += this.OnUploadProgressChangedMega;
            return fileUploader;
        }

        /// <summary>
        /// Handles the event when the upload progress changed for mega.nz.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        private void OnUploadProgressChangedMega(object sender, EventArgs eventArgs)
        {
            if (!(sender is FileUploader fileUploader))
            {
                return;
            }

            if (!(eventArgs is UploadProgressChangedEventArgsMega eventArgsNew))
            {
                return;
            }

            var percentage = eventArgsNew.GetPercentage();
            this.uploadProgress.ShowProgressPublic("Mega", fileUploader.FileName, percentage);
        }

        /// <summary>
        /// Handles the event when the upload progress changed for Google Drive.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        private void OnUploadProgressChangedGDrive(object sender, EventArgs eventArgs)
        {
            if (!(sender is FileUploader fileUploader))
            {
                return;
            }

            if (!(eventArgs is UploadProgressChangedEventArgs eventArgsNew))
            {
                return;
            }

            var sentBytes = eventArgsNew.GetSentBytes();
            var fileSize = GetFileSize(fileUploader.FileName);
            // ReSharper disable once StyleCop.SA1126
            this.ShowProgressGDrive(sentBytes, fileSize, fileUploader);
        }

        /// <summary>
        /// Shows the progress for Google Drive.
        /// </summary>
        /// <param name="sentBytes">The sent bytes.</param>
        /// <param name="fileSize">The file size.</param>
        /// <param name="fileUploader">The file uploader.</param>
        private void ShowProgressGDrive(long sentBytes, long fileSize, IFileUploader fileUploader)
        {
            if (sentBytes == fileSize)
            {
                // ReSharper disable once RedundantCast
                this.uploadProgress.ShowProgressPublic("GDrive", fileUploader.FileName, (double)100.0);
            }
            else
            {
                var percentage = (double)sentBytes / fileSize;
                this.uploadProgress.ShowProgressPublic("GDrive", fileUploader.FileName, percentage);
            }
        }

        /// <summary>
        /// Handles the on upload successful event for Google Drive.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        private void OnUploadSuccessfulGDrive(object sender, EventArgs eventArgs)
        {
            if (!(sender is FileUploader fileUploader))
            {
                return;
            }

            // ReSharper disable once RedundantCast
            this.uploadProgress.ShowProgressPublic("GDrive", fileUploader.FileName, (double)100);
        }

        /// <summary>
        /// Initializes the caption.
        /// </summary>
        private void InitializeCaption()
        {
            this.Text = Application.ProductName + @" " + Application.ProductVersion;
        }

        /// <summary>
        /// Initializes the background worker.
        /// </summary>
        private void InitializeBackgroundWorker()
        {
            this.worker.WorkerReportsProgress = true;
            this.worker.WorkerSupportsCancellation = true;
            this.worker.DoWork += this.EncryptionAndUploadWork;
        }

        /// <summary>
        /// Handles the encrypt and upload work in the background.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void EncryptionAndUploadWork(object sender, DoWorkEventArgs e)
        {
            this.EncryptAndUploadFiles(this.listBoxFiles.Items);
        }

        /// <summary>
        /// Handles the choose files click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void ButtonChooseFilesClick(object sender, EventArgs e)
        {
            this.listBoxFiles.Items.Clear();
            var fileBrowser = new OpenFileDialog { Multiselect = true };
            var result = fileBrowser.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.AddFileNamesTorRichTextBox(fileBrowser.FileNames);
            }
        }

        /// <summary>
        /// Adds the file names to the rich text box.
        /// </summary>
        /// <param name="fileNames">The file names.</param>
        private void AddFileNamesTorRichTextBox(string[] fileNames)
        {
            try
            {
                if (fileNames == null)
                {
                    throw new ArgumentNullException(nameof(fileNames));
                }

                foreach (var file in fileNames)
                {
                    this.listBoxFiles.Items.Add(file);
                }
            }
            catch (Exception ex)
            {
                this.errorHandler.Show(ex);
                Log.Error("An exception occurred: {@Exception}", ex);
            }
        }

        /// <summary>
        /// Handles the choose folder button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void ButtonChooseFolderClick(object sender, EventArgs e)
        {
            this.listBoxOutputFolder.Items.Clear();
            var folderBrowser = new FolderBrowserDialog();
            var result = folderBrowser.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.listBoxOutputFolder.Items.Add(folderBrowser.SelectedPath);
            }
        }

        /// <summary>
        /// Encrypts and uploads the files.
        /// </summary>
        /// <param name="fileNames">The file names.</param>
        private void EncryptAndUploadFiles(ListBox.ObjectCollection fileNames)
        {
            if (fileNames == null)
            {
                throw new ArgumentNullException(nameof(fileNames));
            }

            var outputFolder = string.Empty;
            this.UiThreadInvoke(() => { outputFolder = this.listBoxOutputFolder.Items[0].ToString(); });
            var tasks = this.StartNewTasks(outputFolder, fileNames);
            this.WaitIfNotFinishedAndFinish(tasks);
        }

        /// <summary>
        /// Waits if the upload is not finished and finishes the upload.
        /// </summary>
        /// <param name="tasks">The tasks.</param>
        private void WaitIfNotFinishedAndFinish(IReadOnlyCollection<Task> tasks)
        {
            // Todo: Set to 3 for filehorst, too.
            while (this.uploadProgress.GetRowsCount() != tasks.Count * 2
                   || this.uploadProgress.GetLastRowValue() < 99)
            {
                Thread.Sleep(1);
            }

            this.UploadFinished();
        }

        /// <summary>
        /// Starts the new tasks.
        /// </summary>
        /// <param name="outputFolder">The output folder.</param>
        /// <param name="fileNames">The file names.</param>
        /// <returns>A <see cref="List{T}"/> of <see cref="Task"/>s.</returns>
        private List<Task> StartNewTasks(string outputFolder, IEnumerable fileNames)
        {
            return (from object file in fileNames
                    select Task.Factory.StartNew(
                        () => this.EncryptAndUploadFile(file.ToString(), outputFolder),
                        TaskCreationOptions.LongRunning | TaskCreationOptions.PreferFairness)).ToList();
        }

        /// <summary>
        /// The method to signal that the upload us finished.
        /// </summary>
        private void UploadFinished()
        {
            this.LockGui(false);
            MessageBox.Show(
                this.language.GetWord("UploadFinishedText"),
                this.language.GetWord("UploadFinishedCaption"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        /// <summary>
        /// Encrypts and uploads the file.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="outputFolder">The output folder.</param>
        private async void EncryptAndUploadFile(string fileName, string outputFolder)
        {
            try
            {
                var item = this.fileCryptor.EncryptFile(fileName, outputFolder);
                Log.Information(this.language.GetWord("EncryptionFinished") + fileName);
                var fileUploader = this.GetFileUploader(item.NewFileName);
                var uploadedFile = await UploadFile(fileUploader);
                Wait(uploadedFile);
                AddLinksToItem(item, uploadedFile);
                this.fileWriter.WriteToFile(item);
                Log.Information(this.language.GetWord("UploadFinished") + fileName);
            }
            catch (Exception ex)
            {
                this.errorHandler.Show(ex);
                Log.Error("An exception occurred: {@Exception}", ex);
            }
        }

        /// <summary>
        /// Checks whether the list box is ready or not.
        /// </summary>
        /// <returns>A <see cref="bool"/> indicating whether the list box is ready or not.</returns>
        private bool CheckReady()
        {
            return this.listBoxFiles.Items.Count > 0 && this.listBoxOutputFolder.Items.Count > 0;
        }

        /// <summary>
        /// Handles the button click to encrypt and upload files.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void ButtonEncryptAndUploadClick(object sender, EventArgs e)
        {
            if (this.CheckReady())
            {
                this.LockGui(true);
                this.worker.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show(
                    this.language.GetWord("SelectFolderAndFilesText"),
                    this.language.GetWord("SelectFolderAndFilesCaption"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Locks the GUI.
        /// </summary>
        /// <param name="value">The value.</param>
        private void LockGui(bool value)
        {
            this.UiThreadInvoke(
                () =>
                {
                    this.buttonChooseFiles.Enabled = !value;
                    this.buttonChooseOutputFolder.Enabled = !value;
                    this.buttonEncryptAndUpload.Enabled = !value;
                    if (string.IsNullOrWhiteSpace(this.uploadProgress.Text))
                    {
                        this.uploadProgress =
                            new UploadProgress(this.language) { Text = this.language.GetWord("UploadProgressTitle") };
                    }

                    this.uploadProgress.Show();
                });
        }

        /// <summary>
        /// Handles the click on the account information tooltip menu item.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void AccountInfosToolStripMenuItemClick(object sender, EventArgs e)
        {
            var accountInfos = new AccountInfos
            {
                Config = this.configuration, CurrentLanguage = this.language, Text = this.language.GetWord("AccountInfoTitle")
            };

            accountInfos.Show();
            accountInfos.LogAccountsPublic();
        }

        /// <summary>
        /// Handles the selected language changed event in the combo box.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void ComboBoxLanguageSelectedIndex(object sender, EventArgs e)
        {
            var currentLanguage = this.comboBoxLanguage.SelectedItem.ToString();
            this.languageManager.SetCurrentLanguageFromName(currentLanguage);
            Log.Information(this.language.GetWord("LanguageChanged") + currentLanguage);
        }
    }
}