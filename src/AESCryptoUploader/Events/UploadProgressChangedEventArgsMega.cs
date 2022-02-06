// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UploadProgressChangedEventArgsMega.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The <see cref="UploadProgressChangedEventArgsMega" /> class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AESCryptoUploader.Events
{
    using System;

    /// <summary>
    ///     The <see cref="UploadProgressChangedEventArgsMega" /> class.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public class UploadProgressChangedEventArgsMega : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UploadProgressChangedEventArgsMega"/> class.
        /// </summary>
        /// <param name="percentage">The percentage of bytes already sent.</param>
        public UploadProgressChangedEventArgsMega(double percentage)
        {
            this.Percentage = percentage;
        }

        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// Gets the percentage value.
        /// </summary>
        private double Percentage { get; }

        /// <summary>
        ///     Gets the <see cref="UploadProgressChangedEventArgs" /> sent bytes
        /// </summary>
        /// <returns>The sent bytes of the <see cref="UploadProgressChangedEventArgs" /></returns>
        // ReSharper disable once UnusedMember.Global
        public double GetPercentage()
        {
            return this.Percentage;
        }
    }
}