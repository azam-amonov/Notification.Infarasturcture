using Sms.Notification.Application.Common.Enums;
using Sms.Notification.Application.Common.Notifications.Models;
using Sms.Notification.Application.Common.Notifications.Services;
using Sms.Notification.Domain.Common.Exceptions;
using Sms.Notification.Domain.Entities;
using Sms.Notification.Domain.Extensions;

namespace Sms.Notification.Infrastructure.Common.Notifications.Services;

public class NotificationAggregatorService : INotificationAggregatorService
{
    private readonly ISmsOrchestrationService _smsOrchestrationService;

    public NotificationAggregatorService(ISmsOrchestrationService smsOrchestrationService)
    {
        _smsOrchestrationService = smsOrchestrationService;
    }

    public async ValueTask<FuncResult<bool>> SendAsync(
                    NotificationRequest notificationRequest,
                    CancellationToken cancellationToken = default)
    {
        var test = async () =>
        {
            var senderUser = new User
            {
                            PhoneNumber = "+12565888785"
            };

            var receiverUser = new User
            {
                            PhoneNumber = "+998997771329"
            };

            var senderNotificationTask = notificationRequest.NotificationType switch
            {
                            NotificationType.Sms => SendSmsAsync(senderUser,
                                            receiverUser,
                                            notificationRequest.TemplateType,
                                            notificationRequest.Variables,
                                            cancellationToken),
                            _ => throw new NotImplementedException("Error from SendAsync in Infrastructure service")
            };

            var sendNotificationResult = await senderNotificationTask;
            return sendNotificationResult.Data;

        };

        return await test.GetValueAsync();
    }

    private async ValueTask<FuncResult<bool>> SendSmsAsync(
                    User senderUser,
                    User receiverUser,
                    NotificationTemplateType templateType,
                    Dictionary<string, string> variables,
                    CancellationToken cancellationToken = default
    )
    {
        return await _smsOrchestrationService.SendAsync(senderUser.PhoneNumber,
                        receiverUser.PhoneNumber,
                        templateType,
                        variables,
                        cancellationToken);
    }
}


