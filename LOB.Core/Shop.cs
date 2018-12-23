using System;
using Newtonsoft.Json;

namespace LOB.Core
{
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public ItemState ItemState { get; set; }
        public string Info { get; set; }
        public string Title { get; set; }
        public string Scope { get; set; }
        /// <summary>
        /// East-West position of a point
        /// </summary>
        public double Lng { get; set; }
        /// <summary>
        /// North–South  position of a point
        /// </summary>
        public double Lat { get; set; }
    }
}