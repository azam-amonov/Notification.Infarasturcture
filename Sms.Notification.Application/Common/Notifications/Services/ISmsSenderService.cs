using Sms.Notification.Domain.Common.Exceptions;

namespace Sms.Notification.Application.Common.Notifications.Services;

public interface ISmsSenderService
{
    ValueTask<FuncResult<bool>> SendAsync(
        string senderPhoneNumber,
        string receiverPhoneNumber,
        string message,
        CancellationToken cancellationToken
    );
}