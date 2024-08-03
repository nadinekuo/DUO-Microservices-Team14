using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentProductMicroservice.Models;
using StudentProductMicroservice.Repository;

[TestClass]
public class LoanRepositoryTest
{
    private StudentProductContext _context;
    private LoanRepository _loanRepository;

    [TestInitialize]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<StudentProductContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new StudentProductContext(options);
        _loanRepository = new LoanRepository(_context);
    }

    [TestMethod]
    public async Task GetAllAsync_ShouldReturnAllLoans()
    {
        // Arrange
        var expectedLoans = new List<Loan>
    {
        new InterestBearingLoan(studentId: 12345, monthlyAmount: 100.00m, startDate: new DateTime(2024, 1, 15), endDate: new DateTime(2028, 1, 15)),
        new TuitionFeesLoan(studentId: 54321, monthlyAmount: 200.00m, startDate: new DateTime(2024, 2, 11), endDate: new DateTime(2028, 3, 25))
    };

        foreach (var loan in expectedLoans)
        {
            _context.Loans.Add(loan);
        }
        _context.SaveChanges();

        // Act
        var result = await _loanRepository.GetAllAsync();

        // Assert
        Assert.AreEqual(expectedLoans.Count, result.Count());

        // Assert each property of the loans to ensure they match
        foreach (var expected in expectedLoans)
        {
            var actual = result.FirstOrDefault(g => g.StudentId == expected.StudentId);
            Assert.IsNotNull(actual, $"Loan with StudentId {expected.StudentId} not found");
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
