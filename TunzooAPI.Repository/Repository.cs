namespace TunzooAPI.Repository;
using StackExchange.Redis;

public class Repository
{
    static readonly ConnectionMultiplexer _redis = ConnectionMultiplexer.Connect($"{HOST_NAME}:{PORT_NUMBER},password={PASSWORD}");

    public void PingDatabase()
    {
        var db = _redis.GetDatabase();
        db.Ping();
        db.StringGetSet("foo","bar");
        Console.WriteLine(db.StringGet("foo"));
    }
   
}