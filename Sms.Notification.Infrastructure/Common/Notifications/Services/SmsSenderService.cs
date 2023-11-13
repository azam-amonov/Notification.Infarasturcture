using Sms.Notification.Application.Common.Notifications.Brokers;

namespace Sms.Notification.Infrastructure.Common.Notifications.Services;

public class SmsSenderService : ISmsSenderBroker
{
    private readonly IEnumerable<ISmsSenderBroker> _smsSenderBrokers;

    public SmsSenderService(IEnumerable<ISmsSenderBroker> smsSenderBrokers)
    {
        _smsSenderBrokers = smsSenderBrokers;
    }

    public async ValueTask<bool> SendAsync(
        string senderPhoneNumber,
        string receiverPhoneNumber,
        string message,
        CancellationToken cancellationToken
    )
    {
        var result = false;

        foreach (var smsSenderBroker in _smsSenderBrokers)
        {
            try
            {
                result = await smsSenderBroker.SendAsync(senderPhoneNumber,
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