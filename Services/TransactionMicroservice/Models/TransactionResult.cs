using System.Text.Json.Serialization;
namespace TransactionMicroservice.Models
{
public class TransactionResult
{
    public bool Success { get; set; }

    public string Message { get; set; }

    public string TransactionId { get; set; }
}

}
