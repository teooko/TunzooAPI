namespace TunzooApi.Domain.Entities;

public class Lobby
{
    public string LobbyId { get; private set; }
    
    public Lobby()
    {
        LobbyId = Guid.NewGuid().ToString("N").Substring(0, 6); 
    }
}