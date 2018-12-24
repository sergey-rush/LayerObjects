using System;
using System.Collections.Generic;
using System.Data;
using LOB.Core;

namespace LOB.Data
{
    public abstract class UserManager: DataAccess
    {
        private static UserManager _instance;

        public static UserManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserProvider();
                }
                return _instance;
            }
        }

        #region Users

        public abstract User GetUserByEmail(string email);
        public abstract User ValidateUser(string email, string password);
        public abstract User GetUserByUserId(int userId);
        public abstract List<User> GetRandomUsers(int count, RoleType role);
        public abstract List<User> GetUsersByState(UserState state, RoleType role, int shopId);
        public abstract int CreateUser(User user);
        public abstract bool UpdateUserState(Guid userUid, UserState prevState, UserState newState);
        protected virtual User GetUserFromReader(IDataReader reader)
        {
            User user = new User()
            {
                Id = (int)reader["Id"],
                UserUid = (Guid)reader["UserUid"],
                Email = reader["Email"].ToString(),
                Pass = reader["Pass"].ToString(),
                Role = (RoleType)reader["RoleId"],
                UserState = (UserState)reader["StateId"],
                FailedCount = (int)reader["FailedCount"],
                Created = (DateTime)reader["Created"]
            };

            if (reader["Name"] != DBNull.Value)
            {
                user.Name = reader["Name"].ToString();
            }

            if (reader["Phone"] != DBNull.Value)
            {
                user.Phone = reader["Phone"].ToString();
            }

            if (reader["LastLogin"] != DBNull.Value)
            {
                user.LastLoginDate = (DateTime) reader["LastLogin"];
            }
            
            if (reader["Updated"] != DBNull.Value)
            {
                user.Updated = (DateTime)reader["Updated"];
            }

            return user;
        }
       
        protected virtual List<User> GetUserCollectionFromReader(IDataReader reader)
        {
            List<User> items = new List<User>();
            while (reader.Read())
                items.Add(GetUserFromReader(reader));
            return items;
        }

        #endregion

        #region Roles

        public abstract List<Role> GetRoles();

        protected virtual Role GetRoleFromReader(IDataReader reader)
        {
            Role user = new Role()
            {
                //Id = (int)reader["Id"],
                Name = reader["Name"].ToString(),
                Alias = reader["Alias"].ToString()
            };

            return user;
        }

        protected virtual List<Role> GetRoleCollectionFromReader(IDataReader reader)
        {
            List<Role> items = new List<Role>();
            while (reader.Read())
                items.Add(GetRoleFromReader(reader));
            return items;
        }


        #endregion

    }
}