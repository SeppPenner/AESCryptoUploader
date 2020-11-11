// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileUploader.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   A class to upload files.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AESCryptoUploader.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using AESCryptoUploader.Events;
    using AESCryptoUploader.Interfaces;
    using AESCryptoUploader.Models;

    using CG.Web.MegaApiClient;

    using Google.Apis.Drive.v3;

    /// <inheritdoc cref="IFileUploader"/>
    /// <summary>
    /// A class to upload files.
    /// </summary>
    /// <seealso cref="IFileUploader"/>
    public class FileUploader : IFileUploader
    {
        /// <summary>
        /// The configuration.
        /// </summary>
        private readonly Config configuration;

        /// <summary>
        /// The custom Google Drive service.
        /// </summary>
        private readonly ICustomGDriveService customGDriveService = new CustomGDriveService();

        /// <summary>
        /// Initializes a new instance of the <see cref="FileUploader"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="fileName">The file name.</param>
        public FileUploader(Config configuration, string fileName)
        {
            this.configuration = configuration;
            this.FileName = fileName;
            this.customGDriveService.OnUploadProgressChanged += this.OnUploadProgressChangedGDriveService;
            this.customGDriveService.OnUploadSuccessful += this.OnUploadSuccessfulGDriveService;
        }

        /// <inheritdoc cref="IFileUploader"/>
        /// <summary>
        /// Handles the the upload progress changed event for Google Drive.
        /// </summary>
        /// <seealso cref="IFileUploader"/>
        public event EventHandler OnUploadProgressChangedGDrive;

        /// <inheritdoc cref="IFileUploader"/>
        /// <summary>
        /// Handles the the upload successful event for Google Drive.
        /// </summary>
        /// <seealso cref="IFileUploader"/>
        public event EventHandler OnUploadProgressChangedMega;

        /// <inheritdoc cref="IFileUploader"/>
        /// <summary>
        /// Handles the the upload progress changed event for mega.nz.
        /// </summary>
        /// <seealso cref="IFileUploader"/>
        public event EventHandler OnUploadSuccessfulGDrive;

        /// <summary>
        /// Gets the file name.
        /// </summary>
        public string FileName { get; }

        /// <inheritdoc cref="IFileUploader"/>
        /// <summary>
        /// Uploads the file to filehorst.de.
        /// </summary>
        /// <returns>The result as <see cref="string"/>.</returns>
        /// <seealso cref="IFileUploader"/>
        public string UploadToFilehorst()
        {
            // Todo: Add this
            return string.Empty;
        }

        /// <inheritdoc cref="IFileUploader"/>
        /// <summary>
        /// Uploads the file to Google Drive.
        /// </summary>
        /// <returns>The result as <see cref="string"/>.</returns>
        /// <seealso cref="IFileUploader"/>
        public string UploadToGDrive()
        {
            var possibleAccounts = this.GetPossibleAccounts("gdrive");

            foreach (var account in possibleAccounts)
            {
                var service = this.GetDriveService(account);

                if (this.GetQuotaUsed(service) + GetFileSize(this.FileName) > this.GetQuotaTotal(service))
                {
                    continue;
                }

                var root = this.GetRootFolderId(service);
                return this.customGDriveService.UploadToGDrive(service, this.FileName, root);
            }

            return string.Empty;
        }

        /// <inheritdoc cref="IFileUploader"/>
        /// <summary>
        /// Uploads the file to mega.nz.
        /// </summary>
        /// <returns>The result as <see cref="string"/>.</returns>
        /// <seealso cref="IFileUploader"/>
        public async Task<string> UploadToMega()
        {
            var client = new MegaApiClient();
            var possibleAccounts = this.GetPossibleAccounts("mega");

            foreach (var account in possibleAccounts)
            {
                LoginMega(client, account);
                var info = await client.GetAccountInformationAsync();

                if (info.UsedQuota + GetFileSize(this.FileName) > info.TotalQuota)
                {
                    continue;
                }

                var root = GetRootFolderMega(client);
                var progress = this.GetProgress();
                var node = await client.UploadFileAsync(this.FileName, root, progress, null);
                var link = await client.GetDownloadLinkAsync(node);
                await client.LogoutAsync();
                return link.AbsoluteUri;
            }

            return string.Empty;
        }

        /// <summary>
        /// Does a login for mega.nz.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="account">The account.</param>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        private static void LoginMega(IMegaApiClient client, Account account)
        {
            client.Login(account.UserName, account.Password);
        }

        /// <summary>
        /// Gets the root folder for mega.nz.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns>A <see cref="INode"/>.</returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        private static INode GetRootFolderMega(IMegaApiClient client)
        {
            return client.GetNodes().Single(n => n.Type == NodeType.Root);
        }

        /// <summary>
        /// Gets the file size.
        /// </summary>
        /// <param name="fileName">The file size.</param>
        /// <returns>The file size as <see cref="long"/>.</returns>
        private static long GetFileSize(string fileName)
        {
            return new FileInfo(fileName).Length;
        }

        /// <summary>
        /// Handles the upload successful event in the Google Drive service.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void OnUploadSuccessfulGDriveService(object sender, EventArgs e)
        {
            this.UploadSuccessfulGDrive(e);
        }

        /// <summary>
        /// Handles the progress changed event for Google Drive.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void OnUploadProgressChangedGDriveService(object sender, EventArgs e)
        {
            if (!(e is UploadProgressChangedEventArgs eventArgsNew))
            {
                return;
            }

            eventArgsNew.FileName = this.FileName;
            this.UploadProgressChangedGDrive(eventArgsNew);
        }

        /// <summary>
        /// Handles the upload successful event for mega.nz.
        /// </summary>
        /// <param name="e">The event args.</param>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        private void UploadProgressChangedMega(EventArgs e)
        {
            var handler = this.OnUploadProgressChangedMega;
            handler?.Invoke(this, e);
        }

        /// <summary>
        /// Handles the progress changed event for Google Drive.
        /// </summary>
        /// <param name="e">The event args.</param>
        private void UploadProgressChangedGDrive(EventArgs e)
        {
            var handler = this.OnUploadProgressChangedGDrive;
            handler?.Invoke(this, e);
        }

        /// <summary>
        /// Handles the upload successful event for Google Drive.
        /// </summary>
        /// <param name="e">The event args.</param>
        private void UploadSuccessfulGDrive(EventArgs e)
        {
            var handler = this.OnUploadSuccessfulGDrive;
            handler?.Invoke(this, e);
        }

        /// <summary>
        /// Gets the progress.
        /// </summary>
        /// <returns>The progress as <see cref="IProgress{T}"/> of <see cref="double"/>.</returns>
        private IProgress<double> GetProgress()
        {
            var progress = new Progress<double>();
            progress.ProgressChanged += this.OnUploadProgressChangedMegaInternal;
            return progress;
        }

        /// <summary>
        /// Handles the progress changed event for mega.nz internally.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="percentage">The percentage.</param>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        private void OnUploadProgressChangedMegaInternal(object sender, double percentage)
        {
            var eventArgs = new UploadProgressChangedEventArgsMega(percentage) { FileName = this.FileName };
            this.UploadProgressChangedMega(eventArgs);
        }

        /// <summary>
        /// Gets the drive service.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns>A <see cref="DriveService"/>.</returns>
        private DriveService GetDriveService(Account account)
        {
            return this.customGDriveService.GetDriveService(account.ClientId, account.Password, account.UserName);
        }

        /// <summary>
        /// Gets the possible accounts.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="Account"/>.</returns>
        private IEnumerable<Account> GetPossibleAccounts(string account)
        {
            return this.configuration.Accounts.Where(x => x.Name.ToLower().Equals(account));
        }

        /// <summary>
        /// Gets the root folder identifier.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns>The root folder identifier as <see cref="string"/>.</returns>
        private string GetRootFolderId(DriveService service)
        {
            return this.customGDriveService.GetRootFolderId(service);
        }

        /// <summary>
        /// Gets the used quota.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns>The used quota as <see cref="long"/>.</returns>
        private long GetQuotaUsed(DriveService service)
        {
            return this.customGDriveService.GetQuotaUsed(service);
        }

        /// <summary>
        /// Gets the total quota.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns>The total quota as <see cref="long"/>.</returns>
        private long GetQuotaTotal(DriveService service)
        {
            return this.customGDriveService.GetQuotaTotal(service);
        }
    }
}