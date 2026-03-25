using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodInspectionTracker.Domain
{
    public enum RiskRating { Low, Medium, High }
    public class Premises
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Town { get; set; }
        [RegularExpression("Low|Medium|High", ErrorMessage = "RiskRating must be Low, Medium, or High.")]
        public RiskRating RiskRating { get; set; }
        public ICollection<Inspection> Inspections { get; set; } = new List<Inspection>();
    }

}

