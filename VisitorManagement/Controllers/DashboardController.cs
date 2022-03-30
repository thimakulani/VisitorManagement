using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using VisitorManagement.Data;
using VisitorManagement.Models;
using VisitorManagement.Service;

namespace VisitorManagement.Controllers
{
    public class DashboardController : Controller
    {
        AppDBContext context;
        private readonly SignInManager<Admin> signInManager;
        public DashboardController(AppDBContext context, SignInManager<Admin> signInManager)
        {
            this.context = context;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            DashboardViewModel model = new();
            model.EmployeeCheckIn = context.EmployeeRegister.Where(x => x.Last_login.Value.Date == DateTime.Now.Date).Count();
            model.EmployeeCheckout = context.EmployeeRegister.Where(x => x.Last_logout.Value.Date == DateTime.Now.Date).Count();

            model.VisitorCheckIn = context.VisittorRegister.Where(x => x.Last_login.Value.Date == DateTime.Now.Date).Count();
            model.VisitorCheckOut = context.VisittorRegister.Where(x => x.Last_logout.Value.Date == DateTime.Now.Date).Count();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
