// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UiThreadInvokeClass.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   A class to invoke UI events from background processes.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AESCryptoUploader.UiThreadInvoke
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// A class to invoke UI events from background processes.
    /// </summary>
    public static class UiThreadInvokeClass
    {
        /// <summary>
        /// Handles the UI thread invocation.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="code">The code.</param>
        public static void UiThreadInvoke(this Control control, Action code)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(code);
                return;
            }

            code.Invoke();
        }
    }
}