using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LOB.Core;

namespace LOB.Data
{
    public class RoleProvider : RoleManager
    {
        //public override List<RoleConditionsElement> GetRoleConditionsElementsByElementId(Guid elementId)
        //{
        //    string getRoleConditionsElementsByElementId = @"SELECT RoleId, ElementId, TypeComparer FROM RoleConditionsElements WHERE ElementId = @ElementId";

        //    using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
        //    {
        //        SqlCommand cmd = new SqlCommand(getRoleConditionsElementsByElementId, cn);
        //        cmd.CommandType = CommandType.Text;
        //        cmd.Parameters.Add("@ElementId", SqlDbType.UniqueIdentifier).Value = elementId;

        //        cn.Open();
        //        return GetRoleConditionsElementCollectionFromReader(ExecuteReader(cmd));
        //    }
        //}

        public override int CountRoles(string query)
        {
            string countRoles = @"SELECT COUNT(*) FROM Role WHERE Id LIKE '%' + @Query + '%'";
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(countRoles, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@Query", SqlDbType.NVarChar).Value = query;
                cn.Open();
                return (int)ExecuteScalar(cmd);
            }
        }

        //public override List<RoleElementType> GetRoleElementTypesByElementTypeId(Guid elementTypeId)
        //{
        //    string getRoleElementTypesByElementTypeId = @"SELECT Id, RoleId, ElementTypeId FROM RoleElementType WHERE ElementTypeId = @ElementTypeId";
        //    using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
        //    {
        //        SqlCommand cmd = new SqlCommand(getRoleElementTypesByElementTypeId, cn);
        //        cmd.CommandType = CommandType.Text;
        //        cmd.Parameters.Add("@ElementTypeId", SqlDbType.UniqueIdentifier).Value = elementTypeId;
        //        cn.Open();
        //        return GetRoleElementTypeCollectionFromReader(ExecuteReader(cmd));
        //    }
        //}

        public override Role GetRoleByRoleId(Guid roleId)
        {
            string getRoleByRoleId = @"SELECT Id, Code, DrawingTypeId, Caption FROM Role WHERE Id = @RoleId";
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(getRoleByRoleId, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@RoleId", SqlDbType.UniqueIdentifier).Value = roleId;
                cn.Open();
                using (IDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (reader.Read())
                        return GetRoleFromReader(reader);
                    else
                        return null;
                }
            }
        }

        public override Guid InsertRole(Role role)
        {
            string insertRole = @"INSERT INTO Role (Code, DrawingTypeId, Caption) OUTPUT INSERTED.Id VALUES (@Code, @DrawingTypeId, @Caption);";
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(insertRole, cn);
                cmd.CommandType = CommandType.Text;
                //cmd.Parameters.Add("@Code", SqlDbType.VarChar).Value = role.Code;
                //cmd.Parameters.Add("@DrawingTypeId", SqlDbType.UniqueIdentifier).Value = role.DrawingTypeId;
                //cmd.Parameters.Add("@Caption", SqlDbType.VarChar).Value = role.Caption;
                cn.Open();
                object ret = ExecuteScalar(cmd);
                return (Guid)ret;
            }
        }

        public override bool UpdateRole(Role role)
        {
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand("Roles_UpdateRole", cn);
                cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = role.Id;
                //cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = role.Name;
                //cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = role.Phone;
                //cmd.Parameters.Add("@StateId", SqlDbType.Int).Value = role.ItemState;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }

        public override bool DeleteRoleElementTypeByElementTypeId(Guid elementTypeId)
        {
            string deleteRoleElementTypeByElementTypeId = @"DELETE FROM RoleElementType WHERE ElementTypeId = @ElementTypeId";
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(deleteRoleElementTypeByElementTypeId, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@ElementTypeId", SqlDbType.UniqueIdentifier).Value = elementTypeId;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }

        public override bool DeleteFilterByRoleElementTypeId(Guid roleElementTypeId)
        {
            string deleteRoleByRoleId = @"DELETE FROM Filter WHERE RoleElementTypeId = @RoleElementTypeId";
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(deleteRoleByRoleId, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@RoleElementTypeId", SqlDbType.UniqueIdentifier).Value = roleElementTypeId;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }

        public override bool DeleteFilterByAttributeId(Guid attributeId)
        {
            string deleteFilterByAttributeId = @"DELETE FROM Filter WHERE AttributeId = @AttributeId";
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(deleteFilterByAttributeId, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@AttributeId", SqlDbType.UniqueIdentifier).Value = attributeId;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }

        public override bool DeleteRoleConditionsElementsByElementId(Guid elementId)
        {
            string deleteRoleConditionsElementsByElementId = @"DELETE FROM RoleConditionsElements WHERE ElementId = @ElementId";

            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(deleteRoleConditionsElementsByElementId, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@ElementId", SqlDbType.UniqueIdentifier).Value = elementId;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }
    }
}