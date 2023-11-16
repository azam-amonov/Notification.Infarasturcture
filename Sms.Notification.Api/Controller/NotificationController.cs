using Microsoft.AspNetCore.Mvc;
using Sms.Notification.Application.Common.Notifications.Models;
using Sms.Notification.Application.Common.Notifications.Services;

namespace Sms.Notification.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly INotificationAggregatorService _notificationAggregatorService;

    public NotificationController(INotificationAggregatorService notificationAggregatorService)
    {
        _notificationAggregatorService = notificationAggregatorService;
    }

    [HttpPost]
    public async ValueTask<IActionResult> SendNotificationAsync([FromBody] NotificationRequest request)
    {
        var result = await _notificationAggregatorService.SendAsync(request);

        return result.IsSuccess ? Ok(result) : BadRequest(request);
    }
}