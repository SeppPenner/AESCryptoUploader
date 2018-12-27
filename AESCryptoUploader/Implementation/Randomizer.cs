using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Interfaces;

namespace Implementation
{
    public class Randomizer : IRandomizer
    {
        private readonly Random _random = new Random();

        public string GetRandomNewFileName()
        {
            return
                new string(
                    Enumerable.Repeat(
                            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_#$!%()[]{}+,.;@=^`~²³§ ",
                            64)
                        .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        public string GetRandomPassword()
        {
            var alg = SHA512.Create();
            alg.ComputeHash(Encoding.UTF8.GetBytes(DateTime.Now.ToLongDateString() + _random.Next(int.MaxValue)));
            return BitConverter.ToString(alg.Hash);
        }
    }
}