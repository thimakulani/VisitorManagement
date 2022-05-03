using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using VisitorManagement.Data;
using VisitorManagement.Models;

namespace VisitorManagement.Controllers
{
    public class AdminController : Controller
    {

        private readonly UserManager<Admin> userManager;
        private readonly SignInManager<Admin> signInManager;
        private readonly AppDBContext context;

        public AdminController(UserManager<Admin> userManager, SignInManager<Admin> signInManager, AppDBContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }

        public IActionResult Index()
        {
            var admin = userManager.Users;
            return View(admin);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUser admin)
        {
            if (ModelState.IsValid)
            {
                var user = new Admin()
                {
                    UserName = admin.Email,
                    Email = admin.Email,
                    Firstname = admin.Firstname,
                    DateCreated = admin.DateCreated,
                    Level = "Admin",
                    Lastname = admin.Lastname,
                    Persal = admin.Persal,
                    PhoneNumber = admin.PhoneNumber

                };
                var results = await userManager.CreateAsync(user, admin.Password);
                if (results.Succeeded)
                {


                    return RedirectToAction("Index", "Dashboard");
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
    }
}
