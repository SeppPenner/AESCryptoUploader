using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CG.Web.MegaApiClient;
using Events;
using Google.Apis.Drive.v3;
using Interfaces;
using Models;

namespace Implementation
{
    public class FileUploader : IFileUploader
    {
        private readonly Config _config;
        private readonly ICustomGDriveService _customGDriveService = new CustomGDriveService();

        public FileUploader(Config config, string fileName)
        {
            _config = config;
            FileName = fileName;
            _customGDriveService.OnUploadProgessChanged += OnUploadProgessChangedGDriveService;
            _customGDriveService.OnUploadSuccessfull += OnUploadSuccessfullGDriveService;
        }

        private void OnUploadSuccessfullGDriveService(object sender, EventArgs eventArgs)
        {
            UploadSuccessfullGDrive(eventArgs);
        }

        private void OnUploadProgessChangedGDriveService(object sender, EventArgs eventArgs)
        {
            var eventArgsNew = eventArgs as UploadProgessChangedEventArgs;
            if (eventArgsNew == null) return;
            eventArgsNew.FileName = FileName;
            UploadProgessChangedGDrive(eventArgsNew);
        }

        public event EventHandler OnUploadProgessChangedGDrive;

        public event EventHandler OnUploadProgessChangedMega;

        public event EventHandler OnUploadSuccessfullGDrive;

        private void UploadProgessChangedMega(EventArgs e)
        {
            var handler = OnUploadProgessChangedMega;
            handler?.Invoke(this, e);
        }

        private void UploadProgessChangedGDrive(EventArgs e)
        {
            var handler = OnUploadProgessChangedGDrive;
            handler?.Invoke(this, e);
        }

        private void UploadSuccessfullGDrive(EventArgs e)
        {
            var handler = OnUploadSuccessfullGDrive;
            handler?.Invoke(this, e);
        }

        public string FileName { get; }

        public string UploadToFilehorst()
        {
            //TODO
            return string.Empty;
        }

        public string UploadToGDrive()
        {
            var possibleAccounts = GetPossibleAccount("gdrive");
            foreach (var account in possibleAccounts)
            {
                var service = GetDriveService(account);
                if (GetQuotaUsed(service) + GetFileSize(FileName) > GetQuotaTotal(service)) continue;
                var root = GetRootFolderId(service);
                return _customGDriveService.UploadToGDrive(service, FileName, root);
            }
            return string.Empty;
        }

        public async Task<string> UploadToMega()
        {
            var client = new MegaApiClient();
            var possibleAccounts = GetPossibleAccount("mega");
            foreach (var account in possibleAccounts)
            {
                LoginMega(client, account);
                var info = client.GetAccountInformation();
                if (info.UsedQuota + GetFileSize(FileName) > info.TotalQuota) continue;
                var root = GetRootFolderMega(client);
                var progress = GetProgress();
                var node = await client.UploadFileAsync(FileName, root, progress, null);
                var link = client.GetDownloadLink(node);
                client.Logout();
                return link.AbsoluteUri;
            }
            return string.Empty;
        }

        private IProgress<double> GetProgress()
        {
            var progress = new Progress<double>();
            progress.ProgressChanged += OnUploadProgessChangedMegaInternal;
            return progress;
        }

        private void OnUploadProgessChangedMegaInternal(object sender, double percentage)
        {
            var eventArgs = new UploadProgessChangedEventArgsMega(percentage) {FileName = FileName};
            UploadProgessChangedMega(eventArgs);
        }

        private long GetFileSize(string fileName)
        {
            return new FileInfo(fileName).Length;
        }

        private DriveService GetDriveService(Account account)
        {
            return _customGDriveService.GetDriveService(account.ClientId, account.Password, account.UserName);
        }

        private void LoginMega(IMegaApiClient client, Account account)
        {
            client.Login(account.UserName, account.Password);
        }

        private INode GetRootFolderMega(IMegaApiClient client)
        {
            return client.GetNodes().Single(n => n.Type == NodeType.Root);
        }

        private IEnumerable<Account> GetPossibleAccount(string account)
        {
            return _config.Accounts.Where(x => x.Name.ToLower().Equals(account));
        }

        private string GetRootFolderId(DriveService service)
        {
            return _customGDriveService.GetRootFolderId(service);
        }

        private long GetQuotaUsed(DriveService service)
        {
            return _customGDriveService.GetQuotaUsed(service);
        }

        private long GetQuotaTotal(DriveService service)
        {
            return _customGDriveService.GetQuotaTotal(service);
        }
    }
}