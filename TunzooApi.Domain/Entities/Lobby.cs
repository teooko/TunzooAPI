namespace TunzooApi.Domain.Entities;

public class Lobby
{
    public Guid LobbyId { get; private set; }
    
    public Lobby()
    {
        LobbyId = Guid.NewGuid();
    }
}