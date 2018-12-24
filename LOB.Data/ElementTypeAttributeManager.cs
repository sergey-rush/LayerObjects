using System;
using System.Collections.Generic;
using System.Data;
using LOB.Core;

namespace LOB.Data
{
    public abstract class ElementTypeAttributeManager: DataAccess
    {
        private static ElementTypeAttributeManager _instance;

        public static ElementTypeAttributeManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ElementTypeAttributeProvider();
                }
                return _instance;
            }
        }

        public abstract List<ElementTypeAttribute> GetElementTypeAttributesByElementTypeId(Guid elementTypeId);
        public abstract int CountElementTypeAttributes(Guid elementTypeId);
        public abstract List<ElementTypeAttribute> GetAttributesByElementTypeId(Guid elementTypeId);
        public abstract int InsertElementTypeAttribute(ElementTypeAttribute elementTypeAttribute);
        public abstract bool UpdateElementTypeAttribute(ElementTypeAttribute elementTypeAttribute);
        public abstract bool DeleteElementTypeAttributeByAttributeId(Guid attributeId);
        public abstract bool DeleteElementTypeAttributesByElementTypeId(Guid elementTypeId);
        protected virtual ElementTypeAttribute GetElementTypeAttributeFromReader(IDataReader reader)
        {
            ElementTypeAttribute elementTypeAttribute = new ElementTypeAttribute()
            {
                AttributeId = (Guid)reader["AttributeId"],
                ElementTypeId = (Guid)reader["ElementTypeId"],
                Mandatory = (bool)reader["Mandatory"],
                Visibility = (bool)reader["UnVisible"]
            };
            return elementTypeAttribute;
        }
       
        protected virtual List<ElementTypeAttribute> GetElementTypeAttributeCollectionFromReader(IDataReader reader)
        {
            List<ElementTypeAttribute> items = new List<ElementTypeAttribute>();
            while (reader.Read())
                items.Add(GetElementTypeAttributeFromReader(reader));
            return items;
        }

        protected virtual ElementTypeAttribute GetAttributeFromReader(IDataReader reader)
        {
            ElementTypeAttribute elementTypeAttribute = new ElementTypeAttribute()
            {
                AttributeId = (Guid)reader["AttributeId"],
                ElementTypeId = (Guid)reader["ElementTypeId"],
                Mandatory = (bool)reader["Mandatory"],
                Visibility = (bool)reader["UnVisible"],
                Id = (Guid)reader["Id"],
                AttributeTypeId = (Guid)reader["AttributeTypeId"],
                Description = reader["Description"].ToString(),
                Caption = reader["Caption"].ToString(),
                Title = reader["Title"].ToString(),
                AttributeType = reader["AttributeType"].ToString(),
                Searchable = (bool)reader["Searchable"]
            };

            if (reader["Code"] != DBNull.Value)
            {
                elementTypeAttribute.Code = reader["Code"].ToString();
            }
        return elementTypeAttribute;
        }

        protected virtual List<ElementTypeAttribute> GetAttributeCollectionFromReader(IDataReader reader)
        {
            List<ElementTypeAttribute> items = new List<ElementTypeAttribute>();
            while (reader.Read())
                items.Add(GetAttributeFromReader(reader));
            return items;
        }
    }
}