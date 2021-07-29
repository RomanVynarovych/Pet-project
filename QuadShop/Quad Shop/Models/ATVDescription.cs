using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quad_Shop.Models
{
    public class ATVDescription
    {
		public int ATVDescriptionID { get; set; }
		public int ATVBrandID { get; set; }
		public string Model { get; set; }
		public decimal Price { get; set; }
		public string Engine_Type { get; set; }
		public int Cylinder_Displacement { get; set; }
		public string Fuel_System { get; set; }
		public string Cooling { get; set; }
		public string Transmission { get; set; }
		public string Drive_System { get; set; }
		public string Locking_Differencial { get; set; }
		public string Front_Suspension { get; set; }
		public string Rear_Suspension { get; set; }
		public string Front_Brakes { get; set; }
		public string Rear_Brakes { get; set; }
		public string Parking_Brake { get; set; }
		public string Wheels { get; set; }
		public decimal Length { get; set; }
		public decimal Width { get; set; }
		public decimal Hight { get; set; }
		public decimal Weight { get; set; }
		public int Stock { get; set; }
		public decimal Fuel_Capacity { get; set; }
		public string Instrumentation { get; set; }
		public string Brake_Lights { get; set; }
		public string Head_Lights { get; set; }
		public string Turn_Signal { get; set; }
		public string Horn { get; set; }
		public string Cargo_Rocks { get; set; }
		public string Color { get; set; }
		public string Image { get; set; }
		public int Warranty { get; set; }

		public ATVBrand ATVBrand { get; set; }

		public ICollection<Cart> Carts { get; set; }
		public ICollection<OrdersDetail> OrdersDetails { get; set; }
		public ATVDescription()
		{
			Carts = new List<Cart>();
			OrdersDetails = new List<OrdersDetail>();
		}
	}
}