// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICustomGDriveService.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   An interface to handle the Google Drive service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AESCryptoUploader.Interfaces;

/// <summary>
/// An interface to handle the Google Drive service.
/// </summary>
public interface ICustomGDriveService
{
    /// <summary>
    /// Handles the upload progress changed event.
    /// </summary>
    event EventHandler? OnUploadProgressChanged;

    /// <summary>
    /// Handles the upload successful event.
    /// </summary>
    event EventHandler? OnUploadSuccessful;

    /// <summary>
    /// Gets the used quota.
    /// </summary>
    /// <param name="service">The service.</param>
    /// <returns>The used quota as <see cref="long"/>.</returns>
    long GetQuotaUsed(DriveService service);

    /// <summary>
    /// Gets the total quota.
    /// </summary>
    /// <param name="service">The service.</param>
    /// <returns>The total quota as <see cref="long"/>.</returns>
    long GetQuotaTotal(DriveService service);

    /// <summary>
    /// Uploads a file to Google Drive.
    /// </summary>
    /// <param name="service">The service.</param>
    /// <param name="uploadFile">The upload file.</param>
    /// <param name="parent">The parent.</param>
    /// <returns>The path as <see cref="string"/>.</returns>
    string UploadToGDrive(DriveService service, string uploadFile, string parent);

    /// <summary>
    /// Gets the folder root identifier.
    /// </summary>
    /// <param name="service">The service.</param>
    /// <returns>The root folder identifier as <see cref="string"/>.</returns>
    string GetRootFolderId(DriveService service);

    /// <summary>
    /// Gets the drive service.
    /// </summary>
    /// <param name="clientId">The client identifier.</param>
    /// <param name="clientSecret">The client secret.</param>
    /// <param name="userName">The user name.</param>
    /// <returns>A new <see cref="DriveService"/>.</returns>
    DriveService GetDriveService(string clientId, string clientSecret, string userName);
}
