using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quad_Shop.Models
{
    public class Pagelist
    {
        public IEnumerable<ATVDescription> ATVDescriptions { get; set; }
        public Pageinfo pageinfo { get; set; }
    }
}