using System;
using System.Collections.Generic;
using System.Data;
using LOB.Core;
using DrawingType = LOB.Core.DrawingType;

namespace LOB.Data
{
    public abstract class DrawingTypeManager: DataAccess
    {
        private static DrawingTypeManager _instance;

        public static DrawingTypeManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DrawingTypeProvider();
                }
                return _instance;
            }
        }

        public abstract List<DrawingType> GetDrawingTypes();
        public abstract int CountDrawingTypes(Guid elementTypeId);
        public abstract List<DrawingTypeAttribute> GetDrawingTypeAttributesByAttributeId(Guid attributeId);
        public abstract int CountDrawingTypeValues(Guid drawingTypeId);
        public abstract DrawingType GetDrawingTypeByDrawingTypeId(Guid drawingType);
        public abstract int InsertDrawingType(DrawingType drawingType);
        public abstract bool UpdateDrawingType(DrawingType drawingType);
        public abstract bool DeleteDrawingTypeAttributeByAttributeId(Guid attributeId);
        public abstract bool DeleteDrawingTypeAttributeValueByElementTypeId(Guid elementTypeId);
        public abstract bool DeleteDrawingTypeAttributeValueByDrawingTypeAttributeId(Guid drawingTypeAttributeId);
        protected virtual DrawingType GetDrawingTypeFromReader(IDataReader reader)
        {
            DrawingType drawingType = new DrawingType()
            {
                Id = (Guid)reader["Id"],
                Caption = reader["Caption"].ToString()
            };
            if (reader["Code"] != DBNull.Value)
            {
                drawingType.Code = reader["Code"].ToString();
            }
            return drawingType;
        }
       
        protected virtual List<DrawingType> GetDrawingTypeCollectionFromReader(IDataReader reader)
        {
            List<DrawingType> items = new List<DrawingType>();
            while (reader.Read())
                items.Add(GetDrawingTypeFromReader(reader));
            return items;
        }

        protected virtual DrawingTypeAttribute GetDrawingTypeAttributeFromReader(IDataReader reader)
        {
            DrawingTypeAttribute drawingType = new DrawingTypeAttribute()
            {
                Id = (Guid)reader["Id"],
                DrawingTypeId = (Guid)reader["DrawingTypeId"],
                AttributeId = (Guid)reader["AttributeId"],
            };
            if (reader["Code"] != DBNull.Value)
            {
                drawingType.Code = reader["Code"].ToString();
            }
            return drawingType;
        }

        protected virtual List<DrawingTypeAttribute> GetDrawingTypeAttributeCollectionFromReader(IDataReader reader)
        {
            List<DrawingTypeAttribute> items = new List<DrawingTypeAttribute>();
            while (reader.Read())
                items.Add(GetDrawingTypeAttributeFromReader(reader));
            return items;
        }

    }
}