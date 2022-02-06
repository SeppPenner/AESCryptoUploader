// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomGDriveService.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   A class to handle the Google Drive service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AESCryptoUploader.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;

    using AESCryptoUploader.Events;
    using AESCryptoUploader.Interfaces;

    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Drive.v3;
    using Google.Apis.Drive.v3.Data;
    using Google.Apis.Http;
    using Google.Apis.Services;
    using Google.Apis.Upload;
    using Google.Apis.Util.Store;

    using Microsoft.Win32;

    using Serilog;

    using GoogleFile = Google.Apis.Drive.v3.Data.File;
    using IOFile = System.IO.File;

    /// <inheritdoc cref="ICustomGDriveService"/>
    /// <summary>
    /// A class to handle the Google Drive service.
    /// </summary>
    /// <seealso cref="ICustomGDriveService"/>
    public sealed class CustomGDriveService : ICustomGDriveService
    {
        /// <summary>
        /// The scopes.
        /// </summary>
        private readonly string[] scopes =
        {
            DriveService.Scope.Drive,
            DriveService.Scope.DriveAppdata,
            DriveService.Scope.DriveFile,
            DriveService.Scope.DriveMetadata,
            DriveService.Scope.DriveMetadataReadonly,
            DriveService.Scope.DrivePhotosReadonly,
            DriveService.Scope.DriveReadonly,
            DriveService.Scope.DriveScripts
        };

        /// <inheritdoc cref="ICustomGDriveService"/>
        /// <summary>
        /// Handles the upload progress changed event.
        /// </summary>
        /// <seealso cref="ICustomGDriveService"/>
        public event EventHandler OnUploadProgressChanged;

        /// <inheritdoc cref="ICustomGDriveService"/>
        /// <summary>
        /// Handles the upload successful event.
        /// </summary>
        /// <seealso cref="ICustomGDriveService"/>
        public event EventHandler OnUploadSuccessful;

        /// <inheritdoc cref="ICustomGDriveService"/>
        /// <summary>
        /// Gets the used quota.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns>The used quota as <see cref="long"/>.</returns>
        /// <seealso cref="ICustomGDriveService"/>
        public long GetQuotaUsed(DriveService service)
        {
            var ag = new AboutResource.GetRequest(service) { Fields = "user,storageQuota" };
            var response = ag.Execute();

            if (response.StorageQuota.Usage.HasValue)
            {
                return response.StorageQuota.Usage.Value;
            }

            return -1;
        }

        /// <inheritdoc cref="ICustomGDriveService"/>
        /// <summary>
        /// Gets the total quota.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns>The total quota as <see cref="long"/>.</returns>
        /// <seealso cref="ICustomGDriveService"/>
        public long GetQuotaTotal(DriveService service)
        {
            var ag = new AboutResource.GetRequest(service) { Fields = "user,storageQuota" };
            var response = ag.Execute();
            if (response.StorageQuota.Limit.HasValue)
            {
                return response.StorageQuota.Limit.Value;
            }

            return -1;
        }

        /// <inheritdoc cref="ICustomGDriveService"/>
        /// <summary>
        /// Uploads a file to Google Drive.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="uploadFile">The upload file.</param>
        /// <param name="parent">The parent.</param>
        /// <returns>The path as <see cref="string"/>.</returns>
        /// <seealso cref="ICustomGDriveService"/>
        public string UploadToGDrive(DriveService service, string uploadFile, string parent)
        {
            try
            {
                var byteArray = IOFile.ReadAllBytes(uploadFile);
                var stream = new MemoryStream(byteArray);
                var request = service.Files.Create(GetBody(uploadFile, parent), stream, GetMimeType(uploadFile));
                request.ProgressChanged += this.UploadProgressChanged;
                request.ResponseReceived += this.UploadSuccessful;
                request.Upload();
                var response = request.ResponseBody;
                CreatePermissionForFile(service, response.Id);
                var path = "https://drive.google.com/open?id=" + response.Id;
                stream.Dispose();
                return path;
            }
            catch (Exception ex)
            {
                Log.Error("An exception occurred: {@Exception}", ex);
                return string.Empty;
            }
        }

        /// <inheritdoc cref="ICustomGDriveService"/>
        /// <summary>
        /// Gets the folder root identifier.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns>The root folder identifier as <see cref="string"/>.</returns>
        /// <seealso cref="ICustomGDriveService"/>
        public string GetRootFolderId(DriveService service)
        {
            return service.Files.Get("root").FileId;
        }

        /// <inheritdoc cref="ICustomGDriveService"/>
        /// <summary>
        /// Gets the drive service.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="clientSecret">The client secret.</param>
        /// <param name="userName">The user name.</param>
        /// <returns>A new <see cref="DriveService"/>.</returns>
        /// <seealso cref="ICustomGDriveService"/>
        public DriveService GetDriveService(string clientId, string clientSecret, string userName)
        {
            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GetClientSecrets(clientId, clientSecret),
                this.scopes,
                userName,
                CancellationToken.None,
                new FileDataStore(Application.ProductName)).Result;
            return GetService(credential);
        }

        /// <summary>
        /// Gets the body.
        /// </summary>
        /// <param name="uploadFile">The upload file.</param>
        /// <param name="parent">The parent.</param>
        /// <returns>A new <see cref="GoogleFile"/>.</returns>
        private static GoogleFile GetBody(string uploadFile, string parent)
        {
            var file = new GoogleFile
            {
                Name = Path.GetFileName(uploadFile),
                Description = uploadFile,
                MimeType = GetMimeType(uploadFile),
                Parents = new List<string> { parent }
            };

            return file;
        }

        /// <summary>
        /// Gets the drive service.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <returns>The <see cref="DriveService"/>.</returns>
        private static DriveService GetService(IConfigurableHttpClientInitializer credentials)
        {
            return new DriveService(
                new BaseClientService.Initializer
                {
                    HttpClientInitializer = credentials,
                    ApplicationName = Application.ProductName
                });
        }

        /// <summary>
        /// Gets the client secrets.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="clientSecret">The client secret.</param>
        /// <returns>The <see cref="ClientSecrets"/>.</returns>
        private static ClientSecrets GetClientSecrets(string clientId, string clientSecret)
        {
            return new ClientSecrets { ClientId = clientId, ClientSecret = clientSecret };
        }

        /// <summary>
        /// Gets the MIME type.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <returns>The MIME type as <see cref="string"/>.</returns>
        private static string GetMimeType(string fileName)
        {
            const string MimeType = "application/unknown";
            var extension = Path.GetExtension(fileName);

            if (extension == null)
            {
                return MimeType;
            }

            var ext = extension.ToLower();
            var regKey = Registry.ClassesRoot.OpenSubKey(ext);
            return regKey?.GetValue("Content Type")?.ToString() ?? MimeType;
        }

        /// <summary>
        /// Creates the permission for the file identifier.
        /// </summary>
        /// <param name="driveService">The drive service.</param>
        /// <param name="fileId">The file identifier.</param>
        private static void CreatePermissionForFile(DriveService driveService, string fileId)
        {
            var anyonePermission = new Permission
            {
                Type = "anyone",
                Role = "reader"
            };

            var request = driveService.Permissions.Create(anyonePermission, fileId);
            request.Execute();
        }

        /// <summary>
        /// Handles the upload successful event.
        /// </summary>
        /// <param name="file">The Google file.</param>
        private void UploadSuccessful(GoogleFile file)
        {
            this.UploadSuccessful(new UploadFinishedEventArgs(file.Name));
        }

        /// <summary>
        /// Handles the upload progress changed event.
        /// </summary>
        /// <param name="progress">The progress.</param>
        private void UploadProgressChanged(IUploadProgress progress)
        {
            this.UploadProgressChanged(new UploadProgressChangedEventArgs(progress.Status, progress.BytesSent));
        }

        /// <summary>
        /// Handles the progress changed event.
        /// </summary>
        /// <param name="e">The event args.</param>
        private void UploadProgressChanged(EventArgs e)
        {
            var handler = this.OnUploadProgressChanged;
            handler?.Invoke(this, e);
        }

        /// <summary>
        /// Handles the upload successful event.
        /// </summary>
        /// <param name="e">The event args.</param>
        private void UploadSuccessful(EventArgs e)
        {
            var handler = this.OnUploadSuccessful;
            handler?.Invoke(this, e);
        }
    }
}