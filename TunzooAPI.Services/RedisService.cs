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
        try
        {
            var db = _redis.GetDatabase();
            var result = db.HashGet($"lobby:{lobbyId}:metadata", "gameState");
        
            if (result.IsNullOrEmpty)
            {
                throw new KeyNotFoundException($"Lobby with ID '{lobbyId}' does not exist or 'gameState' is missing.");
            }

            return result;
        }
        catch (RedisException ex) 
        {
            Console.WriteLine($"Redis error occurred: {ex.Message}");
            return "Error: Unable to fetch game state.";
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
            return "Error: Lobby or gameState not found.";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            return "Error: An unexpected issue occurred.";
        }
    }
}