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
        public IActionResult EmployeeChecksIn(EmployeeCheckInViewModel viewModel)
        {
            var names = HttpContext.Session.GetString("Names");
            ViewBag.e_name = names;
            ViewBag.Temp = viewModel.Temperature;

            HttpContext.Session.SetInt32("EmployeeId", viewModel.Percal);
            
            HttpContext.Session.SetString("Temp", viewModel.Temperature.ToString().Trim());
            if (viewModel.AssetType !=null && viewModel.AssetType !=null) 
            {
                HttpContext.Session.SetString("Asset_num", viewModel.AssetNumber);
                HttpContext.Session.SetString("Asset_type", viewModel.AssetType);
            }
            EmployeeRegister employee_reg = new()
            {
                EmployeeId = viewModel.Percal,
                Temp = viewModel.Temperature,
                Asset_num = viewModel.AssetNumber,
                Asset_type = viewModel.AssetType,

            };
             

            return View(employee_reg);
        }
        [HttpPost]
        public IActionResult EmployeeChecksIn(EmployeeRegister model, IFormCollection form)
        {
            var d_t = DateTime.Now;
            var emp = context.Employee.Find(model.EmployeeId);
            if(emp != null)
            {
                emp.LastCheckIn = d_t;
                emp.Status = "signed in";
                context.Employee.Update(emp);
            }
            model.Last_login = d_t;
            context.EmployeeRegister.Add(model);
            context.SaveChanges();
            TempData["success"] = "Successfully logged in";
            //var EMP_REG = context.EmployeeRegister.Where(x => x.EmployeeId == viewModel.EmployeeId && x.Last_login.Value.Date == emp.LastCheckIn.Value.Date);
            
            //var data = new EmployeeRegister()
            //{
            //    Temp = double.Parse(HttpContext.Session.GetString("Temp")),
            //    Asset_num = HttpContext.Session.GetString("Asset_num"),
            //    EmployeeId = (int)HttpContext.Session.GetInt32("EmployeeId"),
            //    Asset_type = HttpContext.Session.GetString("Asset_type"),
            //    Last_login = d_t,
                
            //};

            //healthCheck.EmployeeId = data.EmployeeId;
            //context.EmployeeRegister.Add(data);
            //context.HealthCheck.Add(healthCheck);
            return View();
        }
        [HttpPost]
        public IActionResult CheckIn(EmployeeCheckInViewModel viewModel)
        {
            if (viewModel.Percal > 0)
            {
                var emp = context.Employee.Find(viewModel.Percal);

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
                               
                                HttpContext.Session.SetString("Names", $"{emp.FirstName} {emp.LastName}");
                                

                                return RedirectToAction("EmployeeChecksIn", viewModel);//with healthcheck
                            }
                            else
                            {
                                var d_t = DateTime.Now;
                                //var EMP_REG = context.EmployeeRegister.Where(x => x.EmployeeId == viewModel.EmployeeId && x.Last_login.Value.Date == emp.LastCheckIn.Value.Date);
                                emp.Status = "signed in";
                                emp.LastCheckIn = d_t;

                                //FIND LAST CHECKED IN FROM REGISTER
                                EmployeeRegister employeeRegister = new EmployeeRegister()
                                {
                                    Asset_num = viewModel.AssetNumber,
                                    EmployeeId = viewModel.Percal,
                                    Temp = viewModel.Temperature,
                                    Last_login = d_t,
                                    HealthCheck = null
                                };
                                context.Employee.Update(emp);
                                context.EmployeeRegister.Add(employeeRegister);
                                context.SaveChanges();
                                TempData["success"] = "Employee successfully checked in";
                                return RedirectToAction("CheckIn");
                            }
                        }
                        else
                        {
                            HttpContext.Session.SetString("Names", $"{emp.FirstName} {emp.LastName}");
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
                TempData["error"] = "Persal not registered";
            }
            return View(viewModel);
        }

        public IActionResult CheckOut()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CheckOut(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var emp = context.Employee.Find(employee.Persal);
                if(emp != null)
                {
                    var dt = DateTime.Now;
                    var emp_reg = context.EmployeeRegister.Single(x => x.EmployeeId == emp.Persal && x.Last_login == emp.LastCheckIn);
                    emp.LastCheckOut = dt;
                    emp_reg.Last_logout = dt;
                    emp.Status = "signed out";
                    context.Employee.Update(emp);
                    context.SaveChanges();
                    ModelState.Clear();
                    TempData["success"] = "Successfully Logged out";
                }
            }
            return View(employee);
        }
        public IActionResult Acknowladge()
        {
            return View();
        }

    }
}
