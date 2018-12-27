using Models;

namespace Interfaces
{
    public interface IFileCryptor
    {
        UploadItem EncryptFile(string fileName, string outputFolder);
    }
}