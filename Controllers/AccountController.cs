using Microsoft.AspNetCore.Mvc;

namespace StickyPointsMVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
