using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LOB.Core;

namespace LOB.Data
{
    public class ContactProvider : ContactManager
    {
        public override List<Contact> GetPagedContacts(int pageIndex, int pageSize, int userId)
        {
            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Contacts_GetPagedContacts", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                cmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pageIndex;
                cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
                cn.Open();
                return GetContactCollectionFromReader(ExecuteReader(cmd));
            }
        }

        public override int CountContacts(int userId)
        {
            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Contacts_CountContacts", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                cn.Open();
                return (int)ExecuteScalar(cmd);
            }
        }

        public override List<Contact> GetRandomContacts(int count)
        {
            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Contacts_GetRandomContacts", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Count", SqlDbType.Int).Value = count;
                cn.Open();
                return GetContactCollectionFromReader(ExecuteReader(cmd));
            }
        }

        public override Contact GetContactByContactId(int contactId)
        {
            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Contacts_GetContactByContactId", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ContactId", SqlDbType.Int).Value = contactId;
                cn.Open();
                using (IDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (reader.Read())
                        return GetContactFromReader(reader);
                    else
                        return null;
                }
            }
        }

        public override int InsertContact(Contact contact)
        {
            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Contacts_InsertContact", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = contact.Name;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = contact.Phone;
                cmd.Parameters.Add("@StateId", SqlDbType.Int).Value = contact.ItemState;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (int) cmd.Parameters["@Id"].Value;
            }
        }

        public override bool UpdateContact(Contact contact)
        {
            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Contacts_UpdateContact", cn);
                cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = contact.Id;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = contact.Name;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = contact.Phone;
                cmd.Parameters.Add("@StateId", SqlDbType.Int).Value = contact.ItemState;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }

        public override bool DeleteContactByContactId(int contactId)
        {
            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Contacts_DeleteContactByContactId", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ContactId", SqlDbType.Int).Value = contactId;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }
    }
}