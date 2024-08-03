using NotificationMicroservice.Repository;
using System.Threading.Tasks;

namespace NotificationMicroservice.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task PostEmail(string userId, string message)
        {
            await _notificationRepository.SendEmail(userId, message);
        }
    }
}