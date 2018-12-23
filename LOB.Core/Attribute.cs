using System;

namespace LOB.Core
{
    public class Attribute
    {
        public Guid Id { get; set; }
        public Guid AttributeTypeId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Caption { get; set; }
        public bool Searchable { get; set; }
    }
}