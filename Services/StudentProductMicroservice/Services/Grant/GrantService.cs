using StudentProductMicroservice.Models;
using StudentProductMicroservice.Repository;
using StudentProductMicroservice.Services;
using System.Text;
using Newtonsoft.Json; // For JSON serialization

public class GrantService : IGrantService
{

    public IUnitOfWork _unitOfWork;

    private readonly HttpClient _httpClient;

    public GrantService(IUnitOfWork unitOfWork, HttpClient httpClient)
    {
        _unitOfWork = unitOfWork;
        _httpClient = httpClient;
        // Configure HttpClient base address and any other settings as needed
        _httpClient.BaseAddress = new Uri("https://localhost:7299");
    }

    public async Task<IEnumerable<Grant>> GetAllGrantsAsync()
    {
        return await _unitOfWork.GrantsRepository.GetAllAsync();
    }

    public async Task<Grant> GetGrantByIdAsync(int id)
    {
        var grant = await _unitOfWork.GrantsRepository.GetByIdAsync(id);
        if (grant == null)
        {
            // Handle the case where the grant is not found
            throw new KeyNotFoundException($"Grant with ID {id} not found.");
        }
        return grant;
    }

    public async Task<Grant> CreateGrantAsync(Grant grant)
    {
        ValidateGrant(grant);

        await _unitOfWork.GrantsRepository.AddAsync(grant);
        await _unitOfWork.CommitAsync();
        return grant;
    }

    public async Task<bool> UpdateGrantAsync(int id, Grant grant)
    {
        var grantToUpdate = await GetGrantByIdAsync(id);
        if (grantToUpdate == null)
        {
            return false;
        }
        ValidateGrant(grant);
        // Business logic to update the grant's properties
        grantToUpdate.MonthlyAmount = grant.MonthlyAmount;
        grantToUpdate.StartDate = grant.StartDate;
        grantToUpdate.EndDate = grant.EndDate;
        // Other updates based on the request
        await _unitOfWork.GrantsRepository.UpdateAsync(grantToUpdate);
        await _unitOfWork.CommitAsync();
        return true;
    }
    public async Task<bool> DeleteGrantAsync(int id)
    {
        var grant = await _unitOfWork.GrantsRepository.GetByIdAsync(id);
        // Handle the case where the grant does not exist
        if (grant == null)
        {
            return false;
        }

        await _unitOfWork.GrantsRepository.DeleteAsync(id);
        await _unitOfWork.CommitAsync();
        return true;
    }

    // Expand with more rules
    private void ValidateGrant(Grant grant)
    {
        // Amount cannot be negative
        if (grant.MonthlyAmount <= 0)
        {
            throw new ArgumentException("Monthly amount must be greater than zero.");
        }

        // Check for a valid date range (start date should be before end date)
        if (grant.StartDate >= grant.EndDate)
        {
            throw new ArgumentException("Start date must be before end date.");
        }

        // Ensure the start date is not in the past
        if (grant.StartDate < DateTime.Today)
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
