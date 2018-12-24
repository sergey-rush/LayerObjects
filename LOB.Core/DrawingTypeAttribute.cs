using System;
using Newtonsoft.Json;

namespace LOB.Core
{
    public class DrawingTypeAttribute
    {
        public Guid Id { get; set; }
        public Guid DrawingTypeId { get; set; }
        public Guid AttributeId { get; set; }
        public string Code { get; set; }
    }
}