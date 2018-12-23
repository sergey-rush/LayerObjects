using System;
using Newtonsoft.Json;

namespace LOB.Core
{
    public class Vehicle
    {
        public int Id { get; set; }
        public Guid VehicleUid { get; set; }
        public string Name { get; set; }
        public string Plate { get; set; }
        public int ShopId { get; set; }
        public ItemState ItemState { get; set; }
        public int UserId { get; set; }
    }
}