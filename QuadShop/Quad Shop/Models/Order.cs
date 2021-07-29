using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quad_Shop.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Middle_Name { get; set; }
        public decimal Total_Price { get; set; }
        public DateTime Order_Date { get; set; }

        public ICollection<OrdersDetail> OrdersDetails { get; set; }
        public Order()
        {
            OrdersDetails = new List<OrdersDetail>();
        }
    }
}