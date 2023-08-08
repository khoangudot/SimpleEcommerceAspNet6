using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleEcommerceAspNet6.Data;
using SimpleEcommerceAspNet6.Models;

namespace SimpleEcommerceAspNet6.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class ManageCategoriesController : Controller
    {
        private readonly EcommerceDbContext _context;
        private readonly INotyfService _notyfService;

        public ManageCategoriesController(EcommerceDbContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Manager/ManageCategories
        public async Task<IActionResult> Index()
        {
            return _context.Categories != null ?
                        View(await _context.Categories.ToListAsync()) :
                        Problem("Entity set 'EcommerceDbContext.Category'  is null.");
        }

        // GET: Manager/ManageCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Manager/ManageCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Manager/ManageCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                if (!CategoryNameExists(category.CategoryName))
                {
                    _context.Add(category);
                    await _context.SaveChangesAsync();
                    _notyfService.Success("Tạo mới thành công");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _notyfService.Error("Tên loại sản phẩm đã tồn tại");
                    return View(category);
                }

            }
            _notyfService.Error("Tạo mới thất bại");
            return View(category);
        }

        // GET: Manager/ManageCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Manager/ManageCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,Description")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
                    {
                        return NotFound();
                    }

                    else
                    {
                        throw;
                    }
                }
                _notyfService.Success("Chỉnh sửa thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Manager/ManageCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Manager/ManageCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'EcommerceDbContext.Category'  is null.");
            }
            var catogory = await _context.Categories.FindAsync(id);
            if (catogory != null)
            {
                _context.Categories.Remove(catogory);
            }

            await _context.SaveChangesAsync();
            _notyfService.Success("Xóa thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return (_context.Categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
        private bool CategoryNameExists(string categoryName)
        {
            return (_context.Categories?.Any(e => e.CategoryName.ToLower().Equals(categoryName.ToLower()))).GetValueOrDefault();
        }
    }
}
