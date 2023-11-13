namespace Sms.Notification.Domain.Entities;

public class User
{
    public Guid Id { get; set; }

    public string UserName { get; set; }
    public string PhoneNumber { get; set; }

    public string EmailAddress { get; set; }
}