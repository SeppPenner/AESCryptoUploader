// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Randomizer.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   A class to generate random data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AESCryptoUploader.Implementation;

/// <inheritdoc cref="IRandomizer"/>
/// <summary>
/// A class to generate random data.
/// </summary>
/// <seealso cref="IRandomizer"/>
public class Randomizer : IRandomizer
{
    /// <summary>
    /// The random generator.
    /// </summary>
    private readonly Random random = new();

    /// <inheritdoc cref="IRandomizer"/>
    /// <summary>
    /// Gets a random file name as <see cref="string"/>.
    /// </summary>
    /// <returns>The random file name as <see cref="string"/>.</returns>
    /// <seealso cref="IRandomizer"/>
    public string GetRandomNewFileName()
    {
        return
            new string(
                Enumerable.Repeat(
                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_#$!%()[]{}+,.;@=^`~²³§ ",
                        64)
                    .Select(s => s[this.random.Next(s.Length)]).ToArray());
    }

    /// <inheritdoc cref="IRandomizer"/>
    /// <summary>
    /// Gets a random password as <see cref="string"/>.
    /// </summary>
    /// <returns>The password name as <see cref="string"/>.</returns>
    /// <seealso cref="IRandomizer"/>
    public string GetRandomPassword()
    {
        var alg = SHA512.Create();
        alg.ComputeHash(Encoding.UTF8.GetBytes(DateTime.Now.ToLongDateString() + this.random.Next(int.MaxValue)));
        return BitConverter.ToString(alg.Hash ?? Array.Empty<byte>());
    }
}
