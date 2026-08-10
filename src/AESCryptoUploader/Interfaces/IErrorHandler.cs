// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IErrorHandler.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   An interface that handles the errors.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AESCryptoUploader.Interfaces;

/// <summary>
///     An interface that handles the errors.
/// </summary>
public interface IErrorHandler
{
    /// <summary>
    ///     Gets or sets the current language used for the dialog title.
    /// </summary>
    ILanguage? CurrentLanguage { get; set; }

    /// <summary>
    ///     Shows a message box with the <see cref="Exception" />.
    /// </summary>
    /// <param name="exception">The <see cref="Exception" /> that should be shown.</param>
    void Show(Exception exception);
}
