using Microsoft.AspNetCore.Mvc;
using SimpleEcommerceAspNet6.Data;
using SimpleEcommerceAspNet6.Models;

namespace SimpleEcommerceAspNet6.Areas.Manager.Controllers
{
    [Area(nameof(Manager))]
    public class DemoViewComponentController : Controller
    {
        private readonly EcommerceDbContext _context;

        public DemoViewComponentController(EcommerceDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Category> listCate = _context.Categories.ToList();
           
            return View(listCate);
        }
    }
}
