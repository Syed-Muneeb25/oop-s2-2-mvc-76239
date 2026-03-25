using FoodInspectionTracker.Domain;
using FoodInspectionTracker.MVC.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FoodInspectionTracker.Tests
{
    public class FollowUpTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly AppDbContext _context;

        public FollowUpTests()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(_connection)
                .Options;

            _context = new AppDbContext(options);
            _context.Database.EnsureCreated();
        }

        [Fact]
        public void FollowUps_OverdueOpenFollowUps_ShouldBeFour()
        {
            var referenceDate = new DateTime(2026, 3, 1, 0, 0, 0, DateTimeKind.Utc);

            var overdueOpenFollowUps = _context.FollowUps
                .Where(f => f.DueDate < referenceDate && f.Status == FollowUpStatus.Open)
                .ToList();

            Assert.Equal(4, overdueOpenFollowUps.Count);
        }

        public void Dispose()
        {
            _context.Dispose();
            _connection.Dispose();
        }
    }
}