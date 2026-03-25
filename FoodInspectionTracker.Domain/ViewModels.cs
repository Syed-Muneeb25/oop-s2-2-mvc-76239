using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodInspectionTracker.Domain
{
    public class DashboardViewModel
    {
        public int InspectionsThisMonth { get; set; }
        public int FailedInspectionsThisMonth { get; set; }
        public int OverdueFollowUps { get; set; }
        public List<InspectionSummary> RecentInspections { get; set; } = new();


        public string? FilterTown { get; set; }
        public RiskRating? FilterRiskRating { get; set; }
        public List<string> AvailableTowns { get; set; } = new();
    }

    public class InspectionSummary
    {
        public int InspectionId { get; set; }
        public string PremisesName { get; set; } = string.Empty;
        public string Town { get; set; } = string.Empty;
        public DateTime InspectionDate { get; set; }
        public int Score { get; set; }
        public InspectionOutcome Outcome { get; set; }
        public RiskRating RiskRating { get; set; }
    }

    public class CreateInspectionViewModel
    {
        public int PremisesId { get; set; }
        public DateTime InspectionDate { get; set; } = DateTime.Today;
        public int Score { get; set; }
        public InspectionOutcome Outcome { get; set; }
        public string? Notes { get; set; }
    }

    public class CreateFollowUpViewModel
    {
        public int InspectionId { get; set; }
        public DateTime DueDate { get; set; } = DateTime.Today.AddDays(14);
        public string? Notes { get; set; }
    }

}