using System;
using System.Collections.Generic;
using System.Threading;
using LOB.Core;
using LOB.Data;

namespace LOB.BLL
{
    public class Layers : BaseData
    {
        public static List<LayerConditionsElement> GetLayerConditionsElementsByElementId(Guid elementId)
        {
            List<LayerConditionsElement> layerConditionsElements = null;
            string key = "Layers_GetLayerConditionsElementsByElementId_" + elementId;

            if (Cache[key] != null)
            {
                layerConditionsElements = (List<LayerConditionsElement>)Cache[key];
            }
            else
            {
                layerConditionsElements = DataAccess.Layers.GetLayerConditionsElementsByElementId(elementId);
                CacheData(key, layerConditionsElements);
            }

            return layerConditionsElements;
        }

        public static int CountLayers(string query)
        {
            return DataAccess.Layers.CountLayers(query);
        }

        public static List<LayerElementType> GetLayerElementTypesByElementTypeId(Guid elementTypeId)
        {
            List<LayerElementType> layerElementTypes = null;
            string key = "Layers_GetLayerElementTypesByElementTypeId_" + elementTypeId;

            if (Cache[key] != null)
            {
                layerElementTypes = (List<LayerElementType>)Cache[key];
            }
            else
            {
                layerElementTypes = DataAccess.Layers.GetLayerElementTypesByElementTypeId(elementTypeId);
                CacheData(key, layerElementTypes);
            }

            return layerElementTypes;
        }

        public static Layer GetLayerByLayerId(Guid layerId)
        {
            Layer layer = null;
            string key = "Layers_GetLayerByLayerId_" + layerId;

            if (Cache[key] != null)
            {
                layer = (Layer) Cache[key];
            }
            else
            {
                layer = DataAccess.Layers.GetLayerByLayerId(layerId);
                CacheData(key, layer);
            }
            return layer;
        }

        public static Guid InsertLayer(Layer layer)
        {
            RemoveFromCache("Layers_");
            Guid layerId = DataAccess.Layers.InsertLayer(layer);
            return layerId;
        }

        public static bool UpdateLayer(Layer layer)
        {
            RemoveFromCache("Layers_");
            return DataAccess.Layers.UpdateLayer(layer);
        }

        public static bool DeleteLayerElementTypeByElementTypeId(Guid elementTypeId)
        {
            RemoveFromCache("Layers_");
            return DataAccess.Layers.DeleteLayerElementTypeByElementTypeId(elementTypeId);
        }

        public static bool DeleteFilterByLayerElementTypeId(Guid layerElementTypeId)
        {
            RemoveFromCache("Layers_");
            return DataAccess.Layers.DeleteFilterByLayerElementTypeId(layerElementTypeId);
        }

        public static bool DeleteFilterByAttributeId(Guid attributeId)
        {
            RemoveFromCache("Layers_");
            return DataAccess.Layers.DeleteFilterByAttributeId(attributeId);
        }

        public static bool DeleteLayerConditionsElementsByElementId(Guid elementId)
        {
            RemoveFromCache("Layers_");
            return DataAccess.Layers.DeleteLayerConditionsElementsByElementId(elementId);
        }
    }
}