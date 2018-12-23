using System;
using System.Collections.Generic;
using LOB.Core;
using Newtonsoft.Json;

namespace LOB.Web.Models
{
    public class InfoModel
    {
        public ItemState ItemState { get; set; }
        public UserState UserState { get; set; }
        public ItemState PrevState { get; set; }
        public ItemState NewState { get; set; }
        public Guid UserUid { get; set; }
        public Guid PickUid { get; set; }
        public Guid PackUid { get; set; }
        public Guid PickItemUid { get; set; }
        public int Quantity { get; set; }
        public string Message { get; set; }
    }
}