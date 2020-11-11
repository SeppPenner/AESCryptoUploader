// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinkTriple.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The link triple class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AESCryptoUploader.Models
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The link triple class.
    /// </summary>
    public class LinkTriple
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
    }
}