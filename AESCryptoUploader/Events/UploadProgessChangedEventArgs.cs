using System;
using Google.Apis.Upload;

namespace Events
{
    /// <summary>
    ///     The <see cref="UploadProgessChangedEventArgs" /> class
    /// </summary>
    public class UploadProgessChangedEventArgs : EventArgs
    {
        private UploadStatus Status { get; }

        private long BytesSent { get; }

        public string FileName { get; set; }

        /// <summary>
        ///     Constructor with status and amount of sent bytes
        /// </summary>
        /// <param name="status">The upload status</param>
        /// <param name="bytesSent">The bytes already sent</param>
        public UploadProgessChangedEventArgs(UploadStatus status, long bytesSent)
        {
            Status = status;
            BytesSent = bytesSent;
        }

        /// <summary>
        ///     Gets the <see cref="UploadProgessChangedEventArgs" /> status
        /// </summary>
        /// <returns>The status of the <see cref="UploadProgessChangedEventArgs" /></returns>
        // ReSharper disable once UnusedMember.Global
        public UploadStatus GetStatus()
        {
            return Status;
        }

        /// <summary>
        ///     Gets the <see cref="UploadProgessChangedEventArgs" /> sent bytes
        /// </summary>
        /// <returns>The sent bytes of the <see cref="UploadProgessChangedEventArgs" /></returns>
        public long GetSentBytes()
        {
            return BytesSent;
        }
    }
}