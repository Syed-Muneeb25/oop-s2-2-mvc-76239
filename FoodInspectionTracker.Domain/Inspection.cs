using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodInspectionTracker.Domain
{
    public enum InspectionOutcome { Pass, Fail }
    public class Inspection
    {
        public int Id { get; set; }
        public int PremisesId { get; set; }
        public Premises? Premises { get; set; }

        public DateTime InspectionDate { get; set; }
        [Range(0, 100, ErrorMessage = "Score must be between 0 and 100.")]
        public int Score { get; set; }
        [RegularExpression("Pass|Fail", ErrorMessage = "Outcome must be Pass or Fail.")]
        public InspectionOutcome Outcome { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }

        public ICollection<FollowUp> FollowUps { get; set; } = new List<FollowUp>();
    }

}
