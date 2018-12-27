using System;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IFileUploader
    {
        string FileName { get; }

        string UploadToFilehorst();
        string UploadToGDrive();
        Task<string> UploadToMega();

        // ReSharper disable once EventNeverSubscribedTo.Global
        event EventHandler OnUploadProgessChangedGDrive;

        // ReSharper disable once EventNeverSubscribedTo.Global
        event EventHandler OnUploadSuccessfullGDrive;

        // ReSharper disable once EventNeverSubscribedTo.Global
        event EventHandler OnUploadProgessChangedMega;
    }
}