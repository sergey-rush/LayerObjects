using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LOB.Crypto
{
    internal class RijndaelEncryptor : BaseEncryptor
    {
        private byte[] SecretKey { get; set; }
        private byte[] InitialisationVector { get; set; }

        internal RijndaelEncryptor()
        {
            Settings settings = Settings.Current;

            if (string.IsNullOrEmpty(settings.DecryptionKey))
            {
                throw new ArgumentNullException("DecryptionKey");
            }

            if (string.IsNullOrEmpty(settings.ValidationKey))
            {
                throw new ArgumentNullException("ValidationKey");
            }

            byte[] salt = Encoding.ASCII.GetBytes(settings.DecryptionKey);

            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(settings.ValidationKey, salt);
            SecretKey = pdb.GetBytes(32);
            InitialisationVector = pdb.GetBytes(16);
        }

        public override string Encrypt(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException("input");
            }
            
            byte[] encrypted;
            
            using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
            {
                rijndaelManaged.Key = SecretKey;
                rijndaelManaged.IV = InitialisationVector;
                ICryptoTransform encryptor = rijndaelManaged.CreateEncryptor(rijndaelManaged.Key, rijndaelManaged.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(input);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            //return Encoding.UTF8.GetString(encrypted, 0, encrypted.Length);
            return Convert.ToBase64String(encrypted);
        }

        public override string Decrypt(byte[] inputBytes)
        {
            if (inputBytes == null || inputBytes.Length <= 0)
            {
                throw new ArgumentNullException("inputBytes");
            }
            
            string output = null;
            
            using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
            {
                rijndaelManaged.Key = SecretKey;
                rijndaelManaged.IV = InitialisationVector;
                ICryptoTransform decryptor = rijndaelManaged.CreateDecryptor(rijndaelManaged.Key, rijndaelManaged.IV);
                
                using (MemoryStream msDecrypt = new MemoryStream(inputBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            output = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return output;
        }

        public override void Dispose()
        {
            
        }
    }
}