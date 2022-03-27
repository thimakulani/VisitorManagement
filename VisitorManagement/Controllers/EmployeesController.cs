using Microsoft.AspNetCore.Mvc;
using VisitorManagement.Data;

namespace VisitorManagement.Controllers
{
    public class EmployeesController : Controller
    {
        AppDBContext context;

        public EmployeesController(AppDBContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var employees = context.Employees.ToList();
            return View(employees);
        }
        public IActionResult CheckIn()
        {
            return View();
        }
        public IActionResult EmployeeChecksIn()
        {
            return View();
        }
    }
}
