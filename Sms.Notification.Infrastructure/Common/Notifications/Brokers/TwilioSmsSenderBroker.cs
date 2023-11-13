using Sms.Notification.Application.Common.Notifications.Brokers;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Sms.Notification.Infrastructure.Common.Notifications.Brokers;

public class TwilioSmsSenderBroker : ISmsSenderBroker
{
    public ValueTask<bool> SendAsync(
                    string senderPhoneNumber,
                    string receiverPhoneNumber,
                    string message,
                    CancellationToken cancellationToken
    )
    {
        var test = "AC761bac84b84cbc4d95a5796b865a93b6";
        var test2 = "9837a4dcade2f0d317fb03eb11a5853b";

        TwilioClient.Init(test, test2);
        var messageContent = MessageResource.Create(
                        body: message,
                        from: new Twilio.Types.PhoneNumber(senderPhoneNumber),
                        to: new Twilio.Types.PhoneNumber(receiverPhoneNumber)
        );
        return new ValueTask<bool>(true);
    }
}