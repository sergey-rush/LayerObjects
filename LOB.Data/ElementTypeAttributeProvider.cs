using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LOB.Core;

namespace LOB.Data
{
    public class ElementTypeAttributeProvider : ElementTypeAttributeManager
    {
        public override List<ElementTypeAttribute> GetElementTypeAttributesByElementTypeId(Guid elementTypeId)
        {
            string getPagedElementTypeAttributes = @"SELECT AttributeId, ElementTypeId, Mandatory, UnVisible FROM ElementTypeAttribute WHERE (ElementTypeId = @ElementTypeId)";

            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(getPagedElementTypeAttributes, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@ElementTypeId", SqlDbType.UniqueIdentifier).Value = elementTypeId;
                cn.Open();
                return GetElementTypeAttributeCollectionFromReader(ExecuteReader(cmd));
            }
        }

        public override int CountElementTypeAttributes(Guid elementTypeId)
        {
            string countElementTypeAttributes = @"SELECT COUNT(*) FROM ElementTypeAttribute WHERE (ElementTypeId = @ElementTypeId)";
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(countElementTypeAttributes, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@ElementTypeId", SqlDbType.UniqueIdentifier).Value = elementTypeId;
                cn.Open();
                return (int)ExecuteScalar(cmd);
            }
        }

        public override List<ElementTypeAttribute> GetAttributesByElementTypeId(Guid elementTypeId)
        {
            string getAttributesByElementTypeId = @"SELECT eta.AttributeId, eta.ElementTypeId, eta.Mandatory, eta.UnVisible, att.Id, att.AttributeTypeId, att.Code, att.Description, att.Caption, att.Searchable, atp.Code AS Title, atp.Caption AS AttributeType
FROM ElementTypeAttribute AS eta INNER JOIN Attribute AS att ON eta.AttributeId = att.Id INNER JOIN AttributeType AS atp ON atp.Id = att.AttributeTypeId
WHERE (eta.ElementTypeId = @ElementTypeId)";
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(getAttributesByElementTypeId, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@ElementTypeId", SqlDbType.UniqueIdentifier).Value = elementTypeId;
                cn.Open();
                return GetAttributeCollectionFromReader(ExecuteReader(cmd));
            }
        }

        public override ElementTypeAttribute GetElementTypeAttributeByElementTypeAttributeId(int elementTypeAttributeId)
        {
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand("ElementTypeAttributes_GetElementTypeAttributeByElementTypeAttributeId", cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@ElementTypeAttributeId", SqlDbType.Int).Value = elementTypeAttributeId;
                cn.Open();
                using (IDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (reader.Read())
                        return GetElementTypeAttributeFromReader(reader);
                    else
                        return null;
                }
            }
        }

        public override int InsertElementTypeAttribute(ElementTypeAttribute elementTypeAttribute)
        {
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand("ElementTypeAttributes_InsertElementTypeAttribute", cn);
                cmd.CommandType = CommandType.Text;
                //cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = elementTypeAttribute.Name;
                //cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = elementTypeAttribute.Phone;
                //cmd.Parameters.Add("@StateId", SqlDbType.Int).Value = elementTypeAttribute.ItemState;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (int) cmd.Parameters["@Id"].Value;
            }
        }

        public override bool UpdateElementTypeAttribute(ElementTypeAttribute elementTypeAttribute)
        {
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand("ElementTypeAttributes_UpdateElementTypeAttribute", cn);
                //cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = elementTypeAttribute.Id;
                //cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = elementTypeAttribute.Name;
                //cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = elementTypeAttribute.Phone;
                //cmd.Parameters.Add("@StateId", SqlDbType.Int).Value = elementTypeAttribute.ItemState;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }

        public override bool DeleteElementTypeAttributeByElementTypeAttributeId(int elementTypeAttributeId)
        {
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand("ElementTypeAttributes_DeleteElementTypeAttributeByElementTypeAttributeId", cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@ElementTypeAttributeId", SqlDbType.Int).Value = elementTypeAttributeId;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }
    }
}