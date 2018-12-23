using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LOB.Crypto
{
    internal class DataEncryptor : BaseEncryptor
    {
        private DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        private MemoryStream memoryStream = new MemoryStream();
        private CryptoStream cryptoStream;
        private SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
        private string _cryptoKey;

        internal DataEncryptor()
        {
            Settings settings = Settings.Current;

            if (string.IsNullOrEmpty(settings.DecryptionKey))
            {
                throw new ArgumentNullException("DecryptionKey");
            }
            _cryptoKey = settings.DecryptionKey;
        }

        public override string Encrypt(string input)
        {
            byte[] encrypted;
            memoryStream.SetLength(0);
            des.Key = HashKey(_cryptoKey, des.KeySize/8);
            des.IV = HashKey(_cryptoKey, des.KeySize/8);
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(inputBytes, 0, inputBytes.Length);
            cryptoStream.FlushFinalBlock();
            encrypted = memoryStream.ToArray();
            //return Encoding.UTF8.GetString(encrypted, 0, encrypted.Length);
            return Convert.ToBase64String(encrypted);
        }

        public override string Decrypt(byte[] inputBytes)
        {
            try
            {
                des.Key = HashKey(_cryptoKey, des.KeySize/8);
                des.IV = HashKey(_cryptoKey, des.KeySize/8);
                cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(inputBytes, 0, inputBytes.Length);
                cryptoStream.FlushFinalBlock();
                Encoding encoding = Encoding.UTF8;
                return encoding.GetString(memoryStream.ToArray());
            }
            catch (CryptographicException exception)
            {
                throw new CryptographicException("inputBytes: {0}", exception.Message);
            }
        }


        /// <summary>
        /// Hash a key
        /// </summary>
        /// <param name="key">Key being hashed</param>
        /// <param name="length">Length of the output</param>
        /// <returns></returns>
        private byte[] HashKey(string key, int length)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] hash = sha1.ComputeHash(keyBytes);
            byte[] truncatedHash = new byte[length];
            Array.Copy(hash, 0, truncatedHash, 0, length);
            return truncatedHash;
        }

        public override void Dispose()
        {
            des.Clear();
            memoryStream.Dispose();
            sha1.Clear();
        }
    }
}