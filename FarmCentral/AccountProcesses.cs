using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace FarmCentral
{
    public class AccountProcesses
    {
        // Instantiate random number generator for the recovery code
        private readonly Random _random = new Random();

        // Set the permutationsfor encryption and decryption
        private const String strPermutation = "ouiveyxaqtd";
        private const Int32 bytePermutation1 = 0x19;
        private const Int32 bytePermutation2 = 0x59;
        private const Int32 bytePermutation3 = 0x17;
        private const Int32 bytePermutation4 = 0x41;

        /// <summary>
        /// Method used to encode the password provided
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        public string Encrypt(string password)
        {
            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(password)));
        }

        /// <summary>
        /// Method used to decode the password provided
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        public string Decrypt(string password)
        {
            password = password.Replace(" ", "+");

            int mod4 = password.Length % 4;
            if (mod4 > 0)
            {
                password += new string('=', 4 - mod4);
            }

            return Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(password)));
        }

        /// <summary>
        /// Method used to encrypt the encoded password
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        public byte[] Encrypt(byte[] encodedPassword)
        {
            PasswordDeriveBytes passbytes =
            new PasswordDeriveBytes(strPermutation,
            new byte[] 
            { 
                bytePermutation1,
                bytePermutation2,
                bytePermutation3,
                bytePermutation4
            });

            MemoryStream memstream = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = passbytes.GetBytes(aes.KeySize / 8);
            aes.IV = passbytes.GetBytes(aes.BlockSize / 8);

            CryptoStream cryptostream = new CryptoStream(memstream,
            aes.CreateEncryptor(), CryptoStreamMode.Write);
            cryptostream.Write(encodedPassword, 0, encodedPassword.Length);
            cryptostream.Close();
            return memstream.ToArray();
        }

        /// <summary>
        /// Method used to decrypt the decoded password
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        public byte[] Decrypt(byte[] decodedPassword)
        {
            PasswordDeriveBytes passbytes =
            new PasswordDeriveBytes(strPermutation,
            new byte[] 
            { 
                bytePermutation1,
                bytePermutation2,
                bytePermutation3,
                bytePermutation4
            });

            MemoryStream memstream = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = passbytes.GetBytes(aes.KeySize / 8);
            aes.IV = passbytes.GetBytes(aes.BlockSize / 8);

            CryptoStream cryptostream = new CryptoStream(memstream,
            aes.CreateDecryptor(), CryptoStreamMode.Write);
            cryptostream.Write(decodedPassword, 0, decodedPassword.Length);
            cryptostream.Close();
            return memstream.ToArray();
        }

        /// <summary>
        /// Method used to generate a random number within a given range
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        /// <summary>
        /// Generates a random string with a given size
        /// </summary>
        /// <param name="size"></param>
        /// <param name="lowerCase"></param>
        /// <returns></returns>
        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.

            // char is a single Unicode character
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        /// <summary>
        /// Generates a random recovery code
        /// 4-LowerCase Letters, 4-Numbers + 2-UpperCase Letters
        /// </summary>
        /// <returns></returns>
        public string RandomRecoveryCode()
        {
            var RKBuilder = new StringBuilder();

            // 4-Letters lower case
            RKBuilder.Append(RandomString(4, true));

            // 4-Digits between 1000 and 9999
            RKBuilder.Append(RandomNumber(1000, 9999));

            // 2-Letters upper case
            RKBuilder.Append(RandomString(2));
            return "RK-" + RKBuilder.ToString();
        }
    }
}
//----------------------------------------END OF FILE------------------------------------------------------
