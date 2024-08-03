using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf.Compiler;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using TransactionMicroservice.Models;
using TransactionMicroservice.Repository;
using TransactionMicroservice.Services;

[TestClass]
public class PayoutServiceTest
{
    private Mock<IUnitOfWork> _mockUnitOfWork;
    private Mock<IPayoutRepository> _mockPayoutRepository;

    private IConfiguration _configuration;
    private HttpClient _mockHttpClient;
    private PayoutService _payoutService;
    private Mock<HttpMessageHandler> _httpMessageHandlerMock;

    [TestInitialize]
    public void SetUp()
    {
        // Instantiate the mock object
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        // Set up the SendAsync method behavior.
        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("mocked API response")
            });

        var configuration = new Mock<IConfiguration>();
        // Setup configuration mock
        configuration.Setup(c => c[It.Is<string>(s => s == "StubServer:BaseUrl")]).Returns("http://localhost:5000");

        _configuration = configuration.Object;
        // create the HttpClient
        _mockHttpClient = new HttpClient(_httpMessageHandlerMock.Object);

        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockPayoutRepository = new Mock<IPayoutRepository>();
        _mockUnitOfWork.Setup(uow => uow.PayoutRepository).Returns(_mockPayoutRepository.Object);
        _payoutService = new PayoutService(_mockUnitOfWork.Object, _configuration, _mockHttpClient);




    }


    [TestMethod]
    public async Task GetPayoutById_ShouldReturnPayout_WhenPayoutExists()
    {
        // Arrange
        var expectedPayout = new Payout(1, 1, DateTime.Now, 500.00m);
        _mockPayoutRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(expectedPayout);

        // Act
        var result = await _payoutService.GetPayoutByIdAsync(1);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedPayout, result);
    }

    [TestMethod]
    public async Task CreatePayout_ShouldReturnPayout_WhenPayoutIsCreated()
    {
        // Arrange
        var newPayout = new Payout(1, 1, DateTime.Now, 500.00m);
        _mockPayoutRepository.Setup(repo => repo.AddAsync(It.IsAny<Payout>())).Returns(Task.CompletedTask);

        // Act
        var result = await _payoutService.CreatePayoutAsync(newPayout);

        // Assert
        Assert.IsNotNull(result);
        _mockPayoutRepository.Verify(repo => repo.AddAsync(It.IsAny<Payout>()), Times.Once);
    }

    [TestMethod]
    public async Task UpdatePayout_ShouldReturnTrue_WhenPayoutExists()
    {
        // Arrange
        var existingPayout = new Payout(1, 1, DateTime.Now, 500.00m);
        _mockPayoutRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(existingPayout);
        _mockPayoutRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Payout>())).Returns(Task.CompletedTask);

        // Act
        var result = await _payoutService.UpdatePayoutAsync(1, existingPayout);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task DeletePayout_ShouldReturnTrue_WhenPayoutExists()
    {
        // Arrange
        var existingPayout = new Payout(1, 1, DateTime.Now, 500.00m); ;
        _mockPayoutRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(existingPayout);
        _mockPayoutRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

        // Act
        var result = await _payoutService.DeletePayoutAsync(1);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task SendPayment_ShouldReturnSuccessResult_WhenPaymentIsSuccessful()
    {
        // Arrange
        var transactionDetails = new TransactionDetails(1, 1, "student", "DUO", 500.00m, "Monthly Payout", DateTime.Now);
        var transactionResult = new TransactionResult { Success = true };
        var serializedTransactionResult = JsonConvert.SerializeObject(new { Result = transactionResult });

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(serializedTransactionResult)
            });

        // Act
        var result = await _payoutService.SendPaymentAsync(transactionDetails);

        // Assert
        Assert.IsTrue(result.Success);
        _mockPayoutRepository.Verify(repo => repo.AddAsync(It.IsAny<Payout>()), Times.Once);
    }

}
