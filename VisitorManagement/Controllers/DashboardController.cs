using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using System.Dynamic;
using System.Net;
using System.Net.Mail;
using VisitorManagement.Data;
using VisitorManagement.Models;
using VisitorManagement.Service;

namespace VisitorManagement.Controllers
{
    public class DashboardController : Controller
    {
        AppDBContext context;
        private readonly SignInManager<Admin> signInManager;
        private readonly IEmailService _EmailService;
        public DashboardController(AppDBContext context, SignInManager<Admin> signInManager, IEmailService emailService)
        {
            this.context = context;
            this.signInManager = signInManager;
            _EmailService = emailService;
        }

        public IActionResult Index()
        {
            DashboardViewModel model = new();
            model.EmployeeCheckIn = context.EmployeeRegister.Where(x => x.Last_login.Value.Date == DateTime.Now.Date).Count();
            model.EmployeeCheckout = context.EmployeeRegister.Where(x => x.Last_logout.Value.Date == DateTime.Now.Date).Count();

            model.VisitorCheckIn = context.VisitorRegister.Where(x => x.Last_login.Value.Date == DateTime.Now.Date).Count();
            model.VisitorCheckOut = context.VisitorRegister.Where(x => x.Last_logout.Value.Date == DateTime.Now.Date).Count();
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
