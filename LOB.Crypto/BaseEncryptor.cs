namespace LOB.Crypto
{
    public abstract class BaseEncryptor
    {
        public abstract string Encrypt(string input);

        public abstract string Decrypt(byte[] inputBytes);

        public abstract void Dispose();
    }
}