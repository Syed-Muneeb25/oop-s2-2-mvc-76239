using FoodInspectionTracker.MVC.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace FoodInspectionTracker.Tests
{
    public class SeedDataTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly AppDbContext _context;

        public SeedDataTests()
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
        public void SeedData_ShouldCreateExpectedNumberOfRecords()
        {
            Assert.Equal(12, _context.Premises.Count());
            Assert.Equal(25, _context.Inspections.Count());
            Assert.Equal(10, _context.FollowUps.Count());
        }

        public void Dispose()
        {
            _context.Dispose();
            _connection.Dispose();
        }
    }
}