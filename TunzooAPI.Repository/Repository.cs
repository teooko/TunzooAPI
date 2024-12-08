namespace TunzooAPI.Repository;
using StackExchange.Redis;
using TunzooAPI.Constants;
public class Repository
{
    static readonly ConnectionMultiplexer _redis = ConnectionMultiplexer.Connect($"{DbConstants.HostName}:{DbConstants.PortNumber},password={DbConstants.Password}");

    public void PingDatabase()
    {
        var db = _redis.GetDatabase();
        db.Ping();
        db.StringGetSet("foo","bar");
        Console.WriteLine(db.StringGet("foo"));
    }
   
}