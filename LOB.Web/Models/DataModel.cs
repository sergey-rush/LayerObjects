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
        public List<ElementType> ElementTypes { get; set; }
        public ElementType SelectedElementType { get; set; }
        public List<ElementTypeAttribute> ElementTypeAttributes { get; set; }
        public ElementTypeAttribute SelectedElementTypeAttribute { get; set; }
        public List<AttributeValue> AttributeValues { get; set; }
        public AttributeValue SelectedAttributeValue { get; set; }


        public List<Contact> Contacts { get; set; }
        public Contact SelectedContact { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public OrderItem SelectedOrderItem { get; set; }
        public List<Product> RelatedProducts { get; set; }
        public List<Product> Products { get; set; }
        public Product SelectedProduct { get; set; }
        public List<Log> Logs { get; set; }
        public Log SelectedLog { get; set; }
        public List<Role> Roles { get; set; }
        public Role SelectedRole { get; set; }
        public List<User> Users { get; set; }
        public User SelectedUser { get; set; }
        public List<Sked> Skeds { get; set; }
        public Sked SelectedSked { get; set; }
        public List<Route> Routes { get; set; }
        public Route SelectedRoute { get; set; }
        public List<Position> Positions { get; set; }
        public Position SelectedPosition { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public Vehicle SelectedVehicle { get; set; }
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