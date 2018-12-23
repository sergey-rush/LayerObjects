using System;

namespace LOB.Core
{
    public class ElementTypeAttribute
    {
        public Guid AttributeId { get; set; }
        public Guid ElementTypeId { get; set; }
        public bool Mandatory { get; set; }
        public bool Visibility { get; set; }
        public Guid Id { get; set; }
        public Guid AttributeTypeId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Caption { get; set; }
        public string Title { get; set; }
        public string AttributeType { get; set; }
        public int AttrValueCount { get; set; }
        public bool Searchable { get; set; }
    }
}