using Microsoft.AspNetCore.Mvc;

namespace StickyPointsMVC.Controllers
{
    public class SessionController : Controller
    {
        public IActionResult Index()
        {
            string email = HttpContext.Session.GetString("email") ?? "No email saved";
            string page = HttpContext.Session.GetString("page") ?? "No page stored";

            ViewBag.Email = email;
            ViewBag.Page = page;

            return View();
        }

        public IActionResult SetSession()
        {
            HttpContext.Session.SetString("email", "samer@student.com");
            HttpContext.Session.SetString("page", "/Home/Index");

            return Ok("Session has been set!");
        }

        public IActionResult ClearSession()
        {
            HttpContext.Session.Clear();
            return Ok("Session cleared!");
        }
    }
}
