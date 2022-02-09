// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileCryptor.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   A class that encrypts files.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AESCryptoUploader.Implementation;

/// <inheritdoc cref="IFileCryptor"/>
/// <summary>
/// A class that encrypts files.
/// </summary>
/// <seealso cref="IFileCryptor"/>
public class FileCryptor : IFileCryptor
{
    /// <summary>
    /// The randomizer.
    /// </summary>
    private readonly IRandomizer randomizer = new Randomizer();

    /// <inheritdoc cref="IFileCryptor"/>
    /// <summary>
    /// Encrypts the file.
    /// </summary>
    /// <param name="fileName">The file name.</param>
    /// <param name="outputFolder">The output folder.</param>
    /// <returns>An <see cref="UploadItem"/>.</returns>
    /// <seealso cref="IFileCryptor"/>
    public UploadItem EncryptFile(string fileName, string outputFolder)
    {
        CheckFileName(fileName);
        var password = this.randomizer.GetRandomPassword();
        var newFileName = this.GetNewFileName(fileName, outputFolder);
        AESCrypt.Encrypt(password, fileName, newFileName);
        var documentationFile = GetDocumentationFileName(fileName, outputFolder);
        return GetNewUploadItem(fileName, documentationFile, newFileName, password);
    }

    /// <summary>
    /// Gets the upload item.
    /// </summary>
    /// <param name="fileName">The file name.</param>
    /// <param name="documentationFile">The documentation file.</param>
    /// <param name="newFileName">The new file name.</param>
    /// <param name="password">The password.</param>
    /// <returns>An <see cref="UploadItem"/>.</returns>
    private static UploadItem GetNewUploadItem(string fileName, string documentationFile, string newFileName, string password)
    {
        return new UploadItem
        {
            FilehorstLink = string.Empty,
            GDriveLink = string.Empty,
            MegaLink = string.Empty,
            FileName = fileName,
            DocumentationFile = documentationFile,
            NewFileName = newFileName,
            Password = password
        };
    }

    /// <summary>
    /// Gets the documentation file name.
    /// </summary>
    /// <param name="fileName">The file name.</param>
    /// <param name="outputFolder">The output folder.</param>
    /// <returns>The documentation file name as <see cref="string"/>.</returns>
    private static string GetDocumentationFileName(string fileName, string outputFolder)
    {
        return Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(fileName)) + ".txt";
    }

    /// <summary>
    /// Checks the file name.
    /// </summary>
    /// <param name="fileName">The file name.</param>
    private static void CheckFileName(string fileName)
    {
        if (fileName == null)
        {
            throw new ArgumentNullException(nameof(fileName));
        }
    }

    /// <summary>
    /// Gets the new file name.
    /// </summary>
    /// <param name="fileName">The file name.</param>
    /// <param name="outputFolder">The output folder.</param>
    /// <returns>The new file name as <see cref="string"/>.</returns>
    private string GetNewFileName(string fileName, string outputFolder)
    {
        return outputFolder + "\\" + this.GetNewRandomFileName() + Path.GetExtension(fileName) + ".aes";
    }

    /// <summary>
    /// Gets a new random file name.
    /// </summary>
    /// <returns>The new random file name as <see cref="string"/>.</returns>
    private string GetNewRandomFileName()
    {
        return this.randomizer.GetRandomNewFileName();
    }
}
