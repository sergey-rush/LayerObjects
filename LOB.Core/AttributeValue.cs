using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LOB.Core
{
    public class AttributeValue
    {
        public Guid AttributeId { get; set; }
        public Guid ElementId { get; set; }
        public string Value { get; set; }
        public string Caption { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
