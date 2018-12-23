using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LOB.Core;
using Attribute = LOB.Core.Attribute;

namespace LOB.Data
{
    public class AttributeProvider : AttributeManager
    {
        public override List<Attribute> GetPagedAttributes(int pageIndex, int pageSize, Guid elementTypeId)
        {
            string getPagedAttributes = @"SELECT AttributeId, ElementTypeId, Mandatory, UnVisible FROM 
(SELECT AttributeId, ElementTypeId, Mandatory, UnVisible, ROW_NUMBER() OVER (ORDER BY ElementTypeId) AS RowNums FROM Attribute
WHERE (ElementTypeId = @ElementTypeId))ST 
WHERE ST.RowNums BETWEEN (@PageIndex * @PageSize + 1) AND ((@PageIndex + 1) * @PageSize)";

            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(getPagedAttributes, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@ElementTypeId", SqlDbType.UniqueIdentifier).Value = elementTypeId;
                cmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pageIndex - 1;
                cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
                cn.Open();
                return GetAttributeCollectionFromReader(ExecuteReader(cmd));
            }
        }

        public override int CountAttributes(Guid elementTypeId)
        {
            string countAttributes = @"SELECT COUNT(*) FROM Attribute WHERE (ElementTypeId = @ElementTypeId)";
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(countAttributes, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@ElementTypeId", SqlDbType.UniqueIdentifier).Value = elementTypeId;
                cn.Open();
                return (int)ExecuteScalar(cmd);
            }
        }

        public override List<AttributeValue> GetPagedAttributeValues(int pageIndex, int pageSize, Guid attributeId)
        {
            string getPagedAttributeValues = @"SELECT AttributeId, ElementId, Value, Caption, Longitude, Latitude FROM 
(SELECT atv.AttributeId, atv.ElementId, atv.Value, elm.Caption, elm.Location.Long AS Longitude, elm.Location.Lat AS Latitude,
 ROW_NUMBER() OVER (ORDER BY AttributeId) AS RowNums 
FROM AttributeValue atv INNER JOIN Element elm ON atv.ElementId = elm.Id
WHERE (AttributeId = @AttributeId))ST 
WHERE ST.RowNums BETWEEN (@PageIndex * @PageSize + 1) AND ((@PageIndex + 1) * @PageSize)";

            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(getPagedAttributeValues, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@AttributeId", SqlDbType.UniqueIdentifier).Value = attributeId;
                cmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pageIndex - 1;
                cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
                cn.Open();
                return GetAttributeValueCollectionFromReader(ExecuteReader(cmd));
            }
        }

        public override AttributeValue GetAvgAttributeValueByAttributeId(Guid attributeId)
        {
            string getAvgAttributeValueByAttributeId = @"SELECT @AttributeId AS AttributeId, @AttributeId AS ElementId, 'Value' AS Value, 'Caption' AS Caption, AVG(elm.Location.Long) AS Longitude, AVG(elm.Location.Lat) AS Latitude 
FROM AttributeValue atv INNER JOIN Element elm ON atv.ElementId = elm.Id
WHERE (AttributeId = @AttributeId)";
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(getAvgAttributeValueByAttributeId, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@AttributeId", SqlDbType.UniqueIdentifier).Value = attributeId;
                cn.Open();
                using (IDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (reader.Read())
                        return GetAttributeValueFromReader(reader);
                    else
                        return null;
                }
            }
        }

        public override int CountAttributeValues(Guid attributeId)
        {
            string countAttributes = @"SELECT COUNT(*) FROM AttributeValue WHERE (AttributeId = @AttributeId)";
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(countAttributes, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@AttributeId", SqlDbType.UniqueIdentifier).Value = attributeId;
                cn.Open();
                return (int)ExecuteScalar(cmd);
            }
        }

        public override Attribute GetAttributeByAttributeId(Guid attributeId)
        {
            string getAttributeByAttributeId = @"SELECT Id, AttributeTypeId, Code, Description, Caption, Searchable FROM Attribute WHERE Id = @AttributeId";
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(getAttributeByAttributeId, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@AttributeId", SqlDbType.UniqueIdentifier).Value = attributeId;
                cn.Open();
                using (IDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (reader.Read())
                        return GetAttributeFromReader(reader);
                    else
                        return null;
                }
            }
        }

        public override int InsertAttribute(Attribute attribute)
        {
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand("Attributes_InsertAttribute", cn);
                cmd.CommandType = CommandType.Text;
                //cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = attribute.Name;
                //cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = attribute.Phone;
                //cmd.Parameters.Add("@StateId", SqlDbType.Int).Value = attribute.ItemState;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (int) cmd.Parameters["@Id"].Value;
            }
        }

        public override bool UpdateAttribute(Attribute attribute)
        {
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand("Attributes_UpdateAttribute", cn);
                //cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = attribute.Id;
                //cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = attribute.Name;
                //cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = attribute.Phone;
                //cmd.Parameters.Add("@StateId", SqlDbType.Int).Value = attribute.ItemState;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }

        public override bool DeleteAttributeByAttributeId(int attributeId)
        {
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand("Attributes_DeleteAttributeByAttributeId", cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@AttributeId", SqlDbType.Int).Value = attributeId;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }
    }
}