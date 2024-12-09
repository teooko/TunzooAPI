using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Hubs
{
    public sealed class LobbyHub : Hub
    {
        public async Task JoinLobby(string lobbyId)
        {
            Console.WriteLine($"Connection to lobby attempted");
            
            await Groups.AddToGroupAsync(Context.ConnectionId, lobbyId);
            Console.WriteLine($"Connection {Context.ConnectionId} joined lobby {lobbyId}");
        }
        
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"{Context.ConnectionId} has joined");
            await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined");
        }
    }
}