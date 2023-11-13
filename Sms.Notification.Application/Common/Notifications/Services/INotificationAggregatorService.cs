using Sms.Notification.Application.Common.Notifications.Models;
using Sms.Notification.Domain.Common.Exceptions;

namespace Sms.Notification.Application.Common.Notifications.Services;

public interface INotificationAggregatorService
{
    ValueTask<FuncResult<bool>> SendAsync(
        NotificationRequest notificationRequest,
        CancellationToken cancellationToken = default
        );
}