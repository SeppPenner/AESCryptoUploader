using System;

namespace Events
{
    /// <summary>
    ///     The <see cref="UploadFinishedEventArgs" /> class
    /// </summary>
    public class UploadFinishedEventArgs : EventArgs
    {
        /// <summary>
        ///     Constructor with status
        /// </summary>
        /// <param name="fileName">The uploaded file name</param>
        public UploadFinishedEventArgs(string fileName)
        {
            FileName = fileName;
        }

        private string FileName { get; }

        /// <summary>
        ///     Gets the <see cref="UploadFinishedEventArgs" /> status
        /// </summary>
        /// <returns>The uploaded file name of the <see cref="UploadFinishedEventArgs" /></returns>
        public string GetStatus()
        {
            return FileName;
        }
    }
}