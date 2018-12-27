using System;
using Google.Apis.Drive.v3;

namespace Interfaces
{
    public interface ICustomGDriveService
    {
        long GetQuotaUsed(DriveService service);

        long GetQuotaTotal(DriveService service);

        string UploadToGDrive(DriveService service, string uploadFile, string parent);

        string GetRootFolderId(DriveService service);

        DriveService GetDriveService(string clientId, string clientSecret, string userName);

        event EventHandler OnUploadProgessChanged;

        event EventHandler OnUploadSuccessfull;
    }
}