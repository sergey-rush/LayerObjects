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

        public override List<ElementType> GetRandomElementTypes(int count)
        {
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand("ElementTypes_GetRandomElementTypes", cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@Count", SqlDbType.Int).Value = count;
                cn.Open();
                return GetElementTypeCollectionFromReader(ExecuteReader(cmd));
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

        public override int InsertElementType(ElementType elementType)
        {
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand("ElementTypes_InsertElementType", cn);
                cmd.CommandType = CommandType.Text;
                //cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = elementType.Name;
                //cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = elementType.Phone;
                //cmd.Parameters.Add("@StateId", SqlDbType.Int).Value = elementType.ItemState;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (int) cmd.Parameters["@Id"].Value;
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

        public override bool DeleteElementTypeByElementTypeId(int elementTypeId)
        {
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand("ElementTypes_DeleteElementTypeByElementTypeId", cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@ElementTypeId", SqlDbType.Int).Value = elementTypeId;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }
    }
}