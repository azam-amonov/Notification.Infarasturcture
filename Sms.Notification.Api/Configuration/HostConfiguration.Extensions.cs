using Sms.Notification.Application.Common.Notifications.Brokers;
using Sms.Notification.Application.Common.Notifications.Services;
using Sms.Notification.Infrastructure.Common.Notifications.Brokers;
using Sms.Notification.Infrastructure.Common.Notifications.Services;

namespace Sms.Notification.Api.Configuration;

public static partial class HostConfiguration
{
    private static WebApplicationBuilder AddNotificationInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ISmsSenderBroker, TwilioSmsSenderBroker>();

        builder.Services.AddScoped<ISmsSenderBroker, SmsSenderService>();

        builder.Services
            .AddScoped<ISmsOrchestrationService, SmsOrchestrationService>()
            .AddScoped<INotificationAggregatorService, NotificationAggregatorService>();

        return builder;
    }

    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers();
        
        return builder;
    }

    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();
        
        return app;
    }
}