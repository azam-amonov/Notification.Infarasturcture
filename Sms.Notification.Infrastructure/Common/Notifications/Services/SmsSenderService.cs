using Sms.Notification.Application.Common.Notifications.Brokers;
using Sms.Notification.Application.Common.Notifications.Services;

namespace Sms.Notification.Infrastructure.Common.Notifications.Services;

public class SmsSenderService : ISmsSenderService
{
    private readonly IEnumerable<ISmsSenderBroker> _smsSenderBrokers;

    public SmsSenderService(IEnumerable<ISmsSenderBroker> smsSendersBrokers)
    {
        _smsSenderBrokers = smsSendersBrokers;
    }

    public async ValueTask<bool> SendAsync(
        string senderPhoneNumber,
        string receiverPhoneNumber,
        string message,
        CancellationToken cancellationToken
    )
    {
        var result = false;

        foreach (var smsSenderService in _smsSenderBrokers)
        {
            try
            {
                result = await smsSenderService.SendAsync(senderPhoneNumber,
                                receiverPhoneNumber,
                                message,
                                cancellationToken);

                if (result) return result;
            }
            catch (Exception e)
            {
                // todo; log exception
            }
        }

        return result;
    }
}