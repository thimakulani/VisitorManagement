using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VisitorManagement.Data;
using VisitorManagement.Models;
using VisitorManagement.Service;

namespace VisitorManagement.Controllers
{
    public class VisitorsController : Controller
    {
        private readonly AppDBContext context;
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
            var visitor_register = context.VisitorRegister.ToList();

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

                    var v_r = context.VisitorRegister.FirstOrDefault(x => x.Last_logout == null && x.VisitorId == visitor.Id);
                    v_r.Last_logout = DateTime.Now;
                    context.VisitorRegister.Update(v_r);
                    context.SaveChanges();
                    TempData["success"] = "Successfully signed out";
                    return RedirectToAction("Acknowladge");
                }
                else
                {
                    TempData["error"] = "Visitor already signed out";
                }
            }
            return View();
        }
        public IActionResult Acknowladge()
        {
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
                    TempData["success"] = "Successfully created";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Id number already exists");
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
                    if (results.Status == "signed in")
                    {
                        TempData["error"] = "Visitor already signed in, please sign out visitor, before he/she can be signed in again";
                        return RedirectToAction("CheckOut");
                    }
                    
                    return RedirectToAction("VisitorCheckIn", "Visitors", results);
                }
                else
                {
                    TempData["error"] = "Id number not registered";
                    ModelState.AddModelError("", "Visitor is not registred");
                }
            }
            return View(visitor);
        }
        private static List<SelectListItem> PopulateReason(List<VisitReason> data, string selected)
        {
            List<SelectListItem> listItems = new();
            foreach (var item in data)
            {
                if (selected == item.Name)
                {
                    listItems.Add(new SelectListItem { Text = item.Name, Selected = true });
                }
                else
                {
                    listItems.Add(new SelectListItem { Text = item.Name, Selected = false });
                }
            }
            return listItems;
        }
        private static List<SelectListItem> PopulateWhom(List<Employee> data, string selected)
        {
            List<SelectListItem> listItems = new()
            {
                new SelectListItem { Text = "Other", Selected = false }
            };
            foreach (var item in data)
            {
                if (selected == $"{item.FirstName} {item.LastName}")
                {
                    listItems.Add(new SelectListItem { Text = $"{item.FirstName} {item.LastName}", Selected = true });
                }
                else
                {
                    listItems.Add(new SelectListItem { Text = $"{item.FirstName} {item.LastName}", Selected = false });
                }
            }
            return listItems;
        }
        public IActionResult VisitorCheckIn(Visitor visitor)
        {

            ViewBag.v_name = visitor.Name;
            HttpContext.Session.SetString("Id", visitor.Id);
            //v_id = visitor.Id;
            ViewData["ReasonVisit"] = PopulateReason(context.VisitReason.ToList(), "");
            ViewData["WhomToVisit"] = PopulateWhom(context.Employee.ToList(), "");

            return View();
        }
        [HttpPost]
        public IActionResult VisitorCheckIn(VisitorRegister data, IFormCollection form)
        {
            var selected = form["ReasonVisit"];
            var selected_whom = form["WhomToVisit"];
            ViewData["ReasonVisit"] = PopulateReason(context.VisitReason.ToList(), selected);
            ViewData["WhomToVisit"] = PopulateWhom(context.Employee.ToList(), selected_whom);
            var temperature = context.Temperature.Find(1).Value;
            if (temperature >= data.Temperature)
            {

                bool healthCheck = DoHealthCheck(data);
                var ll = DateTime.Now;
                data.Last_login = ll;
                data.AppointmentWith = selected_whom;
                data.ReasonVisit = selected;
                data.VisitorId = HttpContext.Session.GetString("Id");
                if (data.VisitorId != null)
                {
                    var visitor = context.Visitor.Find(data.VisitorId);
                    visitor.Status = "signed in";
                    context.VisitorRegister.Add(data);
                    context.Visitor.Update(visitor);
                    context.SaveChanges();
                    TempData["success"] = "Visitor is successfully logged it";
                    return RedirectToAction("CheckIn");
                }
            }
            else
            {
                TempData["error"] = "Visitor's Temperatur is above";


            }
            ViewBag.v_name = context.Visitor.Find(HttpContext.Session.GetString("Id"));
            return View(data);
        }

        private static bool DoHealthCheck(VisitorRegister data)
        {
            if (data.Hc_cough && data.Hc_fevor && data.Hc_loss_taste
                && data.Hc_muscle_pain && data.Hc_other && data.Hc_shortness_breath
                && data.Hc_sore_throat)
            {
                return true;
            }
            return false;
        }

        // GET: Visitor/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitor = await context.Visitor.FindAsync(id);
            if (visitor == null)
            {
                return NotFound();
            }
            return View(visitor);
        }

        // POST: Visitor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Email,Company,Phone,Address,Purpose,Image,Status")] Visitor visitor)
        {
            if (id != visitor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(visitor);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (context.Visitor.Find(visitor.Id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(visitor);
        }
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitor = await context.Visitor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visitor == null)
            {
                return NotFound();
            }

            return View(visitor);
        }
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitor = await context.Visitor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visitor == null)
            {
                return NotFound();
            }

            return View(visitor);
        }

        // POST: Visitor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var visitor = await context.Visitor.FindAsync(id);
            context.Visitor.Remove(visitor);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
