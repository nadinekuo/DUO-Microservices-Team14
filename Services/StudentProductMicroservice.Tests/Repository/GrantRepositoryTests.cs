using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentProductMicroservice.Models;
using StudentProductMicroservice.Repository;

[TestClass]
public class GrantRepositoryTest
{
    private StudentProductContext _context;
    private GrantRepository _grantRepository;

    [TestInitialize]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<StudentProductContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new StudentProductContext(options);
        _grantRepository = new GrantRepository(_context);
    }

    [TestMethod]
    public async Task GetAllAsync_ShouldReturnAllGrants()
    {
        // Arrange
        var expectedGrants = new List<Grant>
    {
        new BasicGrant(studentId: 12345, monthlyAmount: 100.00m, startDate: new DateTime(2024, 1, 15), endDate: new DateTime(2028, 1, 15)),
        new SupplementaryGrant(studentId: 54321, monthlyAmount: 200.00m, startDate: new DateTime(2024, 2, 11), endDate: new DateTime(2028, 3, 25))
    };

        foreach (var grant in expectedGrants)
        {
            _context.Grants.Add(grant);
        }
        _context.SaveChanges();

        // Act
        var result = await _grantRepository.GetAllAsync();

        // Assert
        Assert.AreEqual(expectedGrants.Count, result.Count());

        // Assert each property of the grants to ensure they match
        foreach (var expected in expectedGrants)
        {
            var actual = result.FirstOrDefault(g => g.StudentId == expected.StudentId);
            Assert.IsNotNull(actual, $"Grant with StudentId {expected.StudentId} not found");
            Assert.AreEqual(expected.MonthlyAmount, actual.MonthlyAmount, "MonthlyAmount does not match");
            Assert.AreEqual(expected.StartDate, actual.StartDate, "StartDate does not match");
            Assert.AreEqual(expected.EndDate, actual.EndDate, "EndDate does not match");
        }
    }


    // Clean up after tests
    [TestCleanup]
    public void CleanUp()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    // More tests...
}
