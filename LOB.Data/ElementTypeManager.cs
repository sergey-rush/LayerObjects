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
        public abstract List<ElementType> GetRandomElementTypes(int count);
        public abstract ElementType GetElementTypeByElementTypeId(Guid elementType);
        public abstract int InsertElementType(ElementType elementType);
        public abstract bool UpdateElementType(ElementType elementType);
        public abstract bool DeleteElementTypeByElementTypeId(int elementTypeId);
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

       
    }
}