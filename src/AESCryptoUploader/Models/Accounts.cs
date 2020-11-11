// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Accounts.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The accounts class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------


namespace AESCryptoUploader.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The accounts class.
    /// </summary>
    [Serializable]
    public class Config
    {
        /// <summary>
        /// Gets or sets the accounts.
        /// </summary>
        public List<Account> Accounts { get; set; }
    }
}