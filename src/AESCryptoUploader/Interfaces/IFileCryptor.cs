// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFileCryptor.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   An interface that encrypts files.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AESCryptoUploader.Interfaces
{
    using AESCryptoUploader.Models;

    /// <summary>
    /// An interface that encrypts files.
    /// </summary>
    public interface IFileCryptor
    {
        /// <summary>
        /// Encrypts the file.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="outputFolder">The output folder.</param>
        /// <returns>An <see cref="UploadItem"/>.</returns>
        UploadItem EncryptFile(string fileName, string outputFolder);
    }
}