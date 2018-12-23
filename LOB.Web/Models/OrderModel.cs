using System;
using System.Collections.Generic;
using LOB.Core;

namespace LOB.Web.Models
{
    public class OrderModel
    {
        public Guid OrderUid { get; set; }
        public ItemState ItemState { get; set; }
        public Customer Customer { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string DeliveryAddress { get; set; }
        public string Comments { get; set; }
        public int ShopId { get; set; }
        public Route Route { get; set; }
        public List<OrderItem> OrderItems { get; set; } 
    }
}