using System;
using System.Security.Cryptography;
using System.Text;

namespace LOB.Crypto
{
    public class Md5Encryptor: BaseEncryptor
    {
        private byte[] ValidationKey { get; set; }
        
        internal Md5Encryptor()
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
            string output = null;
            byte[] inputBytes = Encoding.Unicode.GetBytes(input);
            byte[] hashBytes = null;
            using (HMACMD5 hash = new HMACMD5(ValidationKey))
            {
                hashBytes = hash.ComputeHash(inputBytes);
                output = Convert.ToBase64String(hashBytes);
            }
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
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}