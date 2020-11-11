// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UploadItem.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The upload item class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AESCryptoUploader.Models
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The upload item class.
    /// </summary>
    public class UploadItem
    {
        /// <summary>
        /// Gets or sets the link for mega.nz.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public string MegaLink { get; set; }

        /// <summary>
        /// Gets or sets the link for Google Drive.
        /// </summary>
        public string GDriveLink { get; set; }

        /// <summary>
        /// Gets or sets the link for filehorst.de.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public string FilehorstLink { get; set; }

        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the new file name.
        /// </summary>
        public string NewFileName { get; set; }

        /// <summary>
        /// Gets or sets the documentation file.
        /// </summary>
        public string DocumentationFile { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }
    }
}