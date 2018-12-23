using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LOB.Core;

namespace LOB.Data
{
    public class UserProvider : UserManager
    {
        #region Users

        public override User GetUserByEmail(string email)
        {
            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Users_GetUserByEmail", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
                    cn.Open();
                    using (IDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (reader.Read())
                            return GetUserFromReader(reader);
                        else
                            return null;
                    }
                }

            }
        }

        public override User ValidateUser(string email, string password)
        {
            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Users_ValidateUser", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
                cmd.Parameters.Add("@Pass", SqlDbType.NVarChar).Value = password;
                cn.Open();
                using (IDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (reader.Read())
                        return GetUserFromReader(reader);
                    else
                        return null;
                }
            }
        }

        public override User GetUserByUserId(int userId)
        {
            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                cn.Open();

                using (SqlCommand cmd = new SqlCommand("Users_GetUserByUserId", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                    using (IDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (reader.Read())
                            return GetUserFromReader(reader);
                        else
                            return null;
                    }
                }

            }
        }

        public override List<User> GetRandomUsers(int count, RoleType role)
        {
            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                cn.Open();

                using (SqlCommand cmd = new SqlCommand("public.get_random_users", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_count", SqlDbType.Int).Value = count;
                    cmd.Parameters.Add("p_role_id", SqlDbType.Int).Value = role;
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        return GetUserCollectionFromReader(reader);
                    }
                }

            }
        }

        public override List<User> GetUsersByState(UserState state, RoleType role, int shopId)
        {
            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                cn.Open();

                using (SqlCommand cmd = new SqlCommand("public.get_users_by_state", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_shop_id", SqlDbType.Int).Value = shopId;
                    cmd.Parameters.Add("p_state_id", SqlDbType.Int).Value = state;
                    cmd.Parameters.Add("p_role_id", SqlDbType.Int).Value = role;
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        return GetUserCollectionFromReader(reader);
                    }
                }

            }
        }

        public override int CreateUser(User user)
        {
            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Users_CreateUser", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UserUid", SqlDbType.UniqueIdentifier).Value = user.UserUid;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = user.Email;
                cmd.Parameters.Add("@Pass", SqlDbType.NVarChar).Value = user.Pass;
                cmd.Parameters.Add("@RoleId", SqlDbType.Int).Value = (int) user.Role;
                cmd.Parameters.Add("@StateId", SqlDbType.Int).Value = user.UserState;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (int) cmd.Parameters["@Id"].Value;
            }
        }

        public override bool UpdateUserState(Guid userUid, UserState prevState, UserState newState)
        {
            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                cn.Open();

                using (SqlCommand cmd = new SqlCommand("public.update_user_state", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_user_uid", SqlDbType.UniqueIdentifier).Value = userUid;
                    cmd.Parameters.Add("p_prev_state_id", SqlDbType.Int).Value = prevState;
                    cmd.Parameters.Add("p_new_state_id", SqlDbType.Int).Value = newState;
                    cmd.Parameters.Add("p_row_count", SqlDbType.Int).Direction = ParameterDirection.Output;
                    ExecuteNonQuery(cmd);
                    int id = (int) cmd.Parameters["p_row_count"].Value;
                    return id == 1;
                }

            }
        }



        #endregion

        #region Roles

        public override List<Role> GetRoles()
        {
            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                cn.Open();

                using (SqlCommand cmd = new SqlCommand("Users_GetRoles", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        return GetRoleCollectionFromReader(reader);
                    }
                }

            }
        }

        #endregion
    }
}