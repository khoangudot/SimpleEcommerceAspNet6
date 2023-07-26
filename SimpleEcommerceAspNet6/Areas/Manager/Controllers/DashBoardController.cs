using Microsoft.AspNetCore.Mvc;

namespace SimpleEcommerceAspNet6.Areas.Manager.Controllers
{
    [Area(nameof(Manager))]
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
