using Messaging.Domain;
using Messaging.Domain.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messaging.API.Hubs
{
    public class MessagesHub : Hub<IMessageClient>
    {
        private string[] units = new string[] { "UnitA", "UnitB" };

        public async override Task OnConnectedAsync()
        {
            Random random = new Random();

            await this.Groups.AddToGroupAsync(Context.ConnectionId, units[random.Next() % 2]);

            base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(Message message)
        {
            // await this.Clients.All.SendAsync("YouHaveGotMessage", message);

            // Strong Typed Hub
            await this.Clients.Others.YouHaveGotMessage(message);

            // specified client
           // await this.Clients.Client(connectionId).YouHaveGotMessage();
        }

        public async Task SendMessageToUnit(Message message, string unit)
        {
            // await this.Clients.Groups(unit).SendAsync("YouHaveGotMessage", message);

            await this.Clients.Groups(unit).YouHaveGotMessage(message);
        }

        public async Task Ping(string message = "Pong")
        {
            await Clients.Caller.Pong(message);
        }

        // Stream
        // https://docs.microsoft.com/pl-pl/aspnet/core/signalr/streaming?view=aspnetcore-3.1
    }
}
