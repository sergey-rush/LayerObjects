using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using LOB.Core;

namespace LOB.Web.Models
{
    public class DataModel
    {
        public string PageHeader { get; set; }
        public string Query { get; set; }
        public IList<SelectListItem> ConnectionStrings { get; set; }
        public string SelectedConnectionString { get; set; }
        public List<ElementType> ElementTypes { get; set; }
        public ElementType SelectedElementType { get; set; }
        public List<ElementTypeAttribute> ElementTypeAttributes { get; set; }
        public ElementTypeAttribute SelectedElementTypeAttribute { get; set; }
        public List<AttributeValue> AttributeValues { get; set; }
        public AttributeValue SelectedAttributeValue { get; set; }
        public List<DrawingType> DrawingTypes { get; set; }
        public DrawingType SelectedDrawingType { get; set; }


        public List<Contact> Contacts { get; set; }
        public Contact SelectedContact { get; set; }
        public List<DrawingTypeAttribute> RelatedProducts { get; set; }
        public List<DrawingTypeAttribute> Products { get; set; }
        public DrawingTypeAttribute SelectedProduct { get; set; }
        public List<Log> Logs { get; set; }
        public Log SelectedLog { get; set; }
        public List<Role> Roles { get; set; }
        public Role SelectedRole { get; set; }
        public List<User> Users { get; set; }
        public User SelectedUser { get; set; }
        public List<LayerConditionsElement> Skeds { get; set; }
        public LayerConditionsElement SelectedSked { get; set; }
        public List<LayerElementType> Routes { get; set; }
        public LayerElementType SelectedRoute { get; set; }
        public List<Element> Positions { get; set; }
        public Element SelectedPosition { get; set; }
        public List<RoleElementType> Vehicles { get; set; }
        public RoleElementType SelectedVehicle { get; set; }
        public List<Request> Requests { get; set; }
        public Request SelectedRequest { get; set; }
        public DateTime SelectedDate { get; set; }
        public List<DateTime> Dates { get; set; }
        public int Count { get; set; }
        public IList<SelectListItem> Items { get; set; }
        public SelectListItem SelectedItem { get; set; }
        public Pager Pager { get; set; }
    }
}