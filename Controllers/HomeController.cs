using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StickyPointsMVC.Models;
using StickyPointsMVC.Data;
using Microsoft.EntityFrameworkCore;

namespace StickyPointsMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult SetCookies(string cookieName, string cookieValue)
        {
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(15),   // Cookie expires in 15 days
                HttpOnly = true,                      // Prevent JS access
                Secure = true,                        // Only HTTPS
                SameSite = SameSiteMode.Strict        // Prevent CSRF
            };

            Response.Cookies.Append(cookieName, cookieValue, options);
            return Ok("Cookie has been set.");
        }

        public IActionResult Index()
        {

            if (User.Identity.IsAuthenticated)
            {
                SetCookies("userName", User.Identity.Name);
            }
            else
            {
                SetCookies("userName", "guest");
            }

            SetCookies("browserName", Request.Headers["User-Agent"].ToString());

            string? lastVisit = HttpContext.Session.GetString("LastVisit");

            ViewBag.LastVisit = lastVisit;

            HttpContext.Session.SetString("LastVisit", DateTime.Now.ToString());


            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email)
        {
            var student = _context.Students.FirstOrDefault(s => s.Email == email);

            if (student == null)
            {
                ViewBag.LoginError = "No account found with this email. Please sign up first.";
                return View();
            }

            ViewBag.LoginSuccess = $"Welcome back, {student.Name}! Your current points: {student.Points}.";
            return View();
        }

        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(string name, int age, string email)
        {
            // check if email already exists
            var existingStudent = _context.Students.FirstOrDefault(s => s.Email == email);

            if (existingStudent != null)
            {
                ViewBag.SignupError = "This email is already registered. Please login instead.";
                return View();
            }

            var student = new Student
            {
                Name = name,
                Age = age,
                Email = email,
                Points = 0
            };

            _context.Students.Add(student);
            _context.SaveChanges();

            ViewBag.SignupMessage = "Signup successful! Please go to the Login page to confirm.";
            return View();
        }

        public IActionResult StudentJoin()
        {
            return View();
        }

        public IActionResult TeacherDashboard()
        {
            var students = _context.Students.ToList(); 
            return View(students);                     
        }
    }
}
