using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using VisitorManagement.Data;
using VisitorManagement.Models;
using VisitorManagement.Service;

namespace VisitorManagement.Controllers
{
    public class VisitorsController : Controller
    {
        AppDBContext context;
        public VisitorsController(AppDBContext context)
        {
            this.context = context;

        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Report()
        {
            var visitor = context.Visitor.ToList();
            var visitor_register = context.VisittorRegister.ToList();

            var results = (from v_r in visitor_register
                           join v in visitor on v_r.VisitorId equals v.Id
                           select new ReportViewModel
                           {
                               VisittorRegister = v_r,
                               Visitor = v,
                           }
                           );


            return View(results);
        }
        public IActionResult ShowVisitors()
        {
            var visitors = context.Visitor.ToList();
            return View(visitors);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult CheckOut()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CheckOut(Visitor visitor)
        {
            var v = context.Visitor.Find(visitor.Id);
            if (v != null)
            {
                if (v.Status == "signed in" || string.IsNullOrEmpty(v.Status))
                {
                    v.Status = "signed out";
                    context.Visitor.Update(v);

                    var v_r = context.VisittorRegister.FirstOrDefault(x => x.Last_logout == null && x.VisitorId == visitor.Id);
                    v_r.Last_logout = DateTime.Now;
                    context.VisittorRegister.Update(v_r);
                    context.SaveChanges();

                }
                else
                {
                    TempData["error"] = "Visitor already signed out";
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(Visitor visitor)
        {
            if (ModelState.IsValid)
            {
                var v = context.Visitor.Find(visitor.Id);
                if (v == null)
                {
                    context.Visitor.Add(visitor);
                    context.SaveChanges();
                    RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Id Number already exists");
                }
            }

            return View();
        }
        public IActionResult CheckIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CheckIn(Visitor visitor)
        {
            if (visitor.Id != null)
            {
                var results = context.Visitor.Find(visitor.Id);
                if (results != null)
                {
                    return RedirectToAction("VisitorCheckIn", "Visitors", results);
                }
                else
                {
                    ModelState.AddModelError("", "Visitor is not registred");
                }
            }
            return View();
        }
        public IActionResult VisitorCheckIn(Visitor visitor)
        {

            ViewBag.v_name = visitor.Name;
            HttpContext.Session.SetString("Id", visitor.Id);
            //v_id = visitor.Id;
            return View();
        }
        [HttpPost]
        public IActionResult VisitorCheckIn(VisittorRegister data)
        {
            var ll = DateTime.Now;
            data.Last_login = ll;

            data.VisitorId = HttpContext.Session.GetString("Id");
            if (data.VisitorId != null)
            {
                var visitor = context.Visitor.Find(data.VisitorId);
                visitor.Status = "signed in";
                context.VisittorRegister.Add(data);
                context.Visitor.Update(visitor);
                context.SaveChanges();

            }
            return View();
        }
    }
}
