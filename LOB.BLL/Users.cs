using System;
using System.Collections.Generic;
using System.Threading;
using LOB.Core;
using LOB.Data;

namespace LOB.BLL
{
    public class Users : BaseData
    {
        #region Users

        public static User GetUserByEmail(string email)
        {
            User user = null;
            string key = "Users_GetUserByEmail_" + email;

            if (Cache[key] != null)
            {
                user = (User) Cache[key];
            }
            else
            {
                user = DataAccess.Users.GetUserByEmail(email);
                CacheData(key, user);
            }
            return user;
        }

        public static User ValidateUser(string email, string password)
        {
            User user = null;
            string key = "Users_ValidateUser_" + email + "_" + password;

            if (Cache[key] != null)
            {
                user = (User) Cache[key];
            }
            else
            {
                user = DataAccess.Users.ValidateUser(email, password);
                CacheData(key, user);
            }
            return user;
        }

        public static User GetUserByUserId(int userId)
        {
            User user = null;
            string key = "Users_GetUserByUserId_" + userId;

            if (Cache[key] != null)
            {
                user = (User) Cache[key];
            }
            else
            {
                user = DataAccess.Users.GetUserByUserId(userId);
                CacheData(key, user);
            }
            return user;
        }

        public static List<User> GetRandomUsers(int count, RoleType role)
        {
            List<User> users = DataAccess.Users.GetRandomUsers(count, role);
            return users;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state">Obligatory</param>
        /// <param name="role">Obligatory</param>
        /// <param name="shopId">Optional</param>
        /// <returns></returns>
        public static List<User> GetUsersByState(UserState state, RoleType role, int shopId)
        {
            List<User> users = DataAccess.Users.GetUsersByState(state, role, shopId);
            return users;
        }

        public static User CreateUser(User user)
        {
            RemoveFromCache("Users_");
            int userId = DataAccess.Users.CreateUser(user);
            user = DataAccess.Users.GetUserByUserId(userId);
            return user;
        }

        public static bool UpdateUserState(Guid userUid, UserState prevState, UserState newState)
        {
            RemoveFromCache("Users_");
            return DataAccess.Users.UpdateUserState(userUid, prevState, newState);
        }

        

        #endregion

        #region Roles

        public static List<Role> GetRoles()
        {
            List<Role> roles = null;
            string key = "Users_GetRoles_";

            if (Cache[key] != null)
            {
                roles = (List<Role>) Cache[key];
            }
            else
            {
                roles = DataAccess.Users.GetRoles();
                CacheData(key, roles);
            }
            return roles;
        }

        #endregion
    }
}