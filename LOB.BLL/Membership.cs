using System;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using LOB.Core;
using LOB.Crypto;

namespace LOB.BLL
{
    public class Membership
    {
        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        /// <returns>User</returns>
        public static User CreateUser(string email, string password, RoleType role)
        {
            BaseEncryptor cryptoProvider = CryptoManager.GetEncryptor(Encryptor.Md5);
            User user = new User();
            user.UserUid = Guid.NewGuid();
            user.Email = email;
            user.Role = role;
            user.UserState = UserState.Online;
            user.AccountState = AccountState.Enabled;
            user.Pass = cryptoProvider.Encrypt(password);
            user = Users.CreateUser(user);
            return user;
        }

        

        public static bool ValidateUser(string username, string password)
        {
            bool result = false;
            BaseEncryptor cryptoProvider = CryptoManager.GetEncryptor(Encryptor.Md5);
            string encryptedPassword = cryptoProvider.Encrypt(password);
            User user = Users.ValidateUser(username, encryptedPassword);
            if (user != null)
            {
                result = true;
            }
            return result;
        }
    }
}