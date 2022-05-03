using Microsoft.AspNetCore.Mvc;
using VisitorManagement.Data;
using VisitorManagement.Models;

namespace VisitorManagement.Controllers
{
    public class DeliveryController : Controller
    {
        private AppDBContext context;

        public DeliveryController(AppDBContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var data = context.Delivery.ToList();
            return View(data);
        }
        public IActionResult CheckIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CheckIn(Delivery delivery)
        {
            context.Delivery.Add(delivery);
            return View();
        }

    }
}
