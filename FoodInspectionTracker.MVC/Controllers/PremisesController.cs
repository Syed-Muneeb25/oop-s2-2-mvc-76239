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
    [Authorize(Roles = "Admin")]
    public class PremisesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<PremisesController> _log;

        public PremisesController(AppDbContext context, ILogger<PremisesController> log)
        {
            _context = context;
            _log = log;
        }

        // GET: Premises
        public async Task<IActionResult> Index()
        {
            return View(await _context.Premises.ToListAsync());
        }

        // GET: Premises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var premises = await _context.Premises
                .FirstOrDefaultAsync(m => m.Id == id);
            if (premises == null)
            {
                return NotFound();
            }

            return View(premises);
        }

        // GET: Premises/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Premises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,Town,RiskRating")] Premises premises)
        {
            if (ModelState.IsValid)
            {
                _log.LogInformation("Creating premises {Name} in {Town} by {User}",
                    premises.Name, premises.Town, User.Identity?.Name);

                _context.Add(premises);
                await _context.SaveChangesAsync();

                _log.LogInformation("Premises created successfully. Id={Id}, Name={Name}",
                    premises.Id, premises.Name);

                return RedirectToAction(nameof(Index));
            }
            return View(premises);
        }

        // GET: Premises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var premises = await _context.Premises.FindAsync(id);
            if (premises == null)
            {
                return NotFound();
            }
            return View(premises);
        }

        // POST: Premises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,Town,RiskRating")] Premises premises)
        {
            if (id != premises.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _log.LogInformation("Updating premises Id={Id} by {User}",
                        premises.Id, User.Identity?.Name);

                    _context.Update(premises);
                    await _context.SaveChangesAsync();

                    _log.LogInformation("Premises updated successfully. Id={Id}, Name={Name}",
                        premises.Id, premises.Name);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _log.LogError(ex, "Concurrency error while updating premises Id={Id}", premises.Id);

                    if (!PremisesExists(premises.Id))
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
            return View(premises);
        }

        // GET: Premises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var premises = await _context.Premises
                .FirstOrDefaultAsync(m => m.Id == id);
            if (premises == null)
            {
                return NotFound();
            }

            return View(premises);
        }

        // POST: Premises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var premises = await _context.Premises.FindAsync(id);

            if (premises != null)
            {
                _log.LogInformation("Deleting premises Id={Id} by {User}",
                    premises.Id, User.Identity?.Name);

                _context.Premises.Remove(premises);
                await _context.SaveChangesAsync();

                _log.LogInformation("Premises deleted successfully. Id={Id}", premises.Id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PremisesExists(int id)
        {
            return _context.Premises.Any(e => e.Id == id);
        }
    }
}
