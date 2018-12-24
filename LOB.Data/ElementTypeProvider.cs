using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LOB.Core;

namespace LOB.Data
{
    public class ElementTypeProvider : ElementTypeManager
    {
        public override List<ElementType> GetElementTypes(string query)
        {
            string getPagedElementTypes = @"SELECT Id, Code, DrawingTypeId, Caption FROM ElementType WHERE (Code LIKE CONCAT('%', @Query, '%') OR @Query IS NULL)";

            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(getPagedElementTypes, cn);
                cmd.CommandType = CommandType.Text;
                if (string.IsNullOrEmpty(query))
                {
                    cmd.Parameters.Add("@Query", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@Query", SqlDbType.NVarChar).Value = query;
                }
                
                cn.Open();
                return GetElementTypeCollectionFromReader(ExecuteReader(cmd));
            }
        }

        public override int CountElementTypes(string query)
        {
            string countElementTypes = @"SELECT COUNT(*) FROM ElementType WHERE Id LIKE '%' + @Query + '%'";
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(countElementTypes, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@Query", SqlDbType.NVarChar).Value = query;
                cn.Open();
                return (int)ExecuteScalar(cmd);
            }
        }

        public override List<Element> GetElementsByElementTypeId(Guid elementTypeId)
        {
            string getPagedElementTypes = @"SELECT Id, ElementTypeId, Location, Caption FROM Element WHERE ElementTypeId = @ElementTypeId";

            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(getPagedElementTypes, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@ElementTypeId", SqlDbType.UniqueIdentifier).Value = elementTypeId;
                cn.Open();
                return GetElementCollectionFromReader(ExecuteReader(cmd));
            }
        }

        public override ElementType GetElementTypeByElementTypeId(Guid elementTypeId)
        {
            string getElementTypeByElementTypeId = @"SELECT Id, Code, DrawingTypeId, Caption FROM ElementType WHERE Id = @ElementTypeId";
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(getElementTypeByElementTypeId, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@ElementTypeId", SqlDbType.UniqueIdentifier).Value = elementTypeId;
                cn.Open();
                using (IDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (reader.Read())
                        return GetElementTypeFromReader(reader);
                    else
                        return null;
                }
            }
        }

        public override Guid InsertElementType(ElementType elementType)
        {
            string insertElementType = @"INSERT INTO ElementType (Code, DrawingTypeId, Caption) OUTPUT INSERTED.Id VALUES (@Code, @DrawingTypeId, @Caption);";
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(insertElementType, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@Code", SqlDbType.VarChar).Value = elementType.Code;
                cmd.Parameters.Add("@DrawingTypeId", SqlDbType.UniqueIdentifier).Value = elementType.DrawingTypeId;
                cmd.Parameters.Add("@Caption", SqlDbType.VarChar).Value = elementType.Caption;
                cn.Open();
                object ret = ExecuteScalar(cmd);
                return (Guid)ret;
            }
        }

        public override bool UpdateElementType(ElementType elementType)
        {
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand("ElementTypes_UpdateElementType", cn);
                cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = elementType.Id;
                //cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = elementType.Name;
                //cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = elementType.Phone;
                //cmd.Parameters.Add("@StateId", SqlDbType.Int).Value = elementType.ItemState;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }

        public override bool DeleteElementTypeByElementTypeId(Guid elementTypeId)
        {
            string deleteElementTypeByElementTypeId = @"DELETE FROM ElementType WHERE Id = @ElementTypeId";
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(deleteElementTypeByElementTypeId, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@ElementTypeId", SqlDbType.UniqueIdentifier).Value = elementTypeId;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }

        public override bool DeleteElementsByElementTypeId(Guid elementTypeId)
        {
            string deleteElementByElementTypeId = @"DELETE FROM Element WHERE ElementTypeId = @ElementTypeId";

            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(deleteElementByElementTypeId, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@ElementTypeId", SqlDbType.UniqueIdentifier).Value = elementTypeId;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }
    }
}