using System;
using Newtonsoft.Json;

namespace LOB.Core
{
    public class LayerConditionsElement
    {
        public Guid LayerId { get; set; }
        public Guid ElementId { get; set; }
        public int TypeComparer { get; set; }
    }
}