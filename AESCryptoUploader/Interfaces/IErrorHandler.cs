using System;
using Implementation;

namespace Interfaces
{
    /// <summary>
    ///     The <see cref="ErrorHandler" /> class for handling <see cref="Exception" />
    /// </summary>
    public interface IErrorHandler
    {
        /// <summary>
        ///     Shows a message box with the <see cref="Exception" />
        /// </summary>
        /// <param name="exception">The <see cref="Exception" /> that should be shown</param>
        void Show(Exception exception);
    }
}