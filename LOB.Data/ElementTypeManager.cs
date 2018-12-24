using System;
using System.Collections.Generic;
using System.Data;
using LOB.Core;

namespace LOB.Data
{
    public abstract class ElementTypeManager: DataAccess
    {
        private static ElementTypeManager _instance;

        public static ElementTypeManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ElementTypeProvider();
                }
                return _instance;
            }
        }

        public abstract List<ElementType> GetElementTypes(string query);
        public abstract int CountElementTypes(string query);
        public abstract List<Element> GetElementsByElementTypeId(Guid elementTypeId);
        public abstract ElementType GetElementTypeByElementTypeId(Guid elementType);
        public abstract Guid InsertElementType(ElementType elementType);
        public abstract bool UpdateElementType(ElementType elementType);
        public abstract bool DeleteElementTypeByElementTypeId(Guid elementTypeId);
        public abstract bool DeleteElementsByElementTypeId(Guid elementTypeId);
        protected virtual ElementType GetElementTypeFromReader(IDataReader reader)
        {
            ElementType elementType = new ElementType()
            {
                Id = (Guid)reader["Id"],
                DrawingTypeId = (Guid)reader["DrawingTypeId"],
                Caption = reader["Caption"].ToString()
            };

            if (reader["Code"] != DBNull.Value)
            {
                elementType.Code = reader["Code"].ToString();
            }
            return elementType;
        }
       
        protected virtual List<ElementType> GetElementTypeCollectionFromReader(IDataReader reader)
        {
            List<ElementType> items = new List<ElementType>();
            while (reader.Read())
                items.Add(GetElementTypeFromReader(reader));
            return items;
        }

        protected virtual Element GetElementFromReader(IDataReader reader)
        {
            Element elementType = new Element()
            {
                Id = (Guid)reader["Id"],
                ElementTypeId = (Guid)reader["ElementTypeId"],
                Caption = reader["Caption"].ToString()
            };
            
            return elementType;
        }

        protected virtual List<Element> GetElementCollectionFromReader(IDataReader reader)
        {
            List<Element> items = new List<Element>();
            while (reader.Read())
                items.Add(GetElementFromReader(reader));
            return items;
        }
    }
}