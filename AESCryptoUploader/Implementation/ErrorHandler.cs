using System;
using System.Windows.Forms;
using Interfaces;

namespace Implementation
{
    /// <summary>
    ///     <inheritdoc />
    /// </summary>
    public class ErrorHandler : IErrorHandler
    {
        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public void Show(Exception exception)
        {
            MessageBox.Show(exception.StackTrace, exception.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}