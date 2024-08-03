using System.Threading.Tasks;

namespace NotificationMicroservice.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        public async Task SendEmail(string userId, string message)
        {
            //Method not implemented for PoC (yet)
            await Task.Delay(100); // Placeholder
        }
    }
}