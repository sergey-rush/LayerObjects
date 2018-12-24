using System;
using System.Collections.Generic;
using System.Data;
using LOB.Core;

namespace LOB.Data
{
    public abstract class LayerManager: DataAccess
    {
        private static LayerManager _instance;

        public static LayerManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LayerProvider();
                }
                return _instance;
            }
        }

        public abstract List<LayerConditionsElement> GetLayerConditionsElementsByElementId(Guid elementId);
        public abstract int CountLayers(string query);
        public abstract List<LayerElementType> GetLayerElementTypesByElementTypeId(Guid elementTypeId);
        public abstract Layer GetLayerByLayerId(Guid layer);
        public abstract Guid InsertLayer(Layer layer);
        public abstract bool UpdateLayer(Layer layer);
        public abstract bool DeleteLayerElementTypeByElementTypeId(Guid elementTypeId);
        public abstract bool DeleteFilterByLayerElementTypeId(Guid layerElementTypeId);
        public abstract bool DeleteFilterByAttributeId(Guid attributeId);
        public abstract bool DeleteLayerConditionsElementsByElementId(Guid elementId);

        protected virtual Layer GetLayerFromReader(IDataReader reader)
        {
            Layer layer = new Layer()
            {
                Id = (Guid)reader["Id"],
                Caption = reader["Caption"].ToString()
            };
            return layer;
        }
       
        protected virtual List<Layer> GetLayerCollectionFromReader(IDataReader reader)
        {
            List<Layer> items = new List<Layer>();
            while (reader.Read())
                items.Add(GetLayerFromReader(reader));
            return items;
        }

        protected virtual LayerConditionsElement GetLayerConditionsElementFromReader(IDataReader reader)
        {
            LayerConditionsElement layer = new LayerConditionsElement()
            {
                LayerId = (Guid)reader["LayerId"],
                ElementId = (Guid)reader["ElementId"],
                TypeComparer = (int)reader["TypeComparer"]
            };
            return layer;
        }

        protected virtual List<LayerConditionsElement> GetLayerConditionsElementCollectionFromReader(IDataReader reader)
        {
            List<LayerConditionsElement> items = new List<LayerConditionsElement>();
            while (reader.Read())
                items.Add(GetLayerConditionsElementFromReader(reader));
            return items;
        }

        protected virtual LayerElementType GetLayerElementTypeFromReader(IDataReader reader)
        {
            LayerElementType layer = new LayerElementType()
            {
                Id = (Guid)reader["Id"],
                LayerId = (Guid)reader["LayerId"],
                ElementTypeId = (Guid)reader["ElementTypeId"],
            };
            return layer;
        }

        protected virtual List<LayerElementType> GetLayerElementTypeCollectionFromReader(IDataReader reader)
        {
            List<LayerElementType> items = new List<LayerElementType>();
            while (reader.Read())
                items.Add(GetLayerElementTypeFromReader(reader));
            return items;
        }
    }
}