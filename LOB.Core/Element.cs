using System;

namespace LOB.Core
{
    public class Element
    {
        public Guid Id { get; set; }
        public Guid ElementTypeId { get; set; }
        public string Caption { get; set; }
    }
}