using Sms.Notification.Application.Common.Enums;
using Sms.Notification.Domain.Common.Exceptions;

namespace Sms.Notification.Application.Common.Notifications.Services;

public interface ISmsOrchestrationService
{
    ValueTask<FuncResult<bool>> SendAsync(
        string senderPhoneNumber,
        string receiverPhoneNumber,
        NotificationTemplateType templateType,
        Dictionary<string, string> variables,
        CancellationToken cancellationToken = default
    );
}