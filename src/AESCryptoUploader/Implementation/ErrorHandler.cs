// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ErrorHandler.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   A class that handles the errors.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AESCryptoUploader.Implementation;

/// <inheritdoc cref="IErrorHandler"/>
/// <summary>
/// A class that handles the errors.
/// </summary>
/// <seealso cref="IErrorHandler"/>
public class ErrorHandler : IErrorHandler
{
    /// <inheritdoc cref="IErrorHandler"/>
    /// <summary>
    ///     Shows a message box with the <see cref="Exception" />.
    /// </summary>
    /// <param name="exception">The <see cref="Exception" /> that should be shown.</param>
    /// <seealso cref="IErrorHandler"/>
    public void Show(Exception exception)
    {
        MessageBox.Show(exception.StackTrace, exception.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
