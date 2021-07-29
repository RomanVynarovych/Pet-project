using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quad_Shop.Models
{
    public class OrdersDetail
    {
        public int OrdersDetailID { get; set; }
        public int OrderID { get; set; }
        public int ATVDescriptionID { get; set; }
        public int Quantity { get; set; }
        public decimal Unit_Price { get; set; }
        public Order Order { get; set; }
        public ATVDescription ATVDescription { get; set; }
    }
}