using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quad_Shop.Models
{
    public class ShoppingCart
    {
        QuadContext dbtest = new QuadContext();
        string ShoppingCartID { get; set; }
        public const string CartSessionID = "CartID";
        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartID = cart.GetCartID(context);
            return cart;
        }
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }
        public string GetCartID(HttpContextBase context)
        {
            if (context.Session[CartSessionID] == null)
            {
                Guid TempCartID = Guid.NewGuid();
                context.Session[CartSessionID] = TempCartID;
            }
            return context.Session[CartSessionID].ToString();
        }
        public void AddToCart(ATVDescription atvdescription)
        {
            var cartitem = dbtest.Carts.SingleOrDefault(
                c => c.CartSessionID == ShoppingCartID && c.ATVDescriptionID == atvdescription.ATVDescriptionID);
            if (cartitem == null)
            {
                cartitem = new Cart
                {
                    CartSessionID = ShoppingCartID,
                    ATVDescriptionID = atvdescription.ATVDescriptionID,
                    Count = 1
                };
                dbtest.Carts.Add(cartitem);
            }
            else
            {
                cartitem.Count++;
            }
            dbtest.SaveChanges();
        }

        public void RemoveFromCart(int id)
        {
            var removeitem = dbtest.Carts.Single(
                c => c.CartSessionID == ShoppingCartID && c.CartID == id);
            if (removeitem != null)
            {
                if (removeitem.Count > 1)
                {
                    removeitem.Count--;
                }
                else
                {
                    dbtest.Carts.Remove(removeitem);
                }
            }
            dbtest.SaveChanges();
        }

        public void EmptyCart()
        {
            var cartitem = dbtest.Carts.Where(p => p.CartSessionID == ShoppingCartID);
            foreach (var o in cartitem)
            {
                dbtest.Carts.Remove(o);
            }
            dbtest.SaveChanges();
        }
        public List<Cart> GetCartItems()
        {
            return dbtest.Carts.Where(cart => cart.CartSessionID == ShoppingCartID).ToList();
        }
    }
}
