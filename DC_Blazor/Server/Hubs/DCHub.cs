using Microsoft.AspNetCore.SignalR;
using System.Net.Mail;
using System.Runtime.CompilerServices;

namespace DC_Blazor.Server.Hubs
{
    public class DCHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            UserHandler.ConnectedIds.Add(Context.ConnectionId);

            await Sendmsg($"User Joined, there are {UserHandler.ConnectedIds.Count} users here now!");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            UserHandler.ConnectedIds.Remove(Context.ConnectionId);
            await Sendmsg($"User left, there are {UserHandler.ConnectedIds.Count} users here now!");
            await base.OnDisconnectedAsync(exception);
        }
        
        public async Task Sendmsg(string msg)
        {
            await Clients.All.SendAsync("ReceiveMsg", msg);
        }
    }
}
