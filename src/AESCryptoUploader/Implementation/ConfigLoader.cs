// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigLoader.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   A class to load the configuration.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AESCryptoUploader.Implementation;

/// <inheritdoc cref="IConfigLoader"/>
/// <summary>
/// A class to load the configuration.
/// </summary>
/// <seealso cref="IConfigLoader"/>
public class ConfigLoader : IConfigLoader
{
    /// <inheritdoc cref="IConfigLoader"/>
    /// <summary>
    /// Loads the configuration from XML files.
    /// </summary>
    /// <param name="fileName">The file name.</param>
    /// <returns>A new <see cref="Config"/> object.</returns>
    /// <seealso cref="IConfigLoader"/>
    public Config? LoadConfigFromXmlFile(string fileName)
    {
        var xDocument = XDocument.Load(fileName);
        return CreateObjectsFromString<Config?>(xDocument);
    }

    /// <summary>
    /// Creates an object from a <see cref="string"/>.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    /// <param name="xDocument">The X document.</param>
    /// <returns>A new object of type <see cref="T"/>.</returns>
    private static T? CreateObjectsFromString<T>(XDocument xDocument)
    {
        var xmlSerializer = new XmlSerializer(typeof(T));
        return (T?)xmlSerializer.Deserialize(new StringReader(xDocument.ToString()));
    }
}
