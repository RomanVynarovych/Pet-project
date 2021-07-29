using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Quad_Shop.Models;
using System.IO;

namespace Quad_Shop.Controllers
{
    public class HomeController : Controller
    {
        QuadContext dbcontext = new QuadContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ShowProduct(string category = "null", string cylinderdisplacementfrom = "null", string cylinderdisplacementto = "null",
                                        string price_from = "null", string price_to = "null", int page = 1, string sort = "null")
        {
            ViewBag.lol = sort;
            int intcylinder_displacement_from = 0;
            int intcylinder_displacement_to = 0;
            decimal intprice_from = 0;
            decimal intprice_to = 0;
            if(cylinderdisplacementfrom == "null")
            {
                var b = dbcontext.ATVDescriptions.OrderBy(p => p.Cylinder_Displacement).FirstOrDefault();
                intcylinder_displacement_from = b.Cylinder_Displacement;
            }
            else
            {
                intcylinder_displacement_from = int.Parse(cylinderdisplacementfrom);
            }
            if (cylinderdisplacementto == "null")
            {
                var b = dbcontext.ATVDescriptions.OrderByDescending(p => p.Cylinder_Displacement).FirstOrDefault();
                intcylinder_displacement_to = b.Cylinder_Displacement;
            }
            else
            {
                intcylinder_displacement_to = int.Parse(cylinderdisplacementto);
            }
            if (price_from == "null")
            {
                var b = dbcontext.ATVDescriptions.OrderBy(p => p.Price).FirstOrDefault();
                intprice_from = b.Price;
            }
            else
            {
                intprice_from = decimal.Parse(price_from);
            }
            if (price_to == "null")
            {
                var b = dbcontext.ATVDescriptions.OrderByDescending(p => p.Price).FirstOrDefault();
                intprice_to = b.Price;
            }
            else
            {
                intprice_to = decimal.Parse(price_to);
            }
            ViewBag.intcylinder_displacement_from = intcylinder_displacement_from.ToString();
            ViewBag.intcylinder_displacement_to = intcylinder_displacement_to.ToString();
            ViewBag.intprice_from = intprice_from.ToString();
            ViewBag.intprice_to = intprice_to.ToString();
            ViewBag.category = category;
            int pageSize = 2;
            Pagelist list = new Pagelist();
            if(category == "null")
            {
                if (cylinderdisplacementfrom == "null" && cylinderdisplacementto == "null" && price_from == "null" && price_to == "null")
                {
                    list.ATVDescriptions = dbcontext.ATVDescriptions
                    .Include(ATV => ATV.ATVBrand)
                    .OrderBy(ATV => ATV.ATVDescriptionID)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);
                    list.pageinfo = new Pageinfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = dbcontext.ATVDescriptions.Count()
                    };
                }
                else
                {

                    list.ATVDescriptions = dbcontext.ATVDescriptions
                    .Include(ATV => ATV.ATVBrand)
                    .Where(ATV =>  ATV.Cylinder_Displacement >= intcylinder_displacement_from
                                                                   && ATV.Cylinder_Displacement <= intcylinder_displacement_to
                                                                   && ATV.Price >= intprice_from
                                                                   && ATV.Price <= intprice_to)
                    .OrderBy(ATV => ATV.ATVDescriptionID)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);
                    list.pageinfo = new Pageinfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = dbcontext.ATVDescriptions
                            .Include(ATV => ATV.ATVBrand)
                            .Where(ATV =>  ATV.Cylinder_Displacement >= intcylinder_displacement_from
                                                                       && ATV.Cylinder_Displacement <= intcylinder_displacement_to
                                                                       && ATV.Price >= intprice_from
                                                                       && ATV.Price <= intprice_to).Count()
                    };
                }
            }
            else
            {
                if(cylinderdisplacementfrom == "null" && cylinderdisplacementto == "null" && price_from == "null" && price_to == "null")
                {
                    list.ATVDescriptions = dbcontext.ATVDescriptions
                    .Include(ATV => ATV.ATVBrand)
                    .Where(ATV =>  ATV.ATVBrand.Brand == category)
                    .OrderBy(ATV => ATV.ATVDescriptionID)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);
                    list.pageinfo = new Pageinfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = dbcontext.ATVDescriptions.Where(ATV => ATV.ATVBrand.Brand == category).Count()
                    };
                }
                else
                {
                    list.ATVDescriptions = dbcontext.ATVDescriptions
                    .Include(ATV => ATV.ATVBrand)
                    .Where(ATV => ATV.ATVBrand.Brand == category && ATV.Cylinder_Displacement >= intcylinder_displacement_from
                                                                   && ATV.Cylinder_Displacement <= intcylinder_displacement_to
                                                                   && ATV.Price >= intprice_from
                                                                   && ATV.Price <= intprice_to)
                    .OrderBy(ATV => ATV.ATVDescriptionID)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);
                    list.pageinfo = new Pageinfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = dbcontext.ATVDescriptions
                            .Include(ATV => ATV.ATVBrand)
                            .Where(ATV => ATV.ATVBrand.Brand == category && ATV.Cylinder_Displacement >= intcylinder_displacement_from
                                                                       && ATV.Cylinder_Displacement <= intcylinder_displacement_to
                                                                       && ATV.Price >= intprice_from
                                                                       && ATV.Price <= intprice_to).Count()
                    };
                }
            }

            SelectList sortlist = new SelectList(
                new List<SelectListItem> { 
                    new SelectListItem { Text = "All", Value = "0"},
                    new SelectListItem { Text = "priceup", Value = "1"},
                    new SelectListItem { Text = "pricedown", Value = "2"},
                    new SelectListItem { Text = "dscap", Value = "3"},
                    new SelectListItem { Text = "dscdown", Value = "4"}
                }, "Value", "Text", 0
            );
            ViewBag.brands = sortlist;

            return View(list);
        }
            
        public PartialViewResult ShowCategories(string category = null)
        {
            ViewBag.SelectedCategory = category;
            var categories = dbcontext.ATVBrands
                .Select(ATV => ATV.Brand)
                .Distinct()
                .OrderBy(x => x).ToList();
            
            return PartialView(categories);
        }

        public ActionResult AtvDetails(int atv_id)
        {
            var model = dbcontext.ATVDescriptions.Include(ATV => ATV.ATVBrand).Where(id => id.ATVDescriptionID == atv_id);
            return View(model);
        }

        public ActionResult EditList()
        {
            var list = dbcontext.ATVDescriptions.Include(ATV => ATV.ATVBrand);
            return View(list);
        }

        [HttpGet]
        public ActionResult ProductEditing(int? id)
        {
            if(id != null)
            {
                var atv = dbcontext.ATVDescriptions.Find(id);
                SelectList brands = new SelectList(dbcontext.ATVBrands, "ATVBrandID", "Brand", atv.ATVBrandID);
                ViewBag.brands = brands;
                return View(atv);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult ProductEditing(ATVDescription atv, HttpPostedFileBase uploadFile)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Images/Images/";
            string filename = null;
            if (uploadFile != null)
            {
                filename = Path.GetFileName(uploadFile.FileName);
            }
            if (uploadFile != null && filename != atv.Image)
            {
                System.IO.File.Delete(Path.Combine(path, atv.Image));
                uploadFile.SaveAs(Path.Combine(path, filename));
                atv.Image = filename;
            }
            dbcontext.Entry(atv).State = EntityState.Modified;
            dbcontext.SaveChanges();
            return RedirectToAction("EditList");
        }

        [HttpGet]
        public ViewResult AddNewATV()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewATV(ATVDescription atv, HttpPostedFileBase uploadFile, string brand)
        {
            ATVBrand atvbrand = new ATVBrand();
            string path = AppDomain.CurrentDomain.BaseDirectory + "Images/Images/";
            string filename = null;
            if(dbcontext.ATVBrands.Where(brandname => brandname.Brand == brand).FirstOrDefault() == null){

                atvbrand.Brand = brand;
                dbcontext.ATVBrands.Add(atvbrand);
                dbcontext.SaveChanges();
                if (uploadFile != null)
                {
                    filename = Path.GetFileName(uploadFile.FileName);
                }
                if (uploadFile != null && filename != atv.Image)
                {
                    uploadFile.SaveAs(Path.Combine(path, filename));
                    atv.Image = filename;
                }
                atvbrand = dbcontext.ATVBrands.Where(b => b.Brand == brand).First();
                atv.ATVBrandID = atvbrand.ATVBrandID;
                dbcontext.Entry(atv).State = EntityState.Added;
                dbcontext.SaveChanges();
            }
            else
            {
                if (uploadFile != null)
                {
                    filename = Path.GetFileName(uploadFile.FileName);
                }
                if (uploadFile != null && filename != atv.Image)
                {
                    uploadFile.SaveAs(Path.Combine(path, filename));
                    atv.Image = filename;
                }
                atvbrand = dbcontext.ATVBrands.Where(b => b.Brand == brand).First();
                atv.ATVBrandID = atvbrand.ATVBrandID;
                dbcontext.Entry(atv).State = EntityState.Added;
                dbcontext.SaveChanges();
            }
            return RedirectToAction("EditList");
        }

        public ActionResult DeleteATV (int id)
        {
            ATVDescription deleteitem = dbcontext.ATVDescriptions.Find(id);
            int branddelete = deleteitem.ATVBrandID;
            dbcontext.ATVDescriptions.Remove(deleteitem);
            dbcontext.SaveChanges();
            if(dbcontext.ATVDescriptions.Where(p => p.ATVBrandID == branddelete).FirstOrDefault() == null)
            {
                ATVBrand brand = dbcontext.ATVBrands.Find(branddelete);
                dbcontext.ATVBrands.Remove(brand);
                dbcontext.SaveChanges();
                return RedirectToAction("EditList");
            }
            else
            {
                return RedirectToAction("EditList");
            }
        }

        public ActionResult Cart()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            string kk = cart.GetCartID(this.HttpContext);
            if(dbcontext.Carts.Where(o => o.CartSessionID == kk).Count() == 0)
            {
                ViewBag.CartCount = 0;
            }
            else
            {
                ViewBag.CartCount = dbcontext.Carts.Where(o => o.CartSessionID == kk).Count();
                foreach(var b in dbcontext.Carts.Include(p => p.ATVDescription).Where(o => o.CartSessionID == kk))
                {
                    ViewBag.CartTotal = b.Count * b.ATVDescription.Price;
                }
            }
            return View(dbcontext.Carts.Include(p => p.ATVDescription).Include(i => i.ATVDescription.ATVBrand).Where(o => o.CartSessionID == kk));
        }
        public ActionResult AddToCart(int id)
        {
            var addeditem = dbcontext.ATVDescriptions.Single(item => item.ATVDescriptionID == id);
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.AddToCart(addeditem);
            return RedirectToAction("ShowProduct");
        }

        public ActionResult RemoveFromCart(int id)
        {
            var removeditem = dbcontext.Carts.Single(item => item.CartID == id);
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.RemoveFromCart(removeditem.CartID);
            return RedirectToAction("Cart");
        }

        public ActionResult CartTotalItems()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            var count = cart.GetCartItems();
            ViewBag.Count = count.Count().ToString();
            return PartialView("CartTotalItems");
        }

        public ActionResult ShowOrder()
        {
            return View(dbcontext.Orders);
        }

        public ActionResult ShowOrderDetails(int id)
        {
            ViewBag.OrderID = id;
            return View(dbcontext.OrdersDetails.Include(o => o.Order).Include(a => a.ATVDescription).Include(b => b.ATVDescription.ATVBrand).Where(oo => oo.OrderID == id));
        }

        public ActionResult ShowOrderDetailsPartial(int id)
        {
            var order = dbcontext.Orders.Single(p => p.OrderID == id);
            return PartialView(order);
        }

        [HttpGet]
        public ActionResult CreateOrder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateOrder(Order order)
        {
            order.Total_Price = 0;
            order.Order_Date = System.DateTime.Now;
            dbcontext.Orders.Add(order);
            dbcontext.SaveChanges();
            var ordermodified = dbcontext.Orders.SingleOrDefault(p => p.Total_Price == order.Total_Price && p.First_Name == order.First_Name);
            var cart = ShoppingCart.GetCart(this.HttpContext);
            string cartsessionid = cart.GetCartID(this.HttpContext);
            var cartitem = dbcontext.Carts.Include(p => p.ATVDescription).Include(i => i.ATVDescription.ATVBrand).Where(p => p.CartSessionID == cartsessionid);
            foreach (var b in cartitem)
            {
                var ordersdetails = new OrdersDetail
                {
                    OrderID = order.OrderID,
                    ATVDescriptionID = b.ATVDescriptionID,
                    Quantity = b.Count,
                    Unit_Price = b.ATVDescription.Price
                };
                dbcontext.OrdersDetails.Add(ordersdetails);
                ordermodified.Total_Price += b.Count * b.ATVDescription.Price;
            }
            dbcontext.Entry(ordermodified).State = EntityState.Modified;
            dbcontext.SaveChanges();
            cart.EmptyCart();
            return View();
        }
    }
}