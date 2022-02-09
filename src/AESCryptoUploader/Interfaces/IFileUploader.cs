// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFileUploader.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   An interface to upload files.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AESCryptoUploader.Interfaces;

/// <summary>
/// An interface to upload files.
/// </summary>
public interface IFileUploader
{
    /// <summary>
    /// Handles the the upload progress changed event for Google Drive.
    /// </summary>
    event EventHandler? OnUploadProgressChangedGDrive;

    /// <summary>
    /// Handles the the upload successful event for Google Drive.
    /// </summary>
    event EventHandler? OnUploadSuccessfulGDrive;

    /// <summary>
    /// Handles the the upload progress changed event for mega.nz.
    /// </summary>
    event EventHandler? OnUploadProgressChangedMega;

    /// <summary>
    /// Gets the file name.
    /// </summary>
    string FileName { get; }

    /// <summary>
    /// Uploads the file to filehorst.de.
    /// </summary>
    /// <returns>The result as <see cref="string"/>.</returns>
    string UploadToFilehorst();

    /// <summary>
    /// Uploads the file to Google Drive.
    /// </summary>
    /// <returns>The result as <see cref="string"/>.</returns>
    string UploadToGDrive();

    /// <summary>
    /// Uploads the file to mega.nz.
    /// </summary>
    /// <returns>The result as <see cref="string"/>.</returns>
    Task<string> UploadToMega();
}
