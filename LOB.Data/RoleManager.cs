using System;
using System.Collections.Generic;
using System.Data;
using LOB.Core;

namespace LOB.Data
{
    public abstract class RoleManager: DataAccess
    {
        private static RoleManager _instance;

        public static RoleManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RoleProvider();
                }
                return _instance;
            }
        }

        //public abstract List<RoleConditionsElement> GetRoleConditionsElementsByElementId(Guid elementId);
        public abstract int CountRoles(string query);
        //public abstract List<RoleElementType> GetRoleElementTypesByElementTypeId(Guid elementTypeId);
        public abstract Role GetRoleByRoleId(Guid role);
        public abstract Guid InsertRole(Role role);
        public abstract bool UpdateRole(Role role);
        public abstract bool DeleteRoleElementTypeByElementTypeId(Guid elementTypeId);
        public abstract bool DeleteFilterByRoleElementTypeId(Guid roleElementTypeId);
        public abstract bool DeleteFilterByAttributeId(Guid attributeId);
        public abstract bool DeleteRoleConditionsElementsByElementId(Guid elementId);

        protected virtual Role GetRoleFromReader(IDataReader reader)
        {
            Role role = new Role()
            {
                Id = (Guid)reader["Id"],
                Name = reader["Name"].ToString()
            };
            return role;
        }

        protected virtual List<Role> GetRoleElementTypeCollectionFromReader(IDataReader reader)
        {
            List<Role> items = new List<Role>();
            while (reader.Read())
                items.Add(GetRoleFromReader(reader));
            return items;
        }
    }
}