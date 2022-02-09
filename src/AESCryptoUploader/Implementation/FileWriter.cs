// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileWriter.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   A class to write files.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AESCryptoUploader.Implementation;

/// <inheritdoc cref="IFileWriter"/>
/// <summary>
/// A class to write files.
/// </summary>
/// <seealso cref="IFileWriter"/>
public class FileWriter : IFileWriter
{
    /// <inheritdoc cref="IFileWriter"/>
    /// <summary>
    /// Writes the upload item to a file.
    /// </summary>
    /// <param name="item">The upload item.</param>
    /// <seealso cref="IFileWriter"/>
    public void WriteToFile(UploadItem item)
    {
        WriteToFile(item.DocumentationFile, GenerateLines(item));
    }

    /// <summary>
    /// Generates the lines.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="string"/>.</returns>
    private static IEnumerable<string> GenerateLines(UploadItem item)
    {
        return new List<string>
            {
                Path.GetFileNameWithoutExtension(item.FileName),
                Path.GetFileNameWithoutExtension(item.NewFileName),
                "[color=#FF0000]Passwort:[/color] [quote]" + item.Password + "[/quote]",
                "[color=#FF0000]Links:[/color]",
                "[list]",
                "[*]Download: [url]" + item.MegaLink + "[/url]",
                "[*]Backup: [url]" + item.GDriveLink + "[/url]",
                "[*]Backup2: [url]" + item.FilehorstLink + "[/url]",
                "[/list]"
            };
    }

    /// <summary>
    /// Writes to a file.
    /// </summary>
    /// <param name="outputFile">The output file.</param>
    /// <param name="lines">The lines.</param>
    private static void WriteToFile(string outputFile, IEnumerable<string> lines)
    {
        using var writer = new StreamWriter(outputFile);

        foreach (var line in lines)
        {
            writer.WriteLine(line);
        }
    }
}
