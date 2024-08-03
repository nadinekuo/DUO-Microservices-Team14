namespace NotificationMicroservice.Repository
{
    public interface INotificationRepository
    {
        Task SendEmail(string userId, string message);
    }
}