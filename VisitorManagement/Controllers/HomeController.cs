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
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
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
        public async Task<IActionResult> Index(Admin admin)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = admin.Email,
                    Email = admin.Email,
                };
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
        [HttpPost]
        public async Task<IActionResult> RegView(Admin admin)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = admin.Email,
                    Email = admin.Email,

                };
                var results = await userManager.CreateAsync(user, admin.Password);
                if (results.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "home");
                }
                else
                {
                    foreach (var item in results.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(admin);
        }
        public IActionResult Privacy()
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