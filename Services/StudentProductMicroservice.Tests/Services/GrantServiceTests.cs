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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using StudentProductMicroservice.Models;
using StudentProductMicroservice.Repository;
using StudentProductMicroservice.Services;

[TestClass]
public class GrantServiceTest
{
    private Mock<IUnitOfWork> _mockUnitOfWork;
    private Mock<IGrantRepository> _mockGrantRepository;
    private HttpClient _mockHttpClient;
    private GrantService _grantService;
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

        // create the HttpClient
        _mockHttpClient = new HttpClient(_httpMessageHandlerMock.Object);

        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockGrantRepository = new Mock<IGrantRepository>();
        _mockUnitOfWork.Setup(uow => uow.GrantsRepository).Returns(_mockGrantRepository.Object);
        _grantService = new GrantService(_mockUnitOfWork.Object, _mockHttpClient);

        
    }

    [TestMethod]
    public async Task GetAllGrants_ShouldReturnGrants_WhenGrantsExist()
    {
        // Arrange
        var grants = new List<Grant>
        {
            new BasicGrant(studentId: 1, monthlyAmount: 500.00m, startDate: DateTime.Now, endDate: DateTime.Now.AddYears(1)),
            new SupplementaryGrant(studentId: 2, monthlyAmount: 300.00m, startDate: DateTime.Now, endDate: DateTime.Now.AddYears(1))

        };

        _mockGrantRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(grants);

        // Act
        var result = await _grantService.GetAllGrantsAsync();

        // Assert
        Assert.AreEqual(grants.Count, result.Count());
        _mockGrantRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
    }

    [TestMethod]
    public async Task SendPayoutToTransactionMicroservice_ShouldSendRequestToTransactionsMicroservice()
    {
        // Arrange
        var payout = new Payout(studentID: 1, productID: 2, date: DateTime.Now, amount: 100);
        var expectedJsonData = JsonConvert.SerializeObject(payout);

        // Act
        var result = await _grantService.SendPayoutToTransactionMicroservice(payout);

        // Assert
        _httpMessageHandlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req =>
            req.Method == HttpMethod.Post &&
            req.RequestUri == new Uri("https://localhost:7299/api/Payout") && 
            req.Content.ReadAsStringAsync().Result == expectedJsonData 
        ),
        ItExpr.IsAny<CancellationToken>()
);


    }
}

