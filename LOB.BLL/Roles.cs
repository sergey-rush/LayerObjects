using System;
using System.Collections.Generic;
using System.Threading;
using LOB.Core;
using LOB.Data;

namespace LOB.BLL
{
    public class Roles : BaseData
    {
        //public static List<RoleConditionsElement> GetRoleConditionsElementsByElementId(Guid elementId)
        //{
        //    List<RoleConditionsElement> roleConditionsElements = null;
        //    string key = "Roles_GetRoleConditionsElementsByElementId_" + elementId;

        //    if (Cache[key] != null)
        //    {
        //        roleConditionsElements = (List<RoleConditionsElement>)Cache[key];
        //    }
        //    else
        //    {
        //        roleConditionsElements = DataAccess.Roles.GetRoleConditionsElementsByElementId(elementId);
        //        CacheData(key, roleConditionsElements);
        //    }

        //    return roleConditionsElements;
        //}

        public static int CountRoles(string query)
        {
            return DataAccess.Roles.CountRoles(query);
        }

        //public static List<RoleElementType> GetRoleElementTypesByElementTypeId(Guid elementTypeId)
        //{
        //    List<RoleElementType> roleElementTypes = null;
        //    string key = "Roles_GetRoleElementTypesByElementTypeId_" + elementTypeId;

        //    if (Cache[key] != null)
        //    {
        //        roleElementTypes = (List<RoleElementType>)Cache[key];
        //    }
        //    else
        //    {
        //        roleElementTypes = DataAccess.Roles.GetRoleElementTypesByElementTypeId(elementTypeId);
        //        CacheData(key, roleElementTypes);
        //    }

        //    return roleElementTypes;
        //}

        public static Role GetRoleByRoleId(Guid roleId)
        {
            Role role = null;
            string key = "Roles_GetRoleByRoleId_" + roleId;

            if (Cache[key] != null)
            {
                role = (Role) Cache[key];
            }
            else
            {
                role = DataAccess.Roles.GetRoleByRoleId(roleId);
                CacheData(key, role);
            }
            return role;
        }

        public static Guid InsertRole(Role role)
        {
            RemoveFromCache("Roles_");
            Guid roleId = DataAccess.Roles.InsertRole(role);
            return roleId;
        }

        public static bool UpdateRole(Role role)
        {
            RemoveFromCache("Roles_");
            return DataAccess.Roles.UpdateRole(role);
        }

        public static bool DeleteRoleElementTypeByElementTypeId(Guid elementTypeId)
        {
            RemoveFromCache("Roles_");
            return DataAccess.Roles.DeleteRoleElementTypeByElementTypeId(elementTypeId);
        }

        public static bool DeleteFilterByRoleElementTypeId(Guid roleElementTypeId)
        {
            RemoveFromCache("Roles_");
            return DataAccess.Roles.DeleteFilterByRoleElementTypeId(roleElementTypeId);
        }

        public static bool DeleteFilterByAttributeId(Guid attributeId)
        {
            RemoveFromCache("Roles_");
            return DataAccess.Roles.DeleteFilterByAttributeId(attributeId);
        }

        public static bool DeleteRoleConditionsElementsByElementId(Guid elementId)
        {
            RemoveFromCache("Roles_");
            return DataAccess.Roles.DeleteRoleConditionsElementsByElementId(elementId);
        }
    }
}