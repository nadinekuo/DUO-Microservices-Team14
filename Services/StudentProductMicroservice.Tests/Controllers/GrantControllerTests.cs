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
using StudentProductMicroservice.Factories;

[TestClass]
public class GrantControllerTests
{
    private Mock<IGrantService> _mockGrantService;

    private Mock<IGrantFactory> _mockGrantFactory;
    private GrantController _controller;
    [TestInitialize]
    public void SetUp()
    {
        _mockGrantService = new Mock<IGrantService>();
        _mockGrantFactory = new Mock<IGrantFactory>();
        _controller = new GrantController(_mockGrantService.Object, _mockGrantFactory.Object);
    }

    [TestMethod]
    public async Task GetAll_ShouldReturnAllGrants()
    {
        // Arrange
        var grants = new List<Grant> { new
        TravelProduct(
            studentId: 12345,
            monthlyAmount: 100.00m,
            startDate: new DateTime(2024, 1, 15),
            endDate: new DateTime(2028, 1, 15), IsWeek: true),

            new BasicGrant(studentId: 12345,
            monthlyAmount: 100.00m,
            startDate: new DateTime(2024, 1, 15),
            endDate: new DateTime(2028, 1, 15))};

        _mockGrantService.Setup(s => s.GetAllGrantsAsync()).ReturnsAsync(grants);

        // Act
        var result = await _controller.GetAll();

        // Assert
        Assert.IsNotNull(result);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        var returnGrants = okResult.Value as IEnumerable<Grant>;
        Assert.IsNotNull(returnGrants);
        Assert.AreEqual(grants.Count, returnGrants.Count());
        Assert.AreEqual(grants, okResult.Value);
    }

}
