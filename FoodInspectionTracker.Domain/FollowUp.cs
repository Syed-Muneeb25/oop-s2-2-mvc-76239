using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodInspectionTracker.Domain
{
    public enum FollowUpStatus { Open, Closed }
    public class FollowUp
    {
        public int Id { get; set; }
        public int InspectionId { get; set; }
        public Inspection? Inspection { get; set; }

        public DateTime DueDate { get; set; }
        [NotMapped]
        public FollowUpStatus Status { get; set; }
        public DateTime? ClosedDate { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }
    }

}
