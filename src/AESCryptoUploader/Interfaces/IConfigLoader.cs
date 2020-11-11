// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IConfigLoader.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   An interface to load the configuration.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AESCryptoUploader.Interfaces
{
    using AESCryptoUploader.Models;

    /// <summary>
    /// An interface to load the configuration.
    /// </summary>
    public interface IConfigLoader
    {
        /// <summary>
        /// Loads the configuration from XML files.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <returns>A new <see cref="Config"/> object.</returns>
        Config LoadConfigFromXmlFile(string fileName);
    }
}