using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using SimpleEcommerceAspNet6.Data;
using SimpleEcommerceAspNet6.Filter;
using SimpleEcommerceAspNet6.Helpper;
using SimpleEcommerceAspNet6.Models;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace SimpleEcommerceAspNet6.Areas.Manager.Controllers
{
    [Area("Manager")]
    [TypeFilter(typeof(CustomAuthorizeFilter))]
    //[Authorize(Roles ="Admin")]
    public class ManageProductsController : Controller
    {
        private readonly EcommerceDbContext _context;
        private readonly INotyfService _notyfService;

        public ManageProductsController(EcommerceDbContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Manager/ManageProducts
        public async Task<IActionResult> Index(int page = 1, int CategoryId = 0)
        { 
            int pageNumber = page;
            int pageSize = 10;
            Console.WriteLine("catid: " + CategoryId);
            List<Product> products = new List<Product>();
            if (CategoryId != 0)
            {
                products = _context.Products
                .AsNoTracking()
                .Where(x => x.CategoryId == CategoryId)
                .Include(x => x.category)
                .OrderBy(x => x.ProductId).ToList();
            }
            else
            {
                products = _context.Products
                .AsNoTracking()
                .Include(x => x.category)
                .OrderBy(x => x.ProductId).ToList();
            }

            PagedList<Product> models = new PagedList<Product>(products.AsQueryable(), pageNumber, pageSize);

            ViewBag.CurrentCateID = CategoryId;
            ViewBag.CurrentPage = pageNumber;
            ViewData["Categories"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View(models);
        }

        // GET: Manager/ManageProducts/Details/5
        public async Task<IActionResult> Details(int? ProductId)
        {
            if (ProductId == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.category)
                .FirstOrDefaultAsync(m => m.ProductId == ProductId);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Manager/ManageProducts/Create
        public IActionResult Create()
        {
            ViewData["Categories"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Manager/ManageProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,SortDescription,Description,CategoryId,Price,Discount,Thumbnail,CreateDate,ModifiedDate,HomeFlag,Active,UnitInStock")] Product product, IFormFile fileThumnail)
        {
            if (ModelState.IsValid)
            {
                product.ProductName = Utilities.ToTitleCase(product.ProductName);
                Console.WriteLine(product.ProductName);
                if (!ProductNameExists(product.ProductName))
                {
                    if (fileThumnail != null)
                    {
                        string extension = Path.GetExtension(fileThumnail.FileName);
                        string image = Utilities.SEOUrl(product.ProductName) + extension;
                        product.Thumbnail = await Utilities.UploadFile(fileThumnail, @"products", image.ToLower());
                    }
                    if (string.IsNullOrEmpty(product.Thumbnail)) product.Thumbnail = "default.jpg";

                    product.CreateDate = DateTime.Now;
                    Console.WriteLine(product.ProductName);
                    product.ProductName = Utilities.ToTitleCase(product.ProductName);
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    _notyfService.Success("Thêm mới thành công");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _notyfService.Error("Tên sản phẩm đã tồn tại");
                    ViewData["Categories"] = new SelectList(_context.Categories, "CategoryId", "CategoryName",product.CategoryId);
                    return View(product);
                }
               
            }
            ViewData["Categories"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Manager/ManageProducts/Edit/5
        public async Task<IActionResult> Edit(int? ProductId)
        {
            if (ProductId == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(ProductId);
            if (product == null)
            {
                return NotFound();
            }
            
            ViewData["Categories"] = new SelectList(_context.Categories, "CategoryId", "CategoryName",product.CategoryId);
            return View(product);
        }

        // POST: Manager/ManageProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int ProductId, [Bind("ProductId,ProductName,SortDescription,Description,CategoryId,Price,Discount,Thumbnail,CreateDate,ModifiedDate,HomeFlag,Active,UnitInStock")] Product product, IFormFile? fileThumnail)
        {
            if (ProductId != product.ProductId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    product.ProductName = Utilities.ToTitleCase(product.ProductName);
                    if (fileThumnail != null)
                    {
                        string extension = Path.GetExtension(fileThumnail.FileName);
                        string image = Utilities.SEOUrl(product.ProductName) + extension;
                        product.Thumbnail = await Utilities.UploadFile(fileThumnail, @"products", image.ToLower());
                    }
                    if (string.IsNullOrEmpty(product.Thumbnail)) product.Thumbnail = "default.jpg";

                    product.ModifiedDate = DateTime.Now;

                    _context.Update(product);
                    _notyfService.Success("Cập nhật sản phẩm thành công");
                    await _context.SaveChangesAsync();
                   
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            _notyfService.Error("Cập nhật sản phẩm thất bại");
            ViewData["Categories"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Manager/ManageProducts/Delete/5
        public async Task<IActionResult> Delete(int? ProductId)
        {
            if (ProductId == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.category)
                .FirstOrDefaultAsync(m => m.ProductId == ProductId);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Manager/ManageProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int ProductId)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'EcommerceDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(ProductId);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            _notyfService.Success("Xóa sản phẩm thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
        private bool ProductNameExists(string productName)
        {
            return (_context.Products?.Any(e => e.ProductName.ToLower().Equals(productName.ToLower()))).GetValueOrDefault();
        }

        public IActionResult FiltterProductByCategory(int CategoryId = 0)
        {
            var url = $"/Manager/ManageProducts?CategoryId={CategoryId}";
            if (CategoryId == 0)
            {
                url = $"/Manager/ManageProducts";
            }
            return Json(new { status = "success", redirectUrl = url });
        }

        [HttpPost]
        public IActionResult FindProduct(string keywordProduct)
        {
            List<Product> matchedProducts = new List<Product>();
            if (string.IsNullOrEmpty(keywordProduct) || keywordProduct.Length < 1)
            {
                matchedProducts = _context.Products.AsNoTracking()
                                               .Include(p => p.category)
                                               .Take(10)
                                               .ToList();

                return PartialView("_matchedProductsSearch", matchedProducts);
            }

            matchedProducts = _context.Products.AsNoTracking()
                                                .Include(p => p.category)
                                                .Where(p => p.ProductName.ToLower().Contains(keywordProduct.ToLower()))
                                                .Take(10)
                                                .ToList();

            if (matchedProducts == null)
            {
                return PartialView("_matchedProductsSearch", null);
            }
            else
            {
                return PartialView("_matchedProductsSearch", matchedProducts);
            }
           
        }
    }
}
