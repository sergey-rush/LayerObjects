using System;
using System.Collections.Generic;
using System.Data;
using LOB.Core;
using Attribute = LOB.Core.Attribute;

namespace LOB.Data
{
    public abstract class AttributeManager: DataAccess
    {
        private static AttributeManager _instance;

        public static AttributeManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AttributeProvider();
                }
                return _instance;
            }
        }

        public abstract List<Attribute> GetPagedAttributes(int pageIndex, int pageSize, Guid elementTypeId);
        public abstract int CountAttributes(Guid elementTypeId);
        public abstract List<AttributeValue> GetPagedAttributeValues(int pageIndex, int pageSize, Guid attributeId);
        public abstract AttributeValue GetAvgAttributeValueByAttributeId(Guid attributeId);
        public abstract int CountAttributeValues(Guid attributeId);
        public abstract Attribute GetAttributeByAttributeId(Guid attribute);
        public abstract List<AttributeValue> GetAttributeValuesByAttributeId(Guid attributeId);
        public abstract int InsertAttribute(Attribute attribute);
        public abstract bool UpdateAttribute(Attribute attribute);
        public abstract bool DeleteAttributeByAttributeId(Guid attributeId);
        public abstract bool DeleteAttributeValueByAttributeId(Guid attributeId);
        protected virtual Attribute GetAttributeFromReader(IDataReader reader)
        {
            Attribute attribute = new Attribute()
            {
                Id = (Guid)reader["Id"],
                AttributeTypeId = (Guid)reader["AttributeTypeId"],
                Description = reader["Description"].ToString(),
                Caption = reader["Caption"].ToString(),
                Searchable = (bool)reader["Searchable"]
            };
            if (reader["Code"] != DBNull.Value)
            {
                attribute.Code = reader["Code"].ToString();
            }
            return attribute;
        }
       
        protected virtual List<Attribute> GetAttributeCollectionFromReader(IDataReader reader)
        {
            List<Attribute> items = new List<Attribute>();
            while (reader.Read())
                items.Add(GetAttributeFromReader(reader));
            return items;
        }

        protected virtual AttributeValue GetAttributeValueFromReader(IDataReader reader)
        {
            AttributeValue attribute = new AttributeValue();

            if (reader["AttributeId"] != DBNull.Value)
            {
                attribute.AttributeId = (Guid) reader["AttributeId"];
            }

            if (reader["ElementId"] != DBNull.Value)
            {
                attribute.ElementId = (Guid)reader["ElementId"];
            }

            if (reader["Value"] != DBNull.Value)
            {
                attribute.Value = reader["Value"].ToString();
            }

            if (reader.FieldCount > 3)
            {
                if (reader["Caption"] != DBNull.Value)
                {
                    attribute.Caption = reader["Caption"].ToString();
                }

                if (reader["Longitude"] != DBNull.Value)
                {
                    attribute.Longitude = Convert.ToDouble(reader["Longitude"]);
                }

                if (reader["Latitude"] != DBNull.Value)
                {
                    attribute.Latitude = Convert.ToDouble(reader["Latitude"]);
                }
            }

            return attribute;
        }

        protected virtual List<AttributeValue> GetAttributeValueCollectionFromReader(IDataReader reader)
        {
            List<AttributeValue> items = new List<AttributeValue>();
            while (reader.Read())
                items.Add(GetAttributeValueFromReader(reader));
            return items;
        }
    }
}