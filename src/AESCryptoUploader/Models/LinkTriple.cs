// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinkTriple.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The link triple class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AESCryptoUploader.Models;

/// <summary>
/// The link triple class.
/// </summary>
public class LinkTriple
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
}
