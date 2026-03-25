using FoodInspectionTracker.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodInspectionTracker.MVC.Data
{

    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Premises> Premises => Set<Premises>();
        public DbSet<Inspection> Inspections => Set<Inspection>();
        public DbSet<FollowUp> FollowUps => Set<FollowUp>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Premises>()
                .HasMany(p => p.Inspections)
                .WithOne(i => i.Premises)
                .HasForeignKey(i => i.PremisesId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Inspection>()
                .HasMany(i => i.FollowUps)
                .WithOne(f => f.Inspection)
                .HasForeignKey(f => f.InspectionId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<Premises>().HasData(
                new Premises { Id = 1, Name = "The Golden Fork", Address = "12 High St", Town = "Dorchester", RiskRating = RiskRating.Low },
                new Premises { Id = 2, Name = "Burger Palace", Address = "34 Market Sq", Town = "Dorchester", RiskRating = RiskRating.Medium },
                new Premises { Id = 3, Name = "Sea Breeze Fish & Chips", Address = "5 Harbour Rd", Town = "Dorchester", RiskRating = RiskRating.High },
                new Premises { Id = 4, Name = "Mama's Italian Kitchen", Address = "78 Church Lane", Town = "Dorchester", RiskRating = RiskRating.Low },
                new Premises { Id = 5, Name = "Sunrise Cafe", Address = "2 Station Rd", Town = "Weymouth", RiskRating = RiskRating.Medium },
                new Premises { Id = 6, Name = "The Rusty Anchor", Address = "19 Pier Approach", Town = "Weymouth", RiskRating = RiskRating.High },
                new Premises { Id = 7, Name = "Green Garden Deli", Address = "45 Park Ave", Town = "Weymouth", RiskRating = RiskRating.Low },
                new Premises { Id = 8, Name = "Hot Wok Express", Address = "7 Broadway", Town = "Weymouth", RiskRating = RiskRating.Medium },
                new Premises { Id = 9, Name = "The Crown Pub", Address = "1 Kings Rd", Town = "Bridport", RiskRating = RiskRating.Medium },
                new Premises { Id = 10, Name = "Bridport Bakery", Address = "22 West St", Town = "Bridport", RiskRating = RiskRating.Low },
                new Premises { Id = 11, Name = "Spice Route", Address = "88 South St", Town = "Bridport", RiskRating = RiskRating.High },
                new Premises { Id = 12, Name = "Coastline Catering", Address = "3 Cliff Road", Town = "Bridport", RiskRating = RiskRating.Medium }
            );


            var now = DateTime.UtcNow;
            builder.Entity<Inspection>().HasData(
                new Inspection { Id = 1, PremisesId = 1, InspectionDate = now.AddMonths(-6), Score = 92, Outcome = InspectionOutcome.Pass, Notes = "Excellent hygiene standards." },
                new Inspection { Id = 2, PremisesId = 2, InspectionDate = now.AddMonths(-5), Score = 55, Outcome = InspectionOutcome.Fail, Notes = "Inadequate cold storage temperatures." },
                new Inspection { Id = 3, PremisesId = 3, InspectionDate = now.AddMonths(-5), Score = 48, Outcome = InspectionOutcome.Fail, Notes = "Cross-contamination risks identified." },
                new Inspection { Id = 4, PremisesId = 4, InspectionDate = now.AddMonths(-4), Score = 88, Outcome = InspectionOutcome.Pass, Notes = "Good overall, minor paperwork issues." },
                new Inspection { Id = 5, PremisesId = 5, InspectionDate = now.AddMonths(-4), Score = 76, Outcome = InspectionOutcome.Pass, Notes = "Satisfactory." },
                new Inspection { Id = 6, PremisesId = 6, InspectionDate = now.AddMonths(-3), Score = 41, Outcome = InspectionOutcome.Fail, Notes = "Rodent evidence found in storeroom." },
                new Inspection { Id = 7, PremisesId = 7, InspectionDate = now.AddMonths(-3), Score = 95, Outcome = InspectionOutcome.Pass, Notes = "Outstanding. No issues." },
                new Inspection { Id = 8, PremisesId = 8, InspectionDate = now.AddMonths(-2), Score = 63, Outcome = InspectionOutcome.Pass, Notes = "Pass with recommendations." },
                new Inspection { Id = 9, PremisesId = 9, InspectionDate = now.AddMonths(-2), Score = 52, Outcome = InspectionOutcome.Fail, Notes = "Handwashing facilities non-compliant." },
                new Inspection { Id = 10, PremisesId = 10, InspectionDate = now.AddMonths(-2), Score = 84, Outcome = InspectionOutcome.Pass, Notes = "Good standards maintained." },
                new Inspection { Id = 11, PremisesId = 11, InspectionDate = now.AddMonths(-1), Score = 38, Outcome = InspectionOutcome.Fail, Notes = "Critical violations — pest control." },
                new Inspection { Id = 12, PremisesId = 12, InspectionDate = now.AddMonths(-1), Score = 71, Outcome = InspectionOutcome.Pass, Notes = "Adequate, staff training recommended." },
                new Inspection { Id = 13, PremisesId = 1, InspectionDate = now.AddMonths(-1), Score = 90, Outcome = InspectionOutcome.Pass, Notes = "Follow-up: still excellent." },
                new Inspection { Id = 14, PremisesId = 2, InspectionDate = now.AddDays(-25), Score = 68, Outcome = InspectionOutcome.Pass, Notes = "Cold storage now fixed." },
                new Inspection { Id = 15, PremisesId = 3, InspectionDate = now.AddDays(-20), Score = 59, Outcome = InspectionOutcome.Fail, Notes = "Still non-compliant on storage." },
                new Inspection { Id = 16, PremisesId = 4, InspectionDate = now.AddDays(-18), Score = 91, Outcome = InspectionOutcome.Pass, Notes = "Paperwork now in order." },
                new Inspection { Id = 17, PremisesId = 5, InspectionDate = now.AddDays(-15), Score = 80, Outcome = InspectionOutcome.Pass, Notes = "Consistent performance." },
                new Inspection { Id = 18, PremisesId = 6, InspectionDate = now.AddDays(-12), Score = 66, Outcome = InspectionOutcome.Pass, Notes = "Pest issue resolved." },
                new Inspection { Id = 19, PremisesId = 7, InspectionDate = now.AddDays(-10), Score = 97, Outcome = InspectionOutcome.Pass, Notes = "Exemplary." },
                new Inspection { Id = 20, PremisesId = 8, InspectionDate = now.AddDays(-8), Score = 74, Outcome = InspectionOutcome.Pass, Notes = "Improvements noted." },
                new Inspection { Id = 21, PremisesId = 9, InspectionDate = now.AddDays(-6), Score = 70, Outcome = InspectionOutcome.Pass, Notes = "Handwashing facilities upgraded." },
                new Inspection { Id = 22, PremisesId = 10, InspectionDate = now.AddDays(-5), Score = 88, Outcome = InspectionOutcome.Pass, Notes = "Consistent high standard." },
                new Inspection { Id = 23, PremisesId = 11, InspectionDate = now.AddDays(-4), Score = 45, Outcome = InspectionOutcome.Fail, Notes = "Partial improvement, still failing." },
                new Inspection { Id = 24, PremisesId = 12, InspectionDate = now.AddDays(-3), Score = 83, Outcome = InspectionOutcome.Pass, Notes = "Good progress." },
                new Inspection { Id = 25, PremisesId = 6, InspectionDate = now.AddDays(-2), Score = 78, Outcome = InspectionOutcome.Pass, Notes = "All cleared." }
            );


            builder.Entity<FollowUp>().HasData(
                new FollowUp { Id = 1, InspectionId = 2, DueDate = now.AddMonths(-4), Status = FollowUpStatus.Closed, ClosedDate = now.AddMonths(-4).AddDays(10), Notes = "Cold storage repaired and verified." },
                new FollowUp { Id = 2, InspectionId = 3, DueDate = now.AddMonths(-3), Status = FollowUpStatus.Closed, ClosedDate = now.AddMonths(-3).AddDays(5), Notes = "Separation protocols implemented." },
                new FollowUp { Id = 3, InspectionId = 6, DueDate = now.AddMonths(-2), Status = FollowUpStatus.Closed, ClosedDate = now.AddMonths(-2).AddDays(8), Notes = "Pest control contractor report submitted." },
                new FollowUp { Id = 4, InspectionId = 9, DueDate = now.AddDays(-30), Status = FollowUpStatus.Open, ClosedDate = null, Notes = "Handwashing facilities replacement pending." },
                new FollowUp { Id = 5, InspectionId = 11, DueDate = now.AddDays(-20), Status = FollowUpStatus.Open, ClosedDate = null, Notes = "Pest control contract not yet signed." },
                new FollowUp { Id = 6, InspectionId = 15, DueDate = now.AddDays(-10), Status = FollowUpStatus.Open, ClosedDate = null, Notes = "Storage unit replacement ordered." },
                new FollowUp { Id = 7, InspectionId = 23, DueDate = now.AddDays(-3), Status = FollowUpStatus.Open, ClosedDate = null, Notes = "Deep clean and pest exclusion required." },
                new FollowUp { Id = 8, InspectionId = 23, DueDate = now.AddDays(7), Status = FollowUpStatus.Open, ClosedDate = null, Notes = "Staff food hygiene training scheduled." },
                new FollowUp { Id = 9, InspectionId = 15, DueDate = now.AddDays(14), Status = FollowUpStatus.Open, ClosedDate = null, Notes = "Re-inspection scheduled for next month." },
                new FollowUp { Id = 10, InspectionId = 9, DueDate = now.AddMonths(-1), Status = FollowUpStatus.Closed, ClosedDate = now.AddDays(-20), Notes = "Interim fix verified by inspector." }
            );
        }
    } 
}