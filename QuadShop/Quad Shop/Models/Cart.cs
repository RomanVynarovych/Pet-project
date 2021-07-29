using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quad_Shop.Models
{
    public class Cart
    {
        public int CartID { get; set; }
        public string CartSessionID { get; set; }
        public int ATVDescriptionID { get; set; }
        public int Count { get; set; }
        public ATVDescription ATVDescription { get; set; }
    }
}