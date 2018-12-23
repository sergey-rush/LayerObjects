using System;
using System.Collections.Generic;
using System.Threading;
using LOB.Core;
using LOB.Data;

namespace LOB.BLL
{
    public class ElementTypeAttributes : BaseData
    {
        public static List<ElementTypeAttribute> GetElementTypeAttributesByElementTypeId(Guid elementTypeId)
        {
            List<ElementTypeAttribute> elementTypeAttributes = null;
            string key = "ElementTypeAttributes_GetElementTypeAttributesByElementTypeId_" + elementTypeId;

            if (Cache[key] != null)
            {
                elementTypeAttributes = (List<ElementTypeAttribute>)Cache[key];
            }
            else
            {
                elementTypeAttributes = DataAccess.ElementTypeAttributes.GetElementTypeAttributesByElementTypeId(elementTypeId);
                CacheData(key, elementTypeAttributes);
            }
            return elementTypeAttributes;
        }

        public static int CountElementTypeAttributes(Guid elementTypeId)
        {
            return DataAccess.ElementTypeAttributes.CountElementTypeAttributes(elementTypeId);
        }

        public static List<ElementTypeAttribute> GetAttributesByElementTypeId(Guid elementTypeId)
        {
            List<ElementTypeAttribute> elementTypeAttributes = null;
            string key = "ElementTypeAttributes_GetAttributesByElementTypeId_" + elementTypeId;

            if (Cache[key] != null)
            {
                elementTypeAttributes = (List<ElementTypeAttribute>) Cache[key];
            }
            else
            {
                elementTypeAttributes = DataAccess.ElementTypeAttributes.GetAttributesByElementTypeId(elementTypeId);
                CacheData(key, elementTypeAttributes);
            }
            return elementTypeAttributes;
        }
        
        public static ElementTypeAttribute InsertElementTypeAttribute(ElementTypeAttribute elementTypeAttribute)
        {
            RemoveFromCache("ElementTypeAttributes_");
            int elementTypeAttributeId = DataAccess.ElementTypeAttributes.InsertElementTypeAttribute(elementTypeAttribute);
            elementTypeAttribute = DataAccess.ElementTypeAttributes.GetElementTypeAttributeByElementTypeAttributeId(elementTypeAttributeId);
            return elementTypeAttribute;
        }

        public static bool UpdateElementTypeAttribute(ElementTypeAttribute elementTypeAttribute)
        {
            RemoveFromCache("ElementTypeAttributes_");
            return DataAccess.ElementTypeAttributes.UpdateElementTypeAttribute(elementTypeAttribute);
        }

    }
}