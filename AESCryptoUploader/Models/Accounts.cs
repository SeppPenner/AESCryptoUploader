using System;
using System.Collections.Generic;

namespace Models
{
    [Serializable]
    public class Config
    {
        public List<Account> Accounts { get; set; }
    }
}