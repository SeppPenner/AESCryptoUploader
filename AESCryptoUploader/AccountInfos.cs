using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CG.Web.MegaApiClient;
using Implementation;
using Interfaces;
using log4net;
using Languages.Implementation;
using Models;
using UiThreadInvoke;

public partial class AccountInfos : Form
{
    private readonly BackgroundWorker _worker = new BackgroundWorker();
    private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    private readonly ICustomGDriveService _gDriveService = new CustomGDriveService();
    private readonly IErrorHandler _errorHandler = new ErrorHandler();

    public AccountInfos()
    {
        InitializeComponent();
        InitializeBackgroundWorker();
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public Config Config { get; set; }

    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public Language CurrentLanguage { get; set; }

    private void ClearColumns()
    {
        this.UiThreadInvoke(() => { dataGridViewAccounts.Rows.Clear();});
    }

    private void InitializeHeaders()
    {
        this.UiThreadInvoke(() => {
            dataGridViewAccounts.Columns[0].HeaderText = CurrentLanguage.GetWord("Account");
            dataGridViewAccounts.Columns[1].HeaderText = CurrentLanguage.GetWord("Username");
            dataGridViewAccounts.Columns[2].HeaderText = CurrentLanguage.GetWord("FreeSpaceInMb");
            dataGridViewAccounts.Columns[3].HeaderText = CurrentLanguage.GetWord("TotalSpaceInMb");
            dataGridViewAccounts.Columns[4].HeaderText = CurrentLanguage.GetWord("UsedSpaceInMb");
            dataGridViewAccounts.Columns[5].HeaderText = CurrentLanguage.GetWord("UsedSpaceInPercent");
        });
    }

    private void InitializeBackgroundWorker()
    {
        _worker.WorkerReportsProgress = true;
        _worker.WorkerSupportsCancellation = true;
        _worker.DoWork += AccountInfosWork;
    }

    public void LogAccountsPublic()
    {
        _worker.RunWorkerAsync();
    }

    private void AccountInfosWork(object sender, DoWorkEventArgs e)
    {
        LogAccounts();
    }

    private void LogAccounts()
    {
        ClearColumns();
        InitializeHeaders();
        CheckGoogleAccounts();
        CheckMegaAccounts();
    }

    private void CheckGoogleAccounts()
    {
        try
        {
            var possibleAccounts = Config.Accounts.Where(x => x.Name.ToLower().Equals("gdrive"));
            foreach (var account in possibleAccounts)
            {
                var client = _gDriveService.GetDriveService(account.ClientId, account.Password, account.UserName);
                var quotaUsed = _gDriveService.GetQuotaUsed(client);
                var quotaTotal = _gDriveService.GetQuotaTotal(client);
                var freeSpace = GetFreeSpace(quotaUsed, quotaTotal);
                AddAccountToTable("Google Drive", account.UserName, GetMegabytes(freeSpace), GetMegabytes(quotaTotal),
                    GetMegabytes(quotaUsed), GetQuotaUsed(quotaUsed, quotaTotal));
            }
        }
        catch (Exception ex)
        {
            _errorHandler.Show(ex);
            _log.Error(ex);
        }
    }

    private void CheckMegaAccounts()
    {
        try
        {
            var client = new MegaApiClient();
            var possibleAccounts = Config.Accounts.Where(x => x.Name.ToLower().Equals("mega"));
            foreach (var account in possibleAccounts)
            {
                client.Login(account.UserName, account.Password);
                var info = client.GetAccountInformation();
                var freeSpace = GetFreeSpace(info.UsedQuota, info.TotalQuota);
                AddAccountToTable("Mega.nz", account.UserName, GetMegabytes(freeSpace), GetMegabytes(info.TotalQuota),
                    GetMegabytes(info.UsedQuota), GetQuotaUsed(info.UsedQuota, info.TotalQuota));
                client.Logout();
            }
        }
        catch (Exception ex)
        {
            _errorHandler.Show(ex);
            _log.Error(ex);
        }
    }

    private long GetFreeSpace(long usedQuota, long totalQuuta)
    {
        if (totalQuuta == usedQuota)
        {
            return 0;
        }
        return totalQuuta - usedQuota;
    }

    private void AddAccountToTable(string hoster, string accountName, double freeSpace, double totalSpace, double usedSpace, double filled)
    {
        this.UiThreadInvoke(() => {
            var row = new object[] { hoster, accountName, freeSpace, totalSpace, usedSpace, filled };
            dataGridViewAccounts.Rows.Add(row);
        });
    }

    private int GetQuotaUsed(double usedQuota, double totalQuota)
    {
        return (int)(usedQuota / totalQuota * 100.0f);
    }

    private double GetMegabytes(long bytes)
    {
        return Math.Round(Convert.ToDouble(Convert.ToDouble(bytes / 1024) / 1024), 2);
    }
}