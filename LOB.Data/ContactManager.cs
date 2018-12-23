using System;
using System.Collections.Generic;
using System.Data;
using LOB.Core;

namespace LOB.Data
{
    public abstract class ContactManager: DataAccess
    {
        private static ContactManager _instance;

        public static ContactManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ContactProvider();
                }
                return _instance;
            }
        }

        public abstract List<Contact> GetPagedContacts(int pageIndex, int pageSize, int userId);
        public abstract int CountContacts(int userId);
        public abstract List<Contact> GetRandomContacts(int count);
        public abstract Contact GetContactByContactId(int contact);
        public abstract int InsertContact(Contact contact);
        public abstract bool UpdateContact(Contact contact);
        public abstract bool DeleteContactByContactId(int contactId);
        protected virtual Contact GetContactFromReader(IDataReader reader)
        {//Id, Name, Phone, Email, StateId, Created
            Contact contact = new Contact()
            {
                Id = (int)reader["Id"],
                ItemState = (ItemState)reader["StateId"],
                Created = (DateTime)reader["Created"]
            };

            if (reader["Name"] != DBNull.Value)
            {
                contact.Name = reader["Name"].ToString();
            }

            if (reader["Phone"] != DBNull.Value)
            {
                contact.Phone = reader["Phone"].ToString();
            }

            if (reader["Email"] != DBNull.Value)
            {
                contact.Name = reader["Email"].ToString();
            }

            if (reader["Name"] != DBNull.Value)
            {
                contact.Name = reader["Name"].ToString();
            }

            return contact;
        }
       
        protected virtual List<Contact> GetContactCollectionFromReader(IDataReader reader)
        {
            List<Contact> items = new List<Contact>();
            while (reader.Read())
                items.Add(GetContactFromReader(reader));
            return items;
        }

       
    }
}