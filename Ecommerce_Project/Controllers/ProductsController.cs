using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ecommerce_Project.Models;
using Ecommerce_Project.Models.ViewModels;

namespace Ecommerce_Project.Controllers
{
    public class ProductsController : Controller
    {
        private readonly eCommerceDbContext _context;
        private readonly IWebHostEnvironment _he;

        public ProductsController(eCommerceDbContext context, IWebHostEnvironment he)
        {
            _context = context;
            _he = he;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
              return _context.Products != null ? 
                          View(await _context.Products.ToListAsync()) :
                          Problem("Entity set 'eCommerceDbContext.Products'  is null.");
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                Products products = new Products()
                {
                    Name = productVM.Name,
                    Unit = productVM.Unit,
                    Price = productVM.Price,
                    Quentity = productVM.Quentity,
                };

                //image
                var file = productVM.PictureFile;
                string webroot = _he.WebRootPath;
                string folder = "Images";
                string fileName= Path.GetFileName(productVM.PictureFile.FileName);
                string fileToSave= Path.Combine(webroot,folder, fileName);
                if(file != null)
                {
                    using(var stream = new FileStream(fileToSave, FileMode.Create))
                    {
                        productVM.PictureFile.CopyTo(stream);
                        products.Picture= "/" + folder + "/" + fileName;
                    }
                }
                _context.Products.Add(products);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x=>x.Id==id);
            ProductVM productVM = new ProductVM()
            {
                Id = product.Id,
                Name = product.Name,
                Unit = product.Unit,
                Price = product.Price,
                Quentity = product.Quentity,
                Picture = product.Picture
            };
            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                Products products = new Products()
                {
                    Id= productVM.Id,
                    Name = productVM.Name,
                    Unit = productVM.Unit,
                    Price = productVM.Price,
                    Quentity = productVM.Quentity,
                };

                //image
                var file = productVM.PictureFile;
                
                if (file != null)
                {
                    string webroot = _he.WebRootPath;
                    string folder = "Images";
                    string fileName = Path.GetFileName(productVM.PictureFile.FileName);
                    string fileToSave = Path.Combine(webroot, folder, fileName);
                    using (var stream = new FileStream(fileToSave, FileMode.Create))
                    {
                        productVM.PictureFile.CopyTo(stream);
                        products.Picture = "/" + folder + "/" + fileName;
                    }
                }
                else
                {
                    products.Picture = productVM.Picture;
                }
                _context.Update(products);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int id)
        {
            var products = _context.Products.Find(id);
            if (products != null)
            {
             
                _context.Products.Remove(products);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }

}

