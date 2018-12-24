using System;
using System.Collections.Generic;
using System.Threading;
using LOB.Core;
using LOB.Data;

namespace LOB.BLL
{
    public class DrawingTypes : BaseData
    {
        public static List<DrawingType> GetDrawingTypes()
        {
            List<DrawingType> drawingTypes = null;
            string key = "DrawingTypes_GetDrawingTypes_";

            if (Cache[key] != null)
            {
                drawingTypes = (List<DrawingType>)Cache[key];
            }
            else
            {
                drawingTypes = DataAccess.DrawingTypes.GetDrawingTypes();
                CacheData(key, drawingTypes);
            }
            return drawingTypes;
        }

        public static int CountDrawingTypes(Guid elementTypeId)
        {
            return DataAccess.DrawingTypes.CountDrawingTypes(elementTypeId);
        }

        public static List<DrawingTypeAttribute> GetDrawingTypeAttributesByAttributeId(Guid attributeId)
        {
            List<DrawingTypeAttribute> drawingTypeAttributes = null;
            string key = "DrawingTypes_GetDrawingTypeAttributesByAttributeId_" + attributeId;

            if (Cache[key] != null)
            {
                drawingTypeAttributes = (List<DrawingTypeAttribute>)Cache[key];
            }
            else
            {
                drawingTypeAttributes = DataAccess.DrawingTypes.GetDrawingTypeAttributesByAttributeId(attributeId);
                CacheData(key, drawingTypeAttributes);
            }
            return drawingTypeAttributes;
        }

        public static int CountDrawingTypeValues(Guid drawingTypeId)
        {
            return DataAccess.DrawingTypes.CountDrawingTypeValues(drawingTypeId);
        }

        public static DrawingType GetDrawingTypeByDrawingTypeId(Guid drawingTypeId)
        {
            DrawingType drawingType = null;
            string key = "DrawingTypes_GetDrawingTypeByDrawingTypeId_" + drawingTypeId;

            if (Cache[key] != null)
            {
                drawingType = (DrawingType) Cache[key];
            }
            else
            {
                drawingType = DataAccess.DrawingTypes.GetDrawingTypeByDrawingTypeId(drawingTypeId);
                CacheData(key, drawingType);
            }
            return drawingType;
        }

        public static bool DeleteDrawingTypeAttributeByAttributeId(Guid attributeId)
        {
            RemoveFromCache("DrawingTypes_");
            return DataAccess.DrawingTypes.DeleteDrawingTypeAttributeByAttributeId(attributeId);
        }

        public static bool DeleteDrawingTypeAttributeValueByElementTypeId(Guid elementTypeId)
        {
            RemoveFromCache("DrawingTypes_");
            return DataAccess.DrawingTypes.DeleteDrawingTypeAttributeValueByElementTypeId(elementTypeId);
        }

        public static bool DeleteDrawingTypeAttributeValueByDrawingTypeAttributeId(Guid drawingTypeAttributeId)
        {
            RemoveFromCache("DrawingTypes_");
            return DataAccess.DrawingTypes.DeleteDrawingTypeAttributeValueByDrawingTypeAttributeId(drawingTypeAttributeId);
        }
    }
}