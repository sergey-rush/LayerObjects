using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LOB.Core;
using DrawingType = LOB.Core.DrawingType;

namespace LOB.Data
{
    public class DrawingTypeProvider : DrawingTypeManager
    {
        public override List<DrawingType> GetDrawingTypes()
        {
            string getPagedDrawingTypes = @"SELECT Id, Code, Caption FROM DrawingType";

            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(getPagedDrawingTypes, cn);
                cmd.CommandType = CommandType.Text;
                cn.Open();
                return GetDrawingTypeCollectionFromReader(ExecuteReader(cmd));
            }
        }

        public override int CountDrawingTypes(Guid elementTypeId)
        {
            string countDrawingTypes = @"SELECT COUNT(*) FROM DrawingType WHERE (ElementTypeId = @ElementTypeId)";
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(countDrawingTypes, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@ElementTypeId", SqlDbType.UniqueIdentifier).Value = elementTypeId;
                cn.Open();
                return (int)ExecuteScalar(cmd);
            }
        }

        public override List<DrawingTypeAttribute> GetDrawingTypeAttributesByAttributeId(Guid attributeId)
        {
            string getDrawingTypeAttributesByAttributeId = @"SELECT Id, DrawingTypeId, AttributeId, Code FROM DrawingTypeAttribute WHERE AttributeId = @AttributeId";

            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(getDrawingTypeAttributesByAttributeId, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@AttributeId", SqlDbType.UniqueIdentifier).Value = attributeId;
                cn.Open();
                return GetDrawingTypeAttributeCollectionFromReader(ExecuteReader(cmd));
            }
        }

        public override int CountDrawingTypeValues(Guid drawingTypeId)
        {
            string countDrawingTypes = @"SELECT COUNT(*) FROM DrawingTypeValue WHERE (DrawingTypeId = @DrawingTypeId)";
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(countDrawingTypes, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@DrawingTypeId", SqlDbType.UniqueIdentifier).Value = drawingTypeId;
                cn.Open();
                return (int)ExecuteScalar(cmd);
            }
        }

        public override DrawingType GetDrawingTypeByDrawingTypeId(Guid drawingTypeId)
        {
            string getDrawingTypeByDrawingTypeId = @"SELECT Id, DrawingTypeTypeId, Code, Description, Caption, Searchable FROM DrawingType WHERE Id = @DrawingTypeId";
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(getDrawingTypeByDrawingTypeId, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@DrawingTypeId", SqlDbType.UniqueIdentifier).Value = drawingTypeId;
                cn.Open();
                using (IDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (reader.Read())
                        return GetDrawingTypeFromReader(reader);
                    else
                        return null;
                }
            }
        }

        public override int InsertDrawingType(DrawingType drawingType)
        {
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand("DrawingTypes_InsertDrawingType", cn);
                cmd.CommandType = CommandType.Text;
                //cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = drawingType.Name;
                //cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = drawingType.Phone;
                //cmd.Parameters.Add("@StateId", SqlDbType.Int).Value = drawingType.ItemState;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (int) cmd.Parameters["@Id"].Value;
            }
        }

        public override bool UpdateDrawingType(DrawingType drawingType)
        {
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand("DrawingTypes_UpdateDrawingType", cn);
                //cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = drawingType.Id;
                //cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = drawingType.Name;
                //cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = drawingType.Phone;
                //cmd.Parameters.Add("@StateId", SqlDbType.Int).Value = drawingType.ItemState;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }

        public override bool DeleteDrawingTypeAttributeByAttributeId(Guid attributeId)
        {
            string deleteDrawingTypeAttributeByElementTypeId = @"DELETE FROM DrawingTypeAttribute WHERE (AttributeId = @AttributeId)";

            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(deleteDrawingTypeAttributeByElementTypeId, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@AttributeId", SqlDbType.UniqueIdentifier).Value = attributeId;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }

        public override bool DeleteDrawingTypeAttributeValueByElementTypeId(Guid elementTypeId)
        {
            string deleteDrawingTypeAttributeValueByElementTypeId = @"DELETE FROM DrawingTypeAttributeValue WHERE (ElementTypeId = @ElementTypeId)";
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(deleteDrawingTypeAttributeValueByElementTypeId, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@ElementTypeId", SqlDbType.UniqueIdentifier).Value = elementTypeId;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }

        public override bool DeleteDrawingTypeAttributeValueByDrawingTypeAttributeId(Guid drawingTypeAttributeId)
        {
            string deleteDrawingTypeAttributeValueByElementTypeId = @"DELETE FROM DrawingTypeAttributeValue WHERE (DrawingTypeAttributeId = @DrawingTypeAttributeId)";
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(deleteDrawingTypeAttributeValueByElementTypeId, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@DrawingTypeAttributeId", SqlDbType.UniqueIdentifier).Value = drawingTypeAttributeId;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }
    }
}