using Microsoft.AspNetCore.Mvc;
using VisitorManagement.Data;
using VisitorManagement.Models;
using VisitorManagement.Service;

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
        public IActionResult Details(int persal)
        {
            var reg = context.EmployeeRegister.Where(x => x.Persal == persal).ToList();
            var emp = context.Employees.Find(persal);

            EmployeeProfileViewModel employeeProfileViewModel = new EmployeeProfileViewModel()
            {
                Employee = emp,
                EmployeeRegister = reg
            };


            return View(employeeProfileViewModel);
        }
        [HttpPost]
        public IActionResult Details(EmployeeProfileViewModel viewModel)
        {

            return View();
        }
        public IActionResult CheckIn()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (employee != null)
            {
                var emp = context.Employees.Find(employee.Persal);
                if (emp == null)
                {
                    context.Employees.Add(employee);
                    context.SaveChanges();
                    TempData["success"] = "Employee profile has been successfully created";
                    RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Persal already registered");
                }
            }
            return View();
        }
        public IActionResult EmployeeChecksIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CheckIn(Employee employee)
        {
            if (employee.Persal > 0)
            {
                var emp = context.Employees.Find(employee.Persal);
                if (emp != null)
                {
                    var emp_reg = context.HealthCheck.Where(x => x.Persal == employee.Persal && x.Last_check_dates.Value.DayOfWeek > DateTime.Now.DayOfWeek);
                }
            }
            else
            {
                TempData["error"] = "Persal not registered";
            }
            return View();
        }
    }
}
