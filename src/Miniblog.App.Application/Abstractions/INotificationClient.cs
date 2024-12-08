namespace Miniblog.App.Application.Abstractions;

public interface INotificationClient
{
    Task ReceiveNotification(string message);
}
