using System;
using System.Security.Cryptography;

namespace ClassLibrary
{
    public class SaltGenerator
    {
        /// <summary>
        /// Creates a new instance of RNG crypto and generates a byte array according to desired size
        /// and returns the base 64 string for hashing via HashGenerator
        /// </summary>
        /// <param name="size">Adjust byte array salt size (pref. 16 bit)</param>
        /// <returns></returns>
        public static string GenerateSalt(int size)
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            Console.WriteLine(Convert.ToBase64String(buff));
            return Convert.ToBase64String(buff);
        }
    }
}
