using Newtonsoft.Json;
using StudentProductMicroservice.Models;
using StudentProductMicroservice.Repository;
using StudentProductMicroservice.Services;
using System.Text;

public class LoanService : ILoanService
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly HttpClient _httpClient;

    public LoanService(IUnitOfWork unitOfWork, HttpClient httpClient)
    {
        _unitOfWork = unitOfWork;
        _httpClient = httpClient;
        // Configure HttpClient base address and any other settings as needed
        _httpClient.BaseAddress = new Uri("https://localhost:7299");
    }

    public async Task<IEnumerable<Loan>> GetAllLoansAsync()
    {
        return await _unitOfWork.LoansRepository.GetAllAsync();
    }

    public async Task<Loan> GetLoanByIdAsync(int id)
    {
        var loan = await _unitOfWork.LoansRepository.GetByIdAsync(id);
        if (loan == null)
        {
            throw new KeyNotFoundException($"Loan with ID {id} not found.");
        }
        return loan;
    }

    public async Task<Loan> CreateLoanAsync(Loan loan)
    {
        Validateloan(loan);
        await _unitOfWork.LoansRepository.AddAsync(loan);
        await _unitOfWork.CommitAsync();
        return loan;
    }

    public async Task<bool> UpdateLoanAsync(int id, Loan loan)
    {
        var loanToUpdate = await GetLoanByIdAsync(id);
        if (loanToUpdate == null)
        {
            return false;
        }

        Validateloan(loan);
        loanToUpdate.MonthlyAmount = loan.MonthlyAmount;
        loanToUpdate.StartDate = loan.StartDate;
        loanToUpdate.EndDate = loan.EndDate;
        // Implement additional property updates as needed

        await _unitOfWork.LoansRepository.UpdateAsync(loanToUpdate);
        await _unitOfWork.CommitAsync();
        return true;
    }

    public async Task<bool> DeleteLoanAsync(int id)
    {
        var loan = await _unitOfWork.LoansRepository.GetByIdAsync(id);
        if (loan == null)
        {
            return false;
        }

        await _unitOfWork.LoansRepository.DeleteAsync(id);
        await _unitOfWork.CommitAsync();
        return true;
    }

    // Expand with more rules
    public void Validateloan(Loan loan)
    {
        // Amount cannot be negative
        if (loan.MonthlyAmount <= 0)
        {
            throw new ArgumentException("Monthly amount must be greater than zero.");
        }

        // Check for a valid date range (start date should be before end date)
        if (loan.StartDate >= loan.EndDate)
        {
            throw new ArgumentException("Start date must be before end date.");
        }

        // Ensure the start date is not in the past
        if (loan.StartDate < DateTime.Today)
        {
            throw new ArgumentException("Start date cannot be in the past.");
        }
    }

    public async Task<string> SendPayoutToTransactionMicroservice(Payout payout)
    {
        try
        {
            // Serialize the data object to JSON
            string jsonData = JsonConvert.SerializeObject(payout);

            // Create the HTTP content with the serialized JSON data
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Make POST request to target microservice's API endpoint
            HttpResponseMessage response = await _httpClient.PostAsync("api/Payout", content);

            // Ensure successful response
            response.EnsureSuccessStatusCode();

            // Read response content
            string responseData = await response.Content.ReadAsStringAsync();
            return responseData;
        }
        catch (HttpRequestException ex)
        {
            // Handle HTTP request exception
            Console.WriteLine($"Error accessing microservice: {ex.Message}");
            throw;
        }
    }
}
