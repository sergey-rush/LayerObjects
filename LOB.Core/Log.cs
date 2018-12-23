using System;
using Newtonsoft.Json;

namespace LOB.Core
{
    public class Log
    {
        public Log()
        {
            
        }

        public int Id { get; set; }
        public int ShopId { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int OrderItemId { get; set; }
        public int PickId { get; set; }
        public int PickItemId { get; set; }
        public int PackId { get; set; }
        public int PackItemId { get; set; }
        public ItemState PrevState { get; set; }
        public ItemState NewState { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public string Info { get; set; }
        public DateTime Created { get; set; }
        public Log(string info)
        {
            Info = info;
        }
        public Log(int userId, string info)
        {
            UserId = userId;
            Info = info;
        }
    }
}