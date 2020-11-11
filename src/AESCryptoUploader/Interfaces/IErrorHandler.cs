// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IErrorHandler.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   An interface that handles the errors.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AESCryptoUploader.Interfaces
{
    using System;

    /// <summary>
    ///     An interface that handles the errors.
    /// </summary>
    public interface IErrorHandler
    {
        /// <summary>
        ///     Shows a message box with the <see cref="Exception" />.
        /// </summary>
        /// <param name="exception">The <see cref="Exception" /> that should be shown.</param>
        void Show(Exception exception);
    }
}