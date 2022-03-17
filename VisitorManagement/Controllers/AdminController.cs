using Microsoft.AspNetCore.Mvc;

namespace VisitorManagement.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
