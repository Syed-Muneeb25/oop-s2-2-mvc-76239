using FoodInspectionTracker.Domain;
using FoodInspectionTracker.MVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodInspectionTracker.MVC.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _db;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(AppDbContext db, ILogger<DashboardController> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string? filterTown, RiskRating? filterRiskRating)
        {
            _logger.LogInformation("Dashboard accessed by {User} with filters Town={Town}, RiskRating={RiskRating}",
                User.Identity?.Name, filterTown ?? "none", filterRiskRating?.ToString() ?? "none");

            var now = DateTime.UtcNow;
            var startOfMonth = new DateTime(now.Year, now.Month, 1);

            var premisesQuery = _db.Premises.AsQueryable();
            if (!string.IsNullOrEmpty(filterTown))
                premisesQuery = premisesQuery.Where(p => p.Town == filterTown);
            if (filterRiskRating.HasValue)
                premisesQuery = premisesQuery.Where(p => p.RiskRating == filterRiskRating.Value);

            var filteredPremisesIds = await premisesQuery.Select(p => p.Id).ToListAsync();

            var inspectionsQuery = _db.Inspections
                .Where(i => filteredPremisesIds.Contains(i.PremisesId));

            var inspectionsThisMonth = await inspectionsQuery
                .CountAsync(i => i.InspectionDate >= startOfMonth);

            var failedThisMonth = await inspectionsQuery
                .CountAsync(i => i.InspectionDate >= startOfMonth && i.Outcome == InspectionOutcome.Fail);

            var overdueFollowUps = await _db.FollowUps
                .Include(f => f.Inspection)
                .CountAsync(f => filteredPremisesIds.Contains(f.Inspection.PremisesId)
                              && f.DueDate < now
                              && f.Status == FollowUpStatus.Open);

            var recentInspections = await inspectionsQuery
                .Include(i => i.Premises)
                .OrderByDescending(i => i.InspectionDate)
                .Take(10)
                .Select(i => new InspectionSummary
                {
                    InspectionId = i.Id,
                    PremisesName = i.Premises.Name,
                    Town = i.Premises.Town,
                    InspectionDate = i.InspectionDate,
                    Score = i.Score,
                    Outcome = i.Outcome,
                    RiskRating = i.Premises.RiskRating
                })
                .ToListAsync();

            var towns = await _db.Premises.Select(p => p.Town).Distinct().OrderBy(t => t).ToListAsync();

            if (overdueFollowUps > 0)
                _logger.LogWarning("Dashboard: {Count} overdue follow-ups detected for current filter set", overdueFollowUps);

            var vm = new DashboardViewModel
            {
                InspectionsThisMonth = inspectionsThisMonth,
                FailedInspectionsThisMonth = failedThisMonth,
                OverdueFollowUps = overdueFollowUps,
                RecentInspections = recentInspections,
                FilterTown = filterTown,
                FilterRiskRating = filterRiskRating,
                AvailableTowns = towns
            };

            return View(vm);
        }
    }