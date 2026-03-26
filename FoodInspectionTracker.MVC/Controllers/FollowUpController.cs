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
    public class FollowUpController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<FollowUpController> _logger;

        public FollowUpController(AppDbContext context, ILogger<FollowUpController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: FollowUp
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.FollowUps.Include(f => f.Inspection);
            return View(await appDbContext.ToListAsync());
        }

        // GET: FollowUp/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var followUp = await _context.FollowUps
                .Include(f => f.Inspection)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (followUp == null)
            {
                return NotFound();
            }

            return View(followUp);
        }

        // GET: FollowUp/Create
        public IActionResult Create()
        {
            ViewData["InspectionId"] = new SelectList(_context.Inspections, "Id", "Id");
            return View();
        }

        // POST: FollowUp/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InspectionId,DueDate,Status,ClosedDate,Notes")] FollowUp followUp)
        {
            if (followUp.Status == FollowUpStatus.Closed && followUp.ClosedDate == null)
            {
                ModelState.AddModelError("ClosedDate", "ClosedDate is required when status is Closed.");
                _logger.LogWarning("Follow-up validation failed: Closed follow-up without ClosedDate. InspectionId={InspectionId}",
                    followUp.InspectionId);
            }

            if (ModelState.IsValid)
            {
                _logger.LogInformation("Creating follow-up for InspectionId={InspectionId} by {User}",
                    followUp.InspectionId, User.Identity?.Name);

                _context.Add(followUp);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Follow-up created successfully. Id={Id}, DueDate={DueDate}, Status={Status}",
                    followUp.Id, followUp.DueDate, followUp.Status);

                return RedirectToAction(nameof(Index));
            }

            ViewData["InspectionId"] = new SelectList(_context.Inspections, "Id", "Id", followUp.InspectionId);
            return View(followUp);
        }

        // GET: FollowUp/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var followUp = await _context.FollowUps.FindAsync(id);
            if (followUp == null)
            {
                return NotFound();
            }
            ViewData["InspectionId"] = new SelectList(_context.Inspections, "Id", "Id", followUp.InspectionId);
            return View(followUp);
        }

        // POST: FollowUp/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InspectionId,DueDate,Status,ClosedDate,Notes")] FollowUp followUp)
        {
            if (id != followUp.Id)
            {
                return NotFound();
            }

            if (followUp.Status == FollowUpStatus.Closed && followUp.ClosedDate == null)
            {
                ModelState.AddModelError("ClosedDate", "ClosedDate is required when status is Closed.");
                _logger.LogWarning("Follow-up validation failed during edit: Closed follow-up without ClosedDate. FollowUpId={FollowUpId}",
                    followUp.Id);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Updating follow-up Id={Id} by {User}",
                        followUp.Id, User.Identity?.Name);

                    _context.Update(followUp);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("Follow-up updated successfully. Id={Id}, Status={Status}, ClosedDate={ClosedDate}",
                        followUp.Id, followUp.Status, followUp.ClosedDate);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError(ex, "Concurrency error while updating follow-up Id={Id}", followUp.Id);

                    if (!FollowUpExists(followUp.Id))
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

            ViewData["InspectionId"] = new SelectList(_context.Inspections, "Id", "Id", followUp.InspectionId);
            return View(followUp);
        }

        // GET: FollowUp/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var followUp = await _context.FollowUps
                .Include(f => f.Inspection)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (followUp == null)
            {
                return NotFound();
            }

            return View(followUp);
        }

        // POST: FollowUp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var followUp = await _context.FollowUps.FindAsync(id);

            if (followUp != null)
            {
                _logger.LogInformation("Deleting follow-up Id={Id} by {User}",
                    followUp.Id, User.Identity?.Name);

                _context.FollowUps.Remove(followUp);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Follow-up deleted successfully. Id={Id}", followUp.Id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool FollowUpExists(int id)
        {
            return _context.FollowUps.Any(e => e.Id == id);
        }
    }
}
