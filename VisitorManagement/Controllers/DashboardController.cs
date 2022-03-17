using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using VisitorManagement.Data;
using VisitorManagement.Service;

namespace VisitorManagement.Controllers
{
    public class DashboardController : Controller
    {
        AppDBContext context;
        public DashboardController(AppDBContext context)
        {
            this.context = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            DashboardStats model = new DashboardStats();
            model.VisitorCounter = context.Visitor.ToList().Count;
            model.EmployeeCounter = context.Employees.ToList().Count;
            return View(model);
        }
    }
}
