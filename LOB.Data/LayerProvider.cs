using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LOB.Core;

namespace LOB.Data
{
    public class LayerProvider : LayerManager
    {
        public override List<LayerConditionsElement> GetLayerConditionsElementsByElementId(Guid elementId)
        {
            string getLayerConditionsElementsByElementId = @"SELECT LayerId, ElementId, TypeComparer FROM LayerConditionsElements WHERE ElementId = @ElementId";

            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(getLayerConditionsElementsByElementId, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@ElementId", SqlDbType.UniqueIdentifier).Value = elementId;

                cn.Open();
                return GetLayerConditionsElementCollectionFromReader(ExecuteReader(cmd));
            }
        }

        public override int CountLayers(string query)
        {
            string countLayers = @"SELECT COUNT(*) FROM Layer WHERE Id LIKE '%' + @Query + '%'";
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(countLayers, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@Query", SqlDbType.NVarChar).Value = query;
                cn.Open();
                return (int)ExecuteScalar(cmd);
            }
        }

        public override List<LayerElementType> GetLayerElementTypesByElementTypeId(Guid elementTypeId)
        {
            string getLayerElementTypesByElementTypeId = @"SELECT Id, LayerId, ElementTypeId FROM LayerElementType WHERE ElementTypeId = @ElementTypeId";
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(getLayerElementTypesByElementTypeId, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@ElementTypeId", SqlDbType.UniqueIdentifier).Value = elementTypeId;
                cn.Open();
                return GetLayerElementTypeCollectionFromReader(ExecuteReader(cmd));
            }
        }

        public override Layer GetLayerByLayerId(Guid layerId)
        {
            string getLayerByLayerId = @"SELECT Id, Code, DrawingTypeId, Caption FROM Layer WHERE Id = @LayerId";
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(getLayerByLayerId, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@LayerId", SqlDbType.UniqueIdentifier).Value = layerId;
                cn.Open();
                using (IDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (reader.Read())
                        return GetLayerFromReader(reader);
                    else
                        return null;
                }
            }
        }

        public override Guid InsertLayer(Layer layer)
        {
            string insertLayer = @"INSERT INTO Layer (Code, DrawingTypeId, Caption) OUTPUT INSERTED.Id VALUES (@Code, @DrawingTypeId, @Caption);";
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(insertLayer, cn);
                cmd.CommandType = CommandType.Text;
                //cmd.Parameters.Add("@Code", SqlDbType.VarChar).Value = layer.Code;
                //cmd.Parameters.Add("@DrawingTypeId", SqlDbType.UniqueIdentifier).Value = layer.DrawingTypeId;
                cmd.Parameters.Add("@Caption", SqlDbType.VarChar).Value = layer.Caption;
                cn.Open();
                object ret = ExecuteScalar(cmd);
                return (Guid)ret;
            }
        }

        public override bool UpdateLayer(Layer layer)
        {
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand("Layers_UpdateLayer", cn);
                cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = layer.Id;
                //cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = layer.Name;
                //cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = layer.Phone;
                //cmd.Parameters.Add("@StateId", SqlDbType.Int).Value = layer.ItemState;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }

        public override bool DeleteLayerElementTypeByElementTypeId(Guid elementTypeId)
        {
            string deleteLayerElementTypeByElementTypeId = @"DELETE FROM LayerElementType WHERE ElementTypeId = @ElementTypeId";
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(deleteLayerElementTypeByElementTypeId, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@ElementTypeId", SqlDbType.UniqueIdentifier).Value = elementTypeId;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }

        public override bool DeleteFilterByLayerElementTypeId(Guid layerElementTypeId)
        {
            string deleteLayerByLayerId = @"DELETE FROM Filter WHERE LayerElementTypeId = @LayerElementTypeId";
            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(deleteLayerByLayerId, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@LayerElementTypeId", SqlDbType.UniqueIdentifier).Value = layerElementTypeId;
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

        public override bool DeleteLayerConditionsElementsByElementId(Guid elementId)
        {
            string deleteLayerConditionsElementsByElementId = @"DELETE FROM LayerConditionsElements WHERE ElementId = @ElementId";

            using (SqlConnection cn = new SqlConnection(LayerObjectsConnection))
            {
                SqlCommand cmd = new SqlCommand(deleteLayerConditionsElementsByElementId, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@ElementId", SqlDbType.UniqueIdentifier).Value = elementId;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }
    }
}