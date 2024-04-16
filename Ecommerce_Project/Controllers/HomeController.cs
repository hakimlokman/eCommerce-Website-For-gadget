using Ecommerce_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Ecommerce_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly  eCommerceDbContext _context;

        public HomeController(eCommerceDbContext _context)
        {
            this._context = _context;
        }


        public IActionResult Index()
        {
            ViewBag.msg = TempData["msg"];
            return View(_context.Products.ToList());
        }

        public IActionResult AddToCart(int pid, double qty)
        {
            bool itemFound= false;
            string msg = "";
            if(pid >0 && qty > 0)
            {
                var prod= _context.Products.FirstOrDefault(p=>p.Id == pid);
                if(prod!=null)
                {
                    List<Products> items = new List<Products>();
                    var xItems = HttpContext.Session.GetObject<List<Products>>("cart");
                    if(xItems!=null)
                    {
                        foreach (var item in xItems)
                        {
                            if (pid == item.Id)
                            {
                                item.Quentity += qty;
                                itemFound= true;
                                msg = "Item already addes , new quentity is addes !!";
                            }
                            items.Add(item);
                        }
                        if(!itemFound)
                        {
                            prod.Quentity = qty;
                            items.Add(prod); 
                            msg = "New item is added with existing items";
                        }
                        HttpContext.Session.SetObject<List<Products>>("cart", items);
                    }
                    else
                    {
                        prod.Quentity = qty;
                        items.Add(prod);
                        HttpContext.Session.SetObject<List<Products>>("cart", items);
                        msg = "New item is added to empty cart !!";
                    }
                }
                else
                {
                    msg = "Item not found !!";
                }
            }
            else
            {
                msg = "Item id or qty could not be found !!";
            }
            TempData["msg"] = msg;
            return RedirectToAction("Index");
        }
        public IActionResult showCart()
        {
            List<Products> items= HttpContext.Session.GetObject<List<Products>>("cart");
            if(items!=null && items.Count !=0)
            {
                return View(items.ToList());
            }
            else
            {
                items = new List<Products>();
                return View();

            }
        }

        public IActionResult RemoveFromCart(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            List<Products> productList = HttpContext.Session.GetObject<List<Products>>("cart");
            var removeItem=productList.FirstOrDefault(x=>x.Id==id);
            productList.Remove(removeItem);
            HttpContext.Session.SetObject("cart", productList);
            return RedirectToAction("showCart");

        }
        public IActionResult Checkout()
        {
            var us = HttpContext.Session.GetString("un");
            var id = HttpContext.Session.GetString("id");
            if (us != null)
            {
                return RedirectToAction("ConfirmOrder");
            }
            else
            {
                return View(nameof(Login));
            }

        }
        public IActionResult ConfirmOrder()
        {
            return View();
        }
        public IActionResult Login(AppUser appUser)
        {
            var userName= _context.AppUsers.FirstOrDefault(x=>x.Username== appUser.Username);
            var password = _context.AppUsers.FirstOrDefault(x => x.Password == appUser.Password);
            if(userName !=null && password != null)
            {
                HttpContext.Session.SetString("un",appUser.Username);
                HttpContext.Session.SetString("id", appUser.Password);
                return RedirectToAction(nameof(ConfirmOrder));
            }
            else
            {
                TempData["wrong"] = "Invalid Login !!";
                return View(appUser);
            }
            
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp([Bind("Username","Password")] AppUser appUser)
        {
            string msg = "";
            if (ModelState.IsValid)
            {
                appUser.Role = 1;
                appUser.IsActive = true;

                var checkUserName= _context.AppUsers.FirstOrDefault(x=>x.Username.ToLower() == appUser.Username.ToLower());
                if(checkUserName != null)
                {
                    TempData["signUp"] = "User name already exist";
                    return View(appUser);
                }
                _context.AppUsers.Add(appUser);
                await _context.SaveChangesAsync();

                HttpContext.Session.SetString("un",appUser.Username);
                HttpContext.Session.SetString("id", appUser.Password);
                appUser.Username = null;
                appUser.Password = null;
                TempData["signUp"] = "Successfully created an account !";
                return RedirectToAction("Login");

            }
            else
            {
                msg = "Invalid user name or password";
                return View(appUser);
            }
           
        }
    }
}