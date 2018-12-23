using System;
using Newtonsoft.Json;

namespace LOB.Core
{
    public class Sked
    {
        public int Id { get; set; }
        public int ShopId { get; set; }
        [JsonIgnore]
        public string ShopName { get; set; }
        public int DayOfYear { get; set; }
        public int StartHour { get; set; }
        public int EndHour { get; set; }
        public ItemState ItemState { get; set; }
        public int YearNum { get; set; }
    }
}