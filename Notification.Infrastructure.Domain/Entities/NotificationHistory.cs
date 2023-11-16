using Notification.Infrastructure.Domain.Common.Entities;

namespace Notification.Infrastructure.Domain.Entities;

public class NotificationHistory : IEntity
{
    public Guid Id { get; set; }
    
    public Guid SenderId { get; set;}
    
    public Guid ReceiverId { get; set; }
}