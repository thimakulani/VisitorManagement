using Microsoft.AspNetCore.Mvc;
using VisitorManagement.Data;
using VisitorManagement.Models;

namespace VisitorManagement.Controllers
{
    public class TemperatureController : Controller
    {
        private AppDBContext context { get; set; }

        public TemperatureController(AppDBContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var temp = context.Temperature.Find(1);
            if (temp == null)
            {
                temp = new Temperature()
                {
                    Value = 36
                };
                context.Temperature.Add(temp);
                context.SaveChanges();
            }
            return View(temp);
        }
        [HttpPost]
        public IActionResult Index(Temperature temperature)
        {
            if (ModelState.IsValid)
            {
                temperature.Id = 1;
                context.Temperature.Update(temperature);
                context.SaveChanges();
            }
            return View(temperature);
        }
    }
}
