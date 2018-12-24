using System.Collections.Generic;
using LOB.Core;

namespace LOB.Web.Models
{
    public class ProductModel
    {
        public List<DrawingTypeAttribute> Products { get; set; }

        public DrawingTypeAttribute Product { get; set; }
    }
}