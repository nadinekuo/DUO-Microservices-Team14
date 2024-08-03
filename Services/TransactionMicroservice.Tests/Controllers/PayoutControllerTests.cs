using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.AspNetCore.Http;
using TransactionMicroservice.Models;
using TransactionMicroservice.Services;
using TransactionMicroservice.Controllers;

[TestClass]

public class PayoutControllerTests
{
    private Mock<IPayoutService> _mockPayoutService;
    private PayoutController _controller;


    [TestInitialize]
    public void SetUp()
    {
        _mockPayoutService = new Mock<IPayoutService>();
        _controller = new PayoutController(_mockPayoutService.Object);
    }

    [TestMethod]
    public async Task GetAll_ShouldReturnAllPayouts()
    {
        // Arrange
        var payouts = new List<Payout> { new
        Payout(
            studentID: 12345,
            productID: 1,
            date: new DateTime(2024, 1, 15),
            amount: 100),

            new Payout(
            studentID: 1,
            productID: 1,
            date: new DateTime(2024, 1, 16),
            amount: 140) };

        _mockPayoutService.Setup(s => s.GetAllPayoutsAsync()).ReturnsAsync(payouts);

        // Act
        var result = await _controller.GetAll();

        // Assert
        Assert.IsNotNull(result);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        var returnPayouts = okResult.Value as IEnumerable<Payout>;
        Assert.IsNotNull(returnPayouts);
        Assert.AreEqual(payouts.Count, returnPayouts.Count());
        Assert.AreEqual(payouts, okResult.Value);
    }

    [TestMethod]
    public async Task PostPayout_ShouldReturnCreated()
    {
        // Arrange
        var newPayout = new Payout(
            studentID: 12,
            productID: 1,
            date: new DateTime(2024, 1, 15),
            amount: 190
        );

        _mockPayoutService.Setup(s => s.CreatePayoutAsync(It.IsAny<Payout>())).ReturnsAsync(newPayout);



        // Act
        var result = await _controller.Create(newPayout);

        // Assert
        Assert.IsNotNull(result);


        var createdAtActionResult = result.Result as CreatedAtActionResult;
        Assert.IsNotNull(createdAtActionResult);
        Assert.AreEqual(StatusCodes.Status201Created, createdAtActionResult.StatusCode);

        var createdPayout = createdAtActionResult.Value as Payout;
        Assert.IsNotNull(createdPayout);
        // Now compare individual fields if necessary
        Assert.AreEqual(newPayout.StudentID, createdPayout.StudentID);

    }



}
