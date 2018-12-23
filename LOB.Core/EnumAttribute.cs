using System;
using Attribute = System.Attribute;
namespace LOB.Core
{
    public class EnumAttribute : System.Attribute
    {
        private string _value;
        public EnumAttribute(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }
    }
}