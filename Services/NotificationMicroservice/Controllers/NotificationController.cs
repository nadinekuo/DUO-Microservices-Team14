using Microsoft.AspNetCore.Mvc;
using NotificationMicroservice.Services;

namespace NotificationMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        public async Task<IActionResult> SendNotification(string userId, string message)
        {
            await _notificationService.PostEmail(userId, message);
            return Ok($"Notification {message} sent to {userId} #PoC");
        }
    }
}