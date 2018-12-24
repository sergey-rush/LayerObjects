using System;
using Newtonsoft.Json;

namespace LOB.Core
{
    public class LayerElementType
    {
        public Guid Id { get; set; }
        public Guid LayerId { get; set; }
        public Guid ElementTypeId { get; set; }
    }
}