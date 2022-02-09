// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UploadItem.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The upload item class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AESCryptoUploader.Models;

/// <summary>
/// The upload item class.
/// </summary>
public class UploadItem
{
    /// <summary>
    /// Gets or sets the link for mega.nz.
    /// </summary>
    public string MegaLink { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the link for Google Drive.
    /// </summary>
    public string GDriveLink { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the link for filehorst.de.
    /// </summary>
    public string FilehorstLink { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the file name.
    /// </summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the new file name.
    /// </summary>
    public string NewFileName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the documentation file.
    /// </summary>
    public string DocumentationFile { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}
