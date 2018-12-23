using System;
using System.Collections.Generic;
using System.Threading;
using LOB.Core;
using LOB.Data;

namespace LOB.BLL
{
    public class ElementTypes : BaseData
    {
        public static List<ElementType> GetElementTypes(string query)
        {
            List<ElementType> elementTypes = DataAccess.ElementTypes.GetElementTypes(query);
               
            return elementTypes;
        }

        public static int CountElementTypes(string query)
        {
            return DataAccess.ElementTypes.CountElementTypes(query);
        }

        public static List<ElementType> GetRandomElementTypes(int count, RoleType role)
        {
            List<ElementType> elementTypes = DataAccess.ElementTypes.GetRandomElementTypes(count);
            return elementTypes;
        }

        public static ElementType GetElementTypeByElementTypeId(Guid elementTypeId)
        {
            ElementType elementType = null;
            string key = "ElementTypes_GetElementTypeByElementTypeId_" + elementTypeId;

            if (Cache[key] != null)
            {
                elementType = (ElementType) Cache[key];
            }
            else
            {
                elementType = DataAccess.ElementTypes.GetElementTypeByElementTypeId(elementTypeId);
                CacheData(key, elementType);
            }
            return elementType;
        }
        
        //public static ElementType InsertElementType(ElementType elementType)
        //{
        //    RemoveFromCache("ElementTypes_");
        //    int elementTypeId = DataAccess.ElementTypes.InsertElementType(elementType);
        //    elementType = DataAccess.ElementTypes.GetElementTypeByElementTypeId(elementTypeId);
        //    return elementType;
        //}

        public static bool UpdateElementType(ElementType elementType)
        {
            RemoveFromCache("ElementTypes_");
            return DataAccess.ElementTypes.UpdateElementType(elementType);
        }

    }
}