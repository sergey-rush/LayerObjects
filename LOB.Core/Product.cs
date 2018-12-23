using System;
using Newtonsoft.Json;

namespace LOB.Core
{
    public class Product
    {
        public int Id { get; set; }
        public Guid ProductUid { get; set; }
        public int SectionId { get; set; }
        public int ShopId { get; set; }
        public ItemState ItemState { get; set; }
        public int Stock { get; set; }
        public int UnitQty { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Pack { get; set; }
        public string Sku { get; set; }
        public string Info { get; set; }
        public string Weight { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string ImageUrl { get; set; }
        public string Attr { get; set; }
        public DateTime Updated { get; set; }
    }
}