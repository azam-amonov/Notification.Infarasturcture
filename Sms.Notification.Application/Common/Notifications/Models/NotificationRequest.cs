using Sms.Notification.Application.Common.Enums;

namespace Sms.Notification.Application.Common.Notifications.Models;

public class NotificationRequest
{
    public Guid ReceivedId {get; set; }
    
    public NotificationTemplateType TemplateType {get; set; }
    
    public Dictionary<string, string> Variables {get; set; }
    
    public NotificationType NotificationType {get; set; }

    public Guid? SenderId { get; set; } = null;
}