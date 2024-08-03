using TransactionMicroservice.Services;
using TransactionMicroservice.Repository;
using TransactionMicroservice.Models;
using System.Text.Json;
public class PayoutService : IPayoutService
{
    public IUnitOfWork _unitOfWork;

    // Used to get stub port
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    public PayoutService(IUnitOfWork unitOfWork, IConfiguration configuration, HttpClient httpClient)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Payout>> GetAllPayoutsAsync()
    {
        return await _unitOfWork.PayoutRepository.GetAllAsync();
    }

    public async Task<Payout> GetPayoutByIdAsync(int id)
    {
        var payout = await _unitOfWork.PayoutRepository.GetByIdAsync(id);
        if (payout == null)
        {
            // Handle the case where the payout is not found
            throw new KeyNotFoundException($"Payout with ID {id} not found.");
        }
        return payout;
    }

    public async Task<Payout> CreatePayoutAsync(Payout payout)
    {

        await _unitOfWork.PayoutRepository.AddAsync(payout);
        return payout;
    }

    public async Task<bool> UpdatePayoutAsync(int id, Payout payout)
    {
        var existingPayout = await _unitOfWork.PayoutRepository.GetByIdAsync(id);
        // Handle the case where the payout does not exist
        if (existingPayout == null)
        {
            return false;
        }

        await _unitOfWork.PayoutRepository.UpdateAsync(payout);
        return true;
    }

    public async Task<bool> DeletePayoutAsync(int id)
    {
        var payout = await _unitOfWork.PayoutRepository.GetByIdAsync(id);
        // Handle the case where the payout does not exist
        if (payout == null)
        {
            return false;
        }

        await _unitOfWork.PayoutRepository.DeleteAsync(id);
        return true;
    }


    public async Task<TransactionResult> SendPaymentAsync(TransactionDetails transactionDetails)
    {
        var stubServerUrl = _configuration["StubServer:BaseUrl"];
        var response = await _httpClient.PostAsJsonAsync(stubServerUrl, transactionDetails);

        var stringResponse = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var jsonObject = JsonDocument.Parse(stringResponse).RootElement;
        var resultJson = jsonObject.GetProperty("Result").ToString();

        var result = JsonSerializer.Deserialize<TransactionResult>(resultJson, options);
        if (result.Success)
        {
            await CreatePayoutAsync(new Payout(transactionDetails.StudentID, transactionDetails.ProductID, transactionDetails.Date, transactionDetails.Amount));
        }
        else
        {
            throw new HttpRequestException("Error in accessing payment system");
        }
        return result;
    }
}
