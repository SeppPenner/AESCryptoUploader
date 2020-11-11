// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFileUploader.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   An interface to upload files.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AESCryptoUploader.Interfaces
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    /// <summary>
    /// An interface to upload files.
    /// </summary>
    public interface IFileUploader
    {
        /// <summary>
        /// Handles the the upload progress changed event for Google Drive.
        /// </summary>
        // ReSharper disable once EventNeverSubscribedTo.Global
        // ReSharper disable once UnusedMemberInSuper.Global
        event EventHandler OnUploadProgressChangedGDrive;

        /// <summary>
        /// Handles the the upload successful event for Google Drive.
        /// </summary>
        // ReSharper disable once EventNeverSubscribedTo.Global
        // ReSharper disable once UnusedMemberInSuper.Global
        event EventHandler OnUploadSuccessfulGDrive;

        /// <summary>
        /// Handles the the upload progress changed event for mega.nz.
        /// </summary>
        // ReSharper disable once EventNeverSubscribedTo.Global
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        // ReSharper disable once UnusedMemberInSuper.Global
        event EventHandler OnUploadProgressChangedMega;

        /// <summary>
        /// Gets the file name.
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// Uploads the file to filehorst.de.
        /// </summary>
        /// <returns>The result as <see cref="string"/>.</returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
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
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        Task<string> UploadToMega();
    }
}