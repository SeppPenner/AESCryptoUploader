using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Events;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Interfaces;
using log4net;
using Microsoft.Win32;
using GoogleFile = Google.Apis.Drive.v3.Data.File;
using IOFile = System.IO.File;

namespace Implementation
{
    public sealed class CustomGDriveService : ICustomGDriveService
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string[] _scopes =
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

        public long GetQuotaUsed(DriveService service)
        {
            var ag = new AboutResource.GetRequest(service) {Fields = "user,storageQuota"};
            var response = ag.Execute();
            if (response.StorageQuota.Usage.HasValue)
                return response.StorageQuota.Usage.Value;
            return -1;
        }

        public long GetQuotaTotal(DriveService service)
        {
            var ag = new AboutResource.GetRequest(service) {Fields = "user,storageQuota"};
            var response = ag.Execute();
            if (response.StorageQuota.Limit.HasValue)
                return response.StorageQuota.Limit.Value;
            return -1;
        }

        public string UploadToGDrive(DriveService service, string uploadFile, string parent)
        {
            try
            {
                var byteArray = IOFile.ReadAllBytes(uploadFile);
                var stream = new MemoryStream(byteArray);
                var request = service.Files.Create(GetBody(uploadFile, parent), stream, GetMimeType(uploadFile));
                request.ProgressChanged += UploadProgessChanged;
                request.ResponseReceived += UploadSuccessfull;
                request.Upload();
                var response = request.ResponseBody;
                CreatePermissionForFile(service, response.Id);
                var path = "https://drive.google.com/open?id=" + response.Id;
                stream.Dispose();
                return path;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return string.Empty;
            }
        }

        public string GetRootFolderId(DriveService service)
        {
            return service.Files.Get("root").FileId;
        }

        public DriveService GetDriveService(string clientId, string clientSecret, string userName)
        {
            var credential =
                GoogleWebAuthorizationBroker.AuthorizeAsync(GetClientSecrets(clientId, clientSecret), _scopes, userName,
                    CancellationToken.None, new FileDataStore(Application.ProductName)).Result;
            return GetService(credential);
        }

        public event EventHandler OnUploadProgessChanged;

        public event EventHandler OnUploadSuccessfull;

        private void UploadSuccessfull(GoogleFile file)
        {
            UploadSuccessfull(new UploadFinishedEventArgs(file.Name));
        }

        private void UploadProgessChanged(IUploadProgress progress)
        {
            UploadProgessChanged(new UploadProgessChangedEventArgs(progress.Status, progress.BytesSent));
        }

        private void UploadProgessChanged(EventArgs e)
        {
            var handler = OnUploadProgessChanged;
            handler?.Invoke(this, e);
        }

        private void UploadSuccessfull(EventArgs e)
        {
            var handler = OnUploadSuccessfull;
            handler?.Invoke(this, e);
        }

        private GoogleFile GetBody(string uploadFile, string parent)
        {
            var file = new GoogleFile
            {
                Name = Path.GetFileName(uploadFile),
                Description = uploadFile,
                MimeType = GetMimeType(uploadFile),
                Parents = new List<string> {parent}
            };
            return file;
        }

        private DriveService GetService(UserCredential credential)
        {
            return new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = Application.ProductName
            });
        }

        private ClientSecrets GetClientSecrets(string clientId, string clientSecret)
        {
            return new ClientSecrets {ClientId = clientId, ClientSecret = clientSecret};
        }

        private string GetMimeType(string fileName)
        {
            var mimeType = "application/unknown";
            var extension = Path.GetExtension(fileName);
            if (extension == null) return mimeType;
            var ext = extension.ToLower();
            var regKey = Registry.ClassesRoot.OpenSubKey(ext);
            return regKey?.GetValue("Content Type") == null ? mimeType : regKey.GetValue("Content Type").ToString();
        }

        private void CreatePermissionForFile(DriveService driveService, string fileId)
        {
            var everonePermission = new Permission
            {
                Type = "anyone",
                Role = "reader"
            };
            var request = driveService.Permissions.Create(everonePermission, fileId);
            request.Execute();
        }
    }
}