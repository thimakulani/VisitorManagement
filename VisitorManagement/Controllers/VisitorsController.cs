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
            var v_r = context.VisittorRegister.ToList();//.Include("Visitor").Where(x => x.Visitor == context.Visitor.Find(x.Id)).ToList();
            List<VisittorRegister> visitors = new List<VisittorRegister>();
            foreach (var item in v_r)
            {

                var v = context.Visitor.Find(item.VisitorId);
                var vr = item;
                if (v != null)
                {
                    vr.Visitor = v;
                }
                visitors.Add(vr);
            }
            //var report = (from v_r in context.VisittorRegister
            //              join v in context.Visitor
            //              on v_r.Visitor equals v
            //              select v_r).ToList();
            // List<VisittorRegister> visitors = new List<Visitor>();


            return View(visitors);
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
                if (v.Status == "signed in" || v.Status == null )
                {
                    v.Status = "signed out";
                    context.Visitor.Update(v);

                    var v_r = context.VisittorRegister.FirstOrDefault(x => x.Last_logout_date == null || x.Last_logout == null && x.VisitorId == visitor.Id);
                    v_r.Last_logout_date = DateTime.Now;
                    context.VisittorRegister.Update(v_r);
                    context.SaveChanges();

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
            data.Last_login_date = ll;
            data.VisitorId = HttpContext.Session.GetString("Id");
            if (data.VisitorId != null)
            {
                var visitor = context.Visitor.Find(data.Visitor.Id);
                visitor.Status = "signed in";
                context.VisittorRegister.Add(data);
                context.Visitor.Update(visitor);
                context.SaveChanges();

            }
            return View();
        }
    }
}
