using System;

namespace Events
{
    /// <summary>
    ///     The <see cref="UploadProgessChangedEventArgsMega" /> class
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public class UploadProgessChangedEventArgsMega : EventArgs
    {

        private double Percentage { get; }

        // ReSharper disable once UnusedMember.Global
        public string FileName { get; set; }

        /// <summary>
        ///     Constructor with status and amount of sent bytes
        /// </summary>
        /// <param name="percentage">The percentage of bytes already sent</param>
        public UploadProgessChangedEventArgsMega(double percentage)
        {
            Percentage = percentage;
        }

        /// <summary>
        ///     Gets the <see cref="UploadProgessChangedEventArgs" /> sent bytes
        /// </summary>
        /// <returns>The sent bytes of the <see cref="UploadProgessChangedEventArgs" /></returns>
        // ReSharper disable once UnusedMember.Global
        public double GetPercentage()
        {
            return Percentage;
        }
    }
}