using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VisitorManagement.Data;
using VisitorManagement.Models;

namespace VisitorManagement.Controllers
{
    public class VisitReasonsController : Controller
    {
        private readonly AppDBContext _context;

        public VisitReasonsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: VisitReasons
        public async Task<IActionResult> Index()
        {
            return View(await _context.VisitReason.ToListAsync());
        }

        // GET: VisitReasons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitReason = await _context.VisitReason
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visitReason == null)
            {
                return NotFound();
            }

            return View(visitReason);
        }

        // GET: VisitReasons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VisitReasons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id")] VisitReason visitReason)
        {
            if (ModelState.IsValid)
            {
                _context.Add(visitReason);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(visitReason);
        }

        // GET: VisitReasons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitReason = await _context.VisitReason.FindAsync(id);
            if (visitReason == null)
            {
                return NotFound();
            }
            return View(visitReason);
        }

        // POST: VisitReasons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Id")] VisitReason visitReason)
        {
            if (id != visitReason.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visitReason);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitReasonExists(visitReason.Id))
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
            return View(visitReason);
        }

        // GET: VisitReasons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitReason = await _context.VisitReason
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visitReason == null)
            {
                return NotFound();
            }

            return View(visitReason);
        }

        // POST: VisitReasons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var visitReason = await _context.VisitReason.FindAsync(id);
            _context.VisitReason.Remove(visitReason);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitReasonExists(int id)
        {
            return _context.VisitReason.Any(e => e.Id == id);
        }
    }
}
