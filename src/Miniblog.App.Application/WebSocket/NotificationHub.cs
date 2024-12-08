using Miniblog.App.Application.Abstractions;
using Microsoft.AspNetCore.SignalR;

namespace Miniblog.App.Application.WebSocket;

public sealed class NotificationHub : Hub<INotificationClient>
{
    public override async Task OnConnectedAsync()
    {
        await Clients.Client(Context.ConnectionId).ReceiveNotification("Conexão realizada com sucesso!");

        await base.OnConnectedAsync();
    }
}
