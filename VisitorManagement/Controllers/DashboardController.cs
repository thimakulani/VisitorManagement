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
            DashboardStats model = new DashboardStats();
            model.VisitorCounter = context.Visitor.ToList().Count;
            model.EmployeeCounter = context.Employees.ToList().Count;
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
