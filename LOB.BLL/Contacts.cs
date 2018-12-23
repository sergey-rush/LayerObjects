using System;
using System.Collections.Generic;
using System.Threading;
using LOB.Core;
using LOB.Data;

namespace LOB.BLL
{
    public class Contacts : BaseData
    {
        public static List<Contact> GetPagedContacts(int pageIndex, int pageSize, int userId)
        {
            List<Contact> contacts = null;
            string key = "Contacts_GetPagedContacts_" + pageIndex + "_" + pageSize;

            if (Cache[key] != null)
            {
                contacts = (List<Contact>)Cache[key];
            }
            else
            {
                contacts = DataAccess.Contacts.GetPagedContacts(pageIndex, pageSize, userId);
                CacheData(key, contacts);
            }
            return contacts;
        }

        public static int CountContacts(int userId)
        {
            return DataAccess.Contacts.CountContacts(userId);
        }

        public static List<Contact> GetRandomContacts(int count, RoleType role)
        {
            List<Contact> contacts = DataAccess.Contacts.GetRandomContacts(count);
            return contacts;
        }

        public static Contact GetContactByContactId(int contactId)
        {
            Contact contact = null;
            string key = "Contacts_GetContactByContactId_" + contactId;

            if (Cache[key] != null)
            {
                contact = (Contact) Cache[key];
            }
            else
            {
                contact = DataAccess.Contacts.GetContactByContactId(contactId);
                CacheData(key, contact);
            }
            return contact;
        }
        
        public static Contact InsertContact(Contact contact)
        {
            RemoveFromCache("Contacts_");
            int contactId = DataAccess.Contacts.InsertContact(contact);
            contact = DataAccess.Contacts.GetContactByContactId(contactId);
            return contact;
        }

        public static bool UpdateContact(Contact contact)
        {
            RemoveFromCache("Contacts_");
            return DataAccess.Contacts.UpdateContact(contact);
        }

    }
}