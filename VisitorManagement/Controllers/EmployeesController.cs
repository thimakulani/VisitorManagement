using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VisitorManagement.Data;
using VisitorManagement.Models;
using VisitorManagement.Service;
using System.Linq;

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
            emp.Persal = persal;
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

            var emp = viewModel.Employee;
            context.Entry(emp).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            context.SaveChanges();
            viewModel.EmployeeRegister = reg;
            TempData["success"] = "Successfully updated";

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
        public IActionResult EmployeeChecksIn(EmployeeRegister viewModel)
        {
            HttpContext.Session.SetInt32("Persal", viewModel.EmployeeId);
            return View();
        }
        [HttpPost]
        public IActionResult CheckIn(EmployeeRegister viewModel)
        {
            if (viewModel.EmployeeId > 0)
            {
                var emp = context.Employee.Find(viewModel.EmployeeId);

                if (emp != null)
                {
                    if (emp.Status == "signed out")//signed out
                    {
                        //if signed out
                        //check last signed in dates
                        if (emp.LastCheckIn.HasValue)
                        {
                            if ((DateTime.Now - emp.LastCheckIn.Value).TotalDays >= 7)
                            {
                                //create id session
                                //create temperature session
                                //asset name  session
                                //create asset number session
                                ViewBag.e_name = $"{emp.FirstName} {emp.LastName}";
                                return RedirectToAction("EmployeeChecksIn", viewModel);//with healthcheck
                            }
                            else
                            {
                                var d_t = DateTime.Now;
                                var EMP_REG = context.EmployeeRegister.Where(x => x.EmployeeId == viewModel.EmployeeId && x.Last_login.Value.Date == emp.LastCheckIn.Value.Date);
                                emp.Status = "signed in";
                                emp.LastCheckIn = d_t;

                                //FIND LAST CHECKED IN FROM REGISTER

                                viewModel.Last_login = d_t;


                                context.Employee.Update(emp);
                                context.EmployeeRegister.Add(viewModel);
                                context.SaveChanges();
                                TempData["success"] = "Employee successfully checked in";
                                return RedirectToAction("CheckIn");
                            }
                        }
                        else
                        {
                            ViewBag.e_name = $"{emp.FirstName} {emp.LastName}";
                            return RedirectToAction("EmployeeChecksIn", viewModel);
                        }


                    }
                    else
                    {
                        TempData["error"] = "Employee is currently signed in from last session";
                        if (emp.LastCheckIn.HasValue)
                        {
                            ModelState.AddModelError("", $"Employee is currently signed it, from {emp.LastCheckIn.Value:dddd, dd/MM/yyyy}");
                        }
                    }


                    //var emp_reg = context.EmployeeRegister.First(x => (x.Last_login.Value - DateTimeOffset.Now).TotalDays <= 7);
                    //if (emp_reg == null)
                    //{
                    //    return RedirectToAction("EmployeeChecKsIn", viewModel);
                    //}
                    //else
                    //{

                    //}
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
            return View(viewModel);
        }

        public IActionResult CheckOut()
        {
            return View();
        }
    }
}
