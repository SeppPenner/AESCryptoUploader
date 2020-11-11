// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountInfos.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The account information page.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AESCryptoUploader
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Windows.Forms;

    using AESCryptoUploader.Implementation;
    using AESCryptoUploader.Interfaces;
    using AESCryptoUploader.Models;
    using AESCryptoUploader.UiThreadInvoke;

    using CG.Web.MegaApiClient;

    using Languages.Interfaces;

    using Serilog;

    /// <summary>
    /// The account information page.
    /// </summary>
    public partial class AccountInfos : Form
    {
        /// <summary>
        /// The background worker.
        /// </summary>
        private readonly BackgroundWorker worker = new BackgroundWorker();

        /// <summary>
        /// The Google Drive service.
        /// </summary>
        private readonly ICustomGDriveService googleDriveService = new CustomGDriveService();

        /// <summary>
        /// The error handler.
        /// </summary>
        private readonly IErrorHandler errorHandler = new ErrorHandler();

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountInfos"/> class.
        /// </summary>
        public AccountInfos()
        {
            this.InitializeComponent();
            this.InitializeBackgroundWorker();
        }

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        // ReSharper disable once MemberCanBePrivate.Global
        public Config Config { get; set; }

        /// <summary>
        /// Gets or sets the current language.
        /// </summary>
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public ILanguage CurrentLanguage { get; set; }

        /// <summary>
        /// Logs the accounts.
        /// </summary>
        public void LogAccountsPublic()
        {
            this.worker.RunWorkerAsync();
        }

        /// <summary>
        /// Gets the free space.
        /// </summary>
        /// <param name="usedQuota">The used quota.</param>
        /// <param name="totalQuota">The total quote.</param>
        /// <returns>The free space as <see cref="long"/>.</returns>
        private static long GetFreeSpace(long usedQuota, long totalQuota)
        {
            if (totalQuota == usedQuota)
            {
                return 0;
            }

            return totalQuota - usedQuota;
        }

        /// <summary>
        /// Gets the used quota.
        /// </summary>
        /// <param name="usedQuota">The used quota.</param>
        /// <param name="totalQuota">The total quota.</param>
        /// <returns>The used quota as <see cref="int"/>.</returns>
        private static int GetQuotaUsed(double usedQuota, double totalQuota)
        {
            return (int)(usedQuota / totalQuota * 100.0f);
        }

        /// <summary>
        /// Gets the mega bytes.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>The mega bytes as <see cref="double"/>.</returns>
        private static double GetMegabytes(long bytes)
        {
            return Math.Round(Convert.ToDouble(Convert.ToDouble(bytes / 1024) / 1024), 2);
        }

        /// <summary>
        /// Adds an account to the table.
        /// </summary>
        /// <param name="hoster">The hoster.</param>
        /// <param name="accountName">The account name.</param>
        /// <param name="freeSpace">The free space.</param>
        /// <param name="totalSpace">The total space.</param>
        /// <param name="usedSpace">The used space.</param>
        /// <param name="filled">The filled percentage value.</param>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        private void AddAccountToTable(string hoster, string accountName, double freeSpace, double totalSpace, double usedSpace, double filled)
        {
            this.UiThreadInvoke(
                () =>
                {
                    var row = new object[] { hoster, accountName, freeSpace, totalSpace, usedSpace, filled };
                    this.dataGridViewAccounts.Rows.Add(row);
                });
        }

        /// <summary>
        /// Clears the columns.
        /// </summary>
        private void ClearColumns()
        {
            this.UiThreadInvoke(() => { this.dataGridViewAccounts.Rows.Clear(); });
        }

        /// <summary>
        /// Initializes the headers.
        /// </summary>
        private void InitializeHeaders()
        {
            this.UiThreadInvoke(
                () =>
                {
                    this.dataGridViewAccounts.Columns[0].HeaderText = this.CurrentLanguage.GetWord("Account");
                    this.dataGridViewAccounts.Columns[1].HeaderText = this.CurrentLanguage.GetWord("Username");
                    this.dataGridViewAccounts.Columns[2].HeaderText = this.CurrentLanguage.GetWord("FreeSpaceInMb");
                    this.dataGridViewAccounts.Columns[3].HeaderText = this.CurrentLanguage.GetWord("TotalSpaceInMb");
                    this.dataGridViewAccounts.Columns[4].HeaderText = this.CurrentLanguage.GetWord("UsedSpaceInMb");
                    this.dataGridViewAccounts.Columns[5].HeaderText =
                        this.CurrentLanguage.GetWord("UsedSpaceInPercent");
                });
        }

        /// <summary>
        /// Initializes the background worker.
        /// </summary>
        private void InitializeBackgroundWorker()
        {
            this.worker.WorkerReportsProgress = true;
            this.worker.WorkerSupportsCancellation = true;
            this.worker.DoWork += this.AccountInfosWork;
        }

        /// <summary>
        /// Runs the background work for the account loading.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void AccountInfosWork(object sender, DoWorkEventArgs e)
        {
            this.LogAccounts();
        }

        /// <summary>
        /// Loads the account information.
        /// </summary>
        private void LogAccounts()
        {
            this.ClearColumns();
            this.InitializeHeaders();
            this.CheckGoogleAccounts();
            this.CheckMegaAccounts();
        }

        /// <summary>
        /// Checks the Google Drive accounts.
        /// </summary>
        private void CheckGoogleAccounts()
        {
            try
            {
                var possibleAccounts = this.Config.Accounts.Where(x => x.Name.ToLower().Equals("gdrive"));
                foreach (var account in possibleAccounts)
                {
                    var client = this.googleDriveService.GetDriveService(account.ClientId, account.Password, account.UserName);
                    var quotaUsed = this.googleDriveService.GetQuotaUsed(client);
                    var quotaTotal = this.googleDriveService.GetQuotaTotal(client);
                    var freeSpace = GetFreeSpace(quotaUsed, quotaTotal);
                    this.AddAccountToTable(
                        "Google Drive",
                        account.UserName,
                        GetMegabytes(freeSpace),
                        GetMegabytes(quotaTotal),
                        GetMegabytes(quotaUsed),
                        GetQuotaUsed(quotaUsed, quotaTotal));
                }
            }
            catch (Exception ex)
            {
                this.errorHandler.Show(ex);
                Log.Error("An exception occurred: {@Exception}", ex);
            }
        }

        /// <summary>
        /// Checks the mega.nz accounts.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        private void CheckMegaAccounts()
        {
            try
            {
                var client = new MegaApiClient();
                var possibleAccounts = this.Config.Accounts.Where(x => x.Name.ToLower().Equals("mega"));
                foreach (var account in possibleAccounts)
                {
                    client.Login(account.UserName, account.Password);
                    var info = client.GetAccountInformation();
                    var freeSpace = GetFreeSpace(info.UsedQuota, info.TotalQuota);
                    this.AddAccountToTable(
                        "Mega.nz",
                        account.UserName,
                        GetMegabytes(freeSpace),
                        GetMegabytes(info.TotalQuota),
                        GetMegabytes(info.UsedQuota),
                        GetQuotaUsed(info.UsedQuota, info.TotalQuota));
                    client.Logout();
                }
            }
            catch (Exception ex)
            {
                this.errorHandler.Show(ex);
                Log.Error("An exception occurred: {@Exception}", ex);
            }
        }
    }
}