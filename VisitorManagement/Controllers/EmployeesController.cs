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
            var employees = context.Employee.ToList();
            return View(employees);
        }
        public IActionResult Details(int persal)
        {
            var reg = context.EmployeeRegister.Where(x => x.EmployeeId == persal).ToList();
            var emp = context.Employee.Find(persal);

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
            var reg = context.EmployeeRegister.Where(x => x.EmployeeId == viewModel.Employee.Persal).ToList();
            viewModel.EmployeeRegister = reg;
            context.Employee.Update(viewModel.Employee);
            context.SaveChanges();
            Console.WriteLine(viewModel);

            return View(viewModel);
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
                var emp = context.Employee.Find(employee.Persal);
                if (emp == null)
                {
                    employee.Status = "signed out";
                    context.Employee.Add(employee);
                    context.SaveChanges();
                    TempData["success"] = "Employee profile has been successfully created";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "Percal Number already registred";
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
                var emp = context.Employee.FirstOrDefault(x => x.Persal == employee.Persal);
                if (emp != null)
                {
                    var emp_reg = context.HealthCheck.Where(x => x.EmployeeId == employee.Persal && x.Last_check_dates.Value.DayOfWeek > DateTime.Now.DayOfWeek);
                }
                else
                {
                    TempData["error"] = "Persal not registered";
                }
            }
            else
            {
                TempData["error"] = "Invalid Persal Number";
            }
            return View();
        }
    }
}
