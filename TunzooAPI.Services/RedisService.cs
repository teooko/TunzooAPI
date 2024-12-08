using StackExchange.Redis;
using TunzooApi.Domain.Entities;

namespace TunzooAPI.Services;

public interface IRedisService
{
    Lobby SetLobby();
    string GetLobby(string lobbyId);
}
public class RedisService : IRedisService
{
    private readonly ConnectionMultiplexer _redis;

    public RedisService(ConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public Lobby SetLobby()
    {
        var db = _redis.GetDatabase();
        var lobby = new Lobby();
        db.HashSet($"lobby:{lobby.LobbyId}:metadata", new HashEntry[] {new HashEntry("gameState", "waiting")});
        return lobby;
    }

    public string GetLobby(string lobbyId)
    { 
        var db = _redis.GetDatabase();
        return db.HashGet($"lobby:{lobbyId}:metadata", "gameState");
    }
}