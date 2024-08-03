using System.Threading.Tasks;

namespace NotificationMicroservice.Services
{
    public interface INotificationService
    {
        Task PostEmail(string userId, string message);
    }
}