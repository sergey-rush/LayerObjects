using System.Collections.Generic;
using LOB.Core;

namespace LOB.Web.Models
{
    public class ProductModel
    {
        public List<Product> Products { get; set; }

        public Product Product { get; set; }
    }
}