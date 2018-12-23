using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOB.Core
{
    public class Picture
    {
        public int Id { get; set; }
        public Guid ProductUid { get; set; }
        public string Name { get; set; }
        public ImageSize ImageSize { get; set; }
        public int Main { get; set; }
    }
}
