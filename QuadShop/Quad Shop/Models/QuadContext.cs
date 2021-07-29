using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Quad_Shop.Models
{
    public class QuadContext: DbContext
    {
        public DbSet<ATVBrand> ATVBrands { get; set; }
        public DbSet<ATVDescription> ATVDescriptions { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrdersDetail> OrdersDetails { get; set; }
    }
}