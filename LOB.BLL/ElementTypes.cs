using System;
using System.Collections.Generic;
using System.Threading;
using LOB.Core;
using LOB.Data;
using Attribute = LOB.Core.Attribute;

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

        public static List<Element> GetElementsByElementTypeId(Guid elementTypeId)
        {
            List<Element> elements = null;
            
            string key = "ElementTypes_GetElementsByElementTypeId_" + elementTypeId;

            if (Cache[key] != null)
            {
                elements = (List<Element>)Cache[key];
            }
            else
            {
                elements = DataAccess.ElementTypes.GetElementsByElementTypeId(elementTypeId);
                CacheData(key, elements);
            }
            return elements;
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

        public static Guid InsertElementType(ElementType elementType)
        {
            RemoveFromCache("ElementTypes_");
            Guid elementTypeId = DataAccess.ElementTypes.InsertElementType(elementType);
            return elementTypeId;
        }

        public static bool UpdateElementType(ElementType elementType)
        {
            RemoveFromCache("ElementTypes_");
            return DataAccess.ElementTypes.UpdateElementType(elementType);
        }

        public static bool DeleteElementTypeByElementTypeId(Guid elementTypeId)
        {
            List<ElementTypeAttribute> elementTypeAttributes = ElementTypeAttributes.GetAttributesByElementTypeId(elementTypeId);

            List<LayerConditionsElement> layerConditionsElements = new List<LayerConditionsElement>();
            List<AttributeValue> attributeValues = new List<AttributeValue>();

            foreach (ElementTypeAttribute eta in elementTypeAttributes)
            {
                List<AttributeValue> attValues = Attributes.GetAttributeValuesByAttributeId(eta.AttributeId);

                foreach (AttributeValue av in attValues)
                {
                    if (av != null)
                    {
                        attributeValues.Add(av);
                    }
                }
            }

            List<Element> elements = GetElementsByElementTypeId(elementTypeId);
            foreach (Element el in elements)
            {
                List<LayerConditionsElement> layerElements = Layers.GetLayerConditionsElementsByElementId(el.Id);

                foreach (LayerConditionsElement lce in layerElements)
                {
                    if (lce != null)
                    {
                        layerConditionsElements.Add(lce);
                    }
                }
            }

            foreach (LayerConditionsElement lce in layerConditionsElements)
            {
                Layers.DeleteLayerConditionsElementsByElementId(lce.ElementId);
            }


            foreach (AttributeValue av in attributeValues)
            {
                Attributes.DeleteAttributeValueByAttributeId(av.AttributeId);
            }

            ElementTypeAttributes.DeleteElementTypeAttributesByElementTypeId(elementTypeId);

            List<DrawingTypeAttribute> drawingTypeAttributes = new List<DrawingTypeAttribute>();
            foreach (ElementTypeAttribute eta in elementTypeAttributes)
            {
                List<DrawingTypeAttribute> attValues = DrawingTypes.GetDrawingTypeAttributesByAttributeId(eta.AttributeId);

                foreach (DrawingTypeAttribute av in attValues)
                {
                    if (av != null)
                    {
                        drawingTypeAttributes.Add(av);
                    }
                }
            }

            foreach (DrawingTypeAttribute dta in drawingTypeAttributes)
            {
                DrawingTypes.DeleteDrawingTypeAttributeValueByDrawingTypeAttributeId(dta.Id);
            }

            List<Attribute> attributes = new List<Attribute>();
            foreach (ElementTypeAttribute eta in elementTypeAttributes)
            {
                Attribute attribute = Attributes.GetAttributeByAttributeId(eta.AttributeId);
                if (attribute != null)
                {
                    attributes.Add(attribute);
                }
            }

            List<LayerElementType> layerElementTypes = Layers.GetLayerElementTypesByElementTypeId(elementTypeId);
            foreach (LayerElementType let in layerElementTypes)
            {
                Layers.DeleteFilterByLayerElementTypeId(let.Id);
            }

            Layers.DeleteLayerElementTypeByElementTypeId(elementTypeId);


            DrawingTypes.DeleteDrawingTypeAttributeValueByElementTypeId(elementTypeId);
            
            foreach (Attribute av in attributes)
            {
                Layers.DeleteFilterByAttributeId(av.Id);
                DrawingTypes.DeleteDrawingTypeAttributeByAttributeId(av.Id);
                ElementTypeAttributes.DeleteElementTypeAttributeByAttributeId(av.Id);
                Attributes.DeleteAttributeByAttributeId(av.Id);
            }

            Roles.DeleteRoleElementTypeByElementTypeId(elementTypeId);
            bool result = DataAccess.ElementTypes.DeleteElementTypeByElementTypeId(elementTypeId);
            RemoveFromCache("ElementTypes_");
            return result;
        }

        public static bool DeleteElementsByElementTypeId(Guid elementTypeId)
        {
            RemoveFromCache("ElementTypes_");
            return DataAccess.ElementTypes.DeleteElementsByElementTypeId(elementTypeId);
        }
    }
}