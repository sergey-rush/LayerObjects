using System;
using Newtonsoft.Json;

namespace LOB.Core
{
    public class Route
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ShopId { get; set; }
        public int SkedId { get; set; }
        public int DayOfYear { get; set; }
        public int Duration { get; set; }
        public int VehicleId { get; set; }
        public string VehicleName { get; set; }
        public ItemState ItemState { get; set; }
        public int YearNum { get; set; }
        /// <summary>
        /// East-West position of a point
        /// </summary>
        public double Lng { get; set; }
        /// <summary>
        /// North–South  position of a point
        /// </summary>
        public double Lat { get; set; }
        public bool Assigned { get; set; }
    }
}