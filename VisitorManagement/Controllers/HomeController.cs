using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VisitorManagement.Models;

namespace VisitorManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<Admin> userManager;
        private readonly SignInManager<Admin> signInManager;

        public HomeController(ILogger<HomeController> logger, UserManager<Admin> userManager, SignInManager<Admin> signInManager)
        {
            _logger = logger;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(ApplicationUser admin)
        {
            if (admin.Email !=null && admin.Password !=null)
            {
                
                var results = await signInManager.PasswordSignInAsync(admin.Email, admin.Password, isPersistent: true, false);
                if (results.Succeeded)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                ModelState.AddModelError("", "Incorrect email orpassword");
            }
            return View(admin);
        }
       public IActionResult RegView()
        {
            return View();
        }
        public IActionResult Logout()
        {
            signInManager.SignOutAsync();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}