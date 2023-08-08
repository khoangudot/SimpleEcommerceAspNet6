using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleEcommerceAspNet6.Data;
using SimpleEcommerceAspNet6.Models;

namespace SimpleEcommerceAspNet6.Areas.Manager.Views.Shared.Components
{
    
    //[ViewComponent]
    public class ProductGridView : ViewComponent
    {
        private EcommerceDbContext _context;

        public ProductGridView(EcommerceDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            List<Product> products = _context.Products.Include(p=>p.category).ToList();
            
            return View<List<Product>>(products);
        }
    }
}
