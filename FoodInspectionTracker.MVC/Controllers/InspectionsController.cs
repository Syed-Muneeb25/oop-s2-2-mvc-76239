using FoodInspectionTracker.Domain;
using FoodInspectionTracker.MVC.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodInspectionTracker.MVC.Controllers
{
    [Authorize(Roles = "Admin,Inspector")]
    public class InspectionsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<InspectionsController> _logger;

        public InspectionsController(AppDbContext context, ILogger<InspectionsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Inspections
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Inspections.Include(i => i.Premises);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Inspections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inspection = await _context.Inspections
                .Include(i => i.Premises)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inspection == null)
            {
                return NotFound();
            }

            return View(inspection);
        }

        // GET: Inspections/Create
        public IActionResult Create()
        {
            ViewData["PremisesId"] = new SelectList(_context.Premises, "Id", "Address");
            return View();
        }

        // POST: Inspections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PremisesId,InspectionDate,Score,Outcome,Notes")] Inspection inspection)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Creating inspection for PremisesId={PremisesId} by {User}",
                    inspection.PremisesId, User.Identity?.Name);

                _context.Add(inspection);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Inspection created successfully. Id={Id}, Score={Score}, Outcome={Outcome}",
                    inspection.Id, inspection.Score, inspection.Outcome);

                return RedirectToAction(nameof(Index));
            }

            ViewData["PremisesId"] = new SelectList(_context.Premises, "Id", "Address", inspection.PremisesId);
            return View(inspection);
        }

        // GET: Inspections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inspection = await _context.Inspections.FindAsync(id);
            if (inspection == null)
            {
                return NotFound();
            }
            ViewData["PremisesId"] = new SelectList(_context.Premises, "Id", "Address", inspection.PremisesId);
            return View(inspection);
        }

        // POST: Inspections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PremisesId,InspectionDate,Score,Outcome,Notes")] Inspection inspection)
        {
            if (id != inspection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Updating inspection Id={Id} by {User}",
                        inspection.Id, User.Identity?.Name);

                    _context.Update(inspection);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("Inspection updated successfully. Id={Id}, Score={Score}, Outcome={Outcome}",
                        inspection.Id, inspection.Score, inspection.Outcome);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError(ex, "Concurrency error while updating inspection Id={Id}", inspection.Id);

                    if (!InspectionExists(inspection.Id))
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

            ViewData["PremisesId"] = new SelectList(_context.Premises, "Id", "Address", inspection.PremisesId);
            return View(inspection);
        }

        // GET: Inspections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inspection = await _context.Inspections
                .Include(i => i.Premises)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inspection == null)
            {
                return NotFound();
            }

            return View(inspection);
        }

        // POST: Inspections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inspection = await _context.Inspections.FindAsync(id);

            if (inspection != null)
            {
                _logger.LogInformation("Deleting inspection Id={Id} by {User}",
                    inspection.Id, User.Identity?.Name);

                _context.Inspections.Remove(inspection);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Inspection deleted successfully. Id={Id}", inspection.Id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool InspectionExists(int id)
        {
            return _context.Inspections.Any(e => e.Id == id);
        }
    }
}
