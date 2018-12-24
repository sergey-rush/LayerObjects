using System;
using System.Collections.Generic;
using System.Threading;
using LOB.Core;
using LOB.Data;
using Attribute = LOB.Core.Attribute;

namespace LOB.BLL
{
    public class Attributes : BaseData
    {
        public static List<Attribute> GetPagedAttributes(int pageIndex, int pageSize, Guid elementTypeId)
        {
            List<Attribute> attributes = DataAccess.Attributes.GetPagedAttributes(pageIndex, pageSize, elementTypeId);
               
            return attributes;
        }

        public static int CountAttributes(Guid elementTypeId)
        {
            return DataAccess.Attributes.CountAttributes(elementTypeId);
        }

        public static List<AttributeValue> GetPagedAttributeValues(int pageIndex, int pageSize, Guid attributeId)
        {
            List<AttributeValue> attributeValues = DataAccess.Attributes.GetPagedAttributeValues(pageIndex, pageSize, attributeId);
            return attributeValues;
        }

        public static AttributeValue GetAvgAttributeValueByAttributeId(Guid attributeId)
        {
            AttributeValue attributeValue = null;
            string key = "Attributes_GetAvgAttributeValueByAttributeId_" + attributeId;

            if (Cache[key] != null)
            {
                attributeValue = (AttributeValue)Cache[key];
            }
            else
            {
                attributeValue = DataAccess.Attributes.GetAvgAttributeValueByAttributeId(attributeId);
                CacheData(key, attributeValue);
            }
            return attributeValue;
        }

        public static int CountAttributeValues(Guid attributeId)
        {
            return DataAccess.Attributes.CountAttributeValues(attributeId);
        }

        public static Attribute GetAttributeByAttributeId(Guid attributeId)
        {
            Attribute attribute = null;
            string key = "Attributes_GetAttributeByAttributeId_" + attributeId;

            if (Cache[key] != null)
            {
                attribute = (Attribute) Cache[key];
            }
            else
            {
                attribute = DataAccess.Attributes.GetAttributeByAttributeId(attributeId);
                CacheData(key, attribute);
            }
            return attribute;
        }

        public static List<AttributeValue> GetAttributeValuesByAttributeId(Guid attributeId)
        {
            List<AttributeValue> attributeValues = null;
            string key = "Attributes_GetAttributeValuesByAttributeId_" + attributeId;

            if (Cache[key] != null)
            {
                attributeValues = (List<AttributeValue>)Cache[key];
            }
            else
            {
                attributeValues = DataAccess.Attributes.GetAttributeValuesByAttributeId(attributeId);
                CacheData(key, attributeValues);
            }
            return attributeValues;
        }

        //public static Attribute InsertAttribute(Attribute attribute)
        //{
        //    RemoveFromCache("Attributes_");
        //    int attributeId = DataAccess.Attributes.InsertAttribute(attribute);
        //    attribute = DataAccess.Attributes.GetAttributeByAttributeId(attributeId);
        //    return attribute;
        //}

        public static bool UpdateAttribute(Attribute attribute)
        {
            RemoveFromCache("Attributes_");
            return DataAccess.Attributes.UpdateAttribute(attribute);
        }

        public static bool DeleteAttributeByAttributeId(Guid attributeId)
        {
            RemoveFromCache("Attributes_");
            return DataAccess.Attributes.DeleteAttributeByAttributeId(attributeId);
        }

        public static bool DeleteAttributeValueByAttributeId(Guid attributeId)
        {
            RemoveFromCache("Attributes_");
            return DataAccess.Attributes.DeleteAttributeValueByAttributeId(attributeId);
        }
    }
}