namespace LOB.Crypto
{
    public class CryptoManager
    {
        private static DataEncryptor dataEncryptor;
        private static RijndaelEncryptor rijndaelEncryptor;
        private static Sha256Encryptor hashEncryptor;

        public static BaseEncryptor GetEncryptor(Encryptor encryptor)
        {
            BaseEncryptor cryptoProvider = null;

            if (encryptor == Encryptor.Aes)
            {
                if (rijndaelEncryptor == null)
                {
                    rijndaelEncryptor = new RijndaelEncryptor();
                }
                cryptoProvider = rijndaelEncryptor;
            }

            if (encryptor == Encryptor.Des)
            {
                if (dataEncryptor == null)
                {
                    dataEncryptor = new DataEncryptor();
                }
                cryptoProvider = dataEncryptor;
            }

            if (encryptor == Encryptor.Sha256)
            {
                if (hashEncryptor == null)
                {
                    hashEncryptor = new Sha256Encryptor();
                }
                cryptoProvider = hashEncryptor;
            }

            if (encryptor == Encryptor.Md5)
            {
                if (hashEncryptor == null)
                {
                    hashEncryptor = new Sha256Encryptor();
                }
                cryptoProvider = hashEncryptor;
            }

            return cryptoProvider;
        }
    }
}