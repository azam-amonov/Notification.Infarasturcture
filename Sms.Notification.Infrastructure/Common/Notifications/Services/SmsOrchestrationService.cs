using System.Text;
using System.Text.RegularExpressions;
using Sms.Notification.Application.Common.Enums;
using Sms.Notification.Application.Common.Notifications.Services;
using Sms.Notification.Domain.Common.Exceptions;
using Sms.Notification.Domain.Extensions;

namespace Sms.Notification.Infrastructure.Common.Notifications.Services;

public class SmsOrchestrationService : ISmsOrchestrationService
{
    private readonly ISmsSenderService _smsSenderService;

    public SmsOrchestrationService(ISmsSenderService senderService)
    {
        _smsSenderService = senderService;
    }

    public async ValueTask<FuncResult<bool>> SendAsync(string senderPhoneNumber, 
                    string receiverPhoneNumber, 
                    NotificationTemplateType templateType,
                    Dictionary<string, string> variables, 
                    CancellationToken cancellationToken = default)
    {
        var test = async () =>
        {
            var template = GetTemplate(templateType);

            var message = GetMessage(template, variables);

            await _smsSenderService.SendAsync(senderPhoneNumber, receiverPhoneNumber, message, cancellationToken);
            
            return true;
        };

        return await test.GetValueAsync();
    }

    public string GetTemplate(NotificationTemplateType templateType)
    {
        var template = templateType switch
        {
                        NotificationTemplateType.SystemWelcomeNotification => "Welcome to the system, {{UserName}}",
                        NotificationTemplateType.EmailVerificationNotification =>
                                        "Verify Email by link {{VerificationLink}}",
                        _ => throw new ArgumentOutOfRangeException(nameof(templateType), "")
        };
        return template;
    }

    public string GetMessage(string template, Dictionary<string, string> variables)
    {
        // TODO: validate with message

        var messageBuilder = new StringBuilder();

        var pattern = @"\{\{([^\{\}]+)\}\}";
        var matchValuePattern = "{{(.*?)}}";
        var matches = Regex.Matches(template, pattern)
                        .Select(match =>
                        {
                            var placeholder = match.Value;
                            var placeholderValue = Regex.Match(placeholder, matchValuePattern).Groups[1].Value;
                            var valid = variables.TryGetValue(placeholderValue, out var value);
                            return new
                            {
                                            Placeholder = placeholder,
                                            Value = value, 
                                            IsValid = valid
                            };
                        });
        foreach (var match in matches)
            messageBuilder.Replace(match.Placeholder, match.Value);

        var message = messageBuilder.ToString();
        return message;
    }
}