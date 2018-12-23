using System;

namespace LOB.Core
{
    public class ElementType
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public Guid DrawingTypeId { get; set; }
        public string Caption { get; set; }
        public int AttributesCount { get; set; }
    }
}