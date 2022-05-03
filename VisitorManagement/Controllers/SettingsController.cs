using Microsoft.AspNetCore.Mvc;

namespace VisitorManagement.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
