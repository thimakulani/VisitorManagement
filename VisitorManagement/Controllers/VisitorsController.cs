using Microsoft.AspNetCore.Mvc;
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
            var visitors = context.Visitor.ToList();
            return View(visitors);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Visitor visitor)
        {
            if (ModelState.IsValid)
            {
                context.Visitor.Add(visitor);
                context.SaveChanges();
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
            if(visitor.Identification != null)
            {
                var results = context.Visitor.Find(visitor.Identification);
                if (results != null)
                {
                    return RedirectToAction("VisitorCheckIn","Visitors", results);
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
            return View();
        }
    }
}
