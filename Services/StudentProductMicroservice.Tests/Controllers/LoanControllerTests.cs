using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentProductMicroservice.Controllers;
using Moq;
using StudentProductMicroservice.Models;
using StudentProductMicroservice.Services;

[TestClass]
public class LoanControllerTests
{
    private Mock<ILoanService> _mockLoanService;
    private Mock<ILoanFactory> _mockLoanFactory;
    private LoanController _controller;

    [TestInitialize]
    public void SetUp()
    {
        _mockLoanService = new Mock<ILoanService>();
        _mockLoanFactory = new Mock<ILoanFactory>();
        _controller = new LoanController(_mockLoanService.Object, _mockLoanFactory.Object);
    }

    [TestMethod]
    public async Task GetAll_ShouldReturnAllLoans()
    {
        // Arrange
        var loans = new List<Loan> { new
        InterestBearingLoan(
            studentId: 12345,
            monthlyAmount: 100.00m,
            startDate: new DateTime(2024, 1, 15),
            endDate: new DateTime(2028, 1, 15)),

            new TuitionFeesLoan(studentId: 12345,
            monthlyAmount: 100.00m,
            startDate: new DateTime(2024, 1, 15),
            endDate: new DateTime(2028, 1, 15))};

        _mockLoanService.Setup(s => s.GetAllLoansAsync()).ReturnsAsync(loans);

        // Act
        var result = await _controller.GetAll();

        // Assert
        Assert.IsNotNull(result);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        var returnLoans = okResult.Value as IEnumerable<Loan>;
        Assert.IsNotNull(returnLoans);
        Assert.AreEqual(loans.Count, returnLoans.Count());
        Assert.AreEqual(loans, okResult.Value);
    }

}
