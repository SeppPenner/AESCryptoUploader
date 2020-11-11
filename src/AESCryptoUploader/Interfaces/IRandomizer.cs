// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRandomizer.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   An interface to generate random data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AESCryptoUploader.Interfaces
{
    /// <summary>
    /// An interface to generate random data.
    /// </summary>
    public interface IRandomizer
    {
        /// <summary>
        /// Gets a random file name as <see cref="string"/>.
        /// </summary>
        /// <returns>The random file name as <see cref="string"/>.</returns>
        string GetRandomNewFileName();

        /// <summary>
        /// Gets a random password as <see cref="string"/>.
        /// </summary>
        /// <returns>The password name as <see cref="string"/>.</returns>
        string GetRandomPassword();
    }
}