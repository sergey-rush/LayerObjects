using System;

namespace LOB.Core
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Guid OrderItemUid { get; set; }
        public int OrderId { get; set; }
        public Guid OrderUid { get; set; }
        public Guid ProductUid { get; set; }
        public int ProductId { get; set; }
        public int SectionId { get; set; }
        public ItemState ItemState { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public string Barcode { get; set; }
        public string Sku { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public DateTime Updated { get; set; }
        public DateTime Created { get; set; }
    }
}
