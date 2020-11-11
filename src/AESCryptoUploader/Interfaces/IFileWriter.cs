// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFileWriter.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   An interface to write files.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AESCryptoUploader.Interfaces
{
    using AESCryptoUploader.Models;

    /// <summary>
    /// An interface to write files.
    /// </summary>
    public interface IFileWriter
    {
        /// <summary>
        /// Writes the upload item to a file.
        /// </summary>
        /// <param name="item">The upload item.</param>
        void WriteToFile(UploadItem item);
    }
}