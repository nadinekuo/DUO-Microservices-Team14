using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using StudentProductMicroservice.Models;
using StudentProductMicroservice.Repository;
using StudentProductMicroservice.Services;

[TestClass]
public class LoanServiceTest
{
    private Mock<IUnitOfWork> _mockUnitOfWork;
    private Mock<ILoanRepository> _mockLoanRepository;
    private LoanService _loanService;
    private HttpClient _mockHttpClient;
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
        _mockLoanRepository = new Mock<ILoanRepository>();
        _mockUnitOfWork.Setup(uow => uow.LoansRepository).Returns(_mockLoanRepository.Object);
        _loanService = new LoanService(_mockUnitOfWork.Object, _mockHttpClient);
    }

    [TestMethod]
    public async Task GetAllLoans_ShouldReturnLoans_WhenLoansExist()
    {
        // Arrange
        var loans = new List<Loan>
        {
            new InterestBearingLoan(studentId: 1, monthlyAmount: 500.00m, startDate: DateTime.Now, endDate: DateTime.Now.AddYears(1)),
            new TuitionFeesLoan(studentId: 2, monthlyAmount: 300.00m, startDate: DateTime.Now, endDate: DateTime.Now.AddYears(1))

        };

        _mockLoanRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(loans);

        // Act
        var result = await _loanService.GetAllLoansAsync();

        // Assert
        Assert.AreEqual(loans.Count, result.Count());
        _mockLoanRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
    }

    [TestMethod]
    public async Task SendPayoutToTransactionMicroservice_ShouldSendRequestToTransactionsMicroservice()
    {
        // Arrange
        var payout = new Payout(studentID: 1, productID: 2, date: DateTime.Now, amount: 100);

        var expectedJsonData = JsonConvert.SerializeObject(payout);

        // Act
        var result = await _loanService.SendPayoutToTransactionMicroservice(payout);

        // Assert
        _httpMessageHandlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req =>
            req.Method == HttpMethod.Post &&
            req.RequestUri == new Uri("https://localhost:7299/api/Payout") &&
            req.Content.ReadAsStringAsync().Result == expectedJsonData
        ),
        ItExpr.IsAny<CancellationToken>());
    }

}
