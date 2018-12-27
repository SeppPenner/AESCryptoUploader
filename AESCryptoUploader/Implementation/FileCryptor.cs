using System;
using System.IO;
using Interfaces;
using Models;
using AESCrypt = SharpAESCrypt.SharpAESCrypt;

namespace Implementation
{
    public class FileCryptor : IFileCryptor
    {
        private readonly IRandomizer _randomizer = new Randomizer();

        public UploadItem EncryptFile(string fileName, string outputFolder)
        {
            CheckFileName(fileName);
            var password = _randomizer.GetRandomPassword();
            var newFileName = GetNewFileName(fileName, outputFolder);
            AESCrypt.Encrypt(password, fileName, newFileName);
            var docuFile = GetDocuFileName(fileName, outputFolder);
            return GetNewUploadItem(fileName, docuFile, newFileName, password);
        }

        private UploadItem GetNewUploadItem(string fileName, string docuFile, string newFileName, string password)
        {
            return new UploadItem
            {
                FilehorstLink = "",
                GDriveLink = "",
                MegaLink = "",
                FileName = fileName,
                DocuFile = docuFile,
                NewFileName = newFileName,
                Password = password
            };
        }

        private string GetDocuFileName(string fileName, string outputFolder)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            return Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(fileName)) + ".txt";
        }

        private void CheckFileName(string fileName)
        {
            if (fileName == null) throw new ArgumentNullException(nameof(fileName));
        }

        private string GetNewFileName(string fileName, string outputFolder)
        {
            return outputFolder + "\\" + GetNewRandomFileName() + Path.GetExtension(fileName) + ".aes";
        }

        private string GetNewRandomFileName()
        {
            return _randomizer.GetRandomNewFileName();
        }
    }
}