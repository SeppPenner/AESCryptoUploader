// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Accounts.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The accounts class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AESCryptoUploader.Models;

/// <summary>
/// The accounts class.
/// </summary>
public class Config
{
    /// <summary>
    /// Gets or sets the accounts.
    /// </summary>
    public List<Account> Accounts { get; set; } = [];
}
