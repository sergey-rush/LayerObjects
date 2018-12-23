using System;
using System.Security.Cryptography;
using System.Text;

namespace LOB.Crypto
{
    public class Sha256Encryptor: BaseEncryptor
    {
        private byte[] ValidationKey { get; set; }
        
        internal Sha256Encryptor()
        {
            Settings settings = Settings.Current;

            if (string.IsNullOrEmpty(settings.ValidationKey))
            {
                throw new ArgumentNullException("ValidationKey");
            }

            ValidationKey = HexToByte(settings.ValidationKey);
        }

        public override string Encrypt(string input)
        {
            HMACSHA256 hash = new HMACSHA256();
            hash.Key = ValidationKey;
            byte[] inputBytes = Encoding.Unicode.GetBytes(input);
            byte[] hashBytes = hash.ComputeHash(inputBytes);
            string output = Convert.ToBase64String(hashBytes);
            return output;
        }

        private byte[] HexToByte(string hexString)
        {
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        public override string Decrypt(byte[] inputBytes)
        {
            throw new System.NotImplementedException();
        }

        public override void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}