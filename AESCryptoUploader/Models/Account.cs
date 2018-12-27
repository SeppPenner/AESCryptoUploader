using System;

namespace Models
{
    [Serializable]
    public class Account
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string ClientId { get; set; }
    }
}