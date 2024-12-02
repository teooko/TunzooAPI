using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Hubs
{
    public sealed class LobbyHub : Hub
    {
        public async Task JoinLobby(string lobbyId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, lobbyId);
            Console.WriteLine($"Connection {Context.ConnectionId} joined lobby {lobbyId}");
        }
        
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined");
        }
    }
}