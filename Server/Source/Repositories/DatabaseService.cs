using MongoDB.Driver;

namespace Torty.Web.Apps.ObsNightBotOverlay.Domain;

public interface IDatabaseService
{
    IMongoCollection<T> GetCollection<T>(string collectionName);
}

public class DatabaseService(string connStr) : IDatabaseService
{
    private IMongoDatabase _db;
    private IMongoDatabase Database => _db ??= _InitializeDb();
    
    /// <summary>
    /// Fetch a collection from the database to perform operations against
    /// </summary>
    /// <param name="collectionName"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public IMongoCollection<T> GetCollection<T>(string collectionName) => Database.GetCollection<T>(collectionName);

    private IMongoDatabase _InitializeDb()
    {
        MongoClientSettings settings = MongoClientSettings.FromConnectionString(connStr);

        settings.ServerApi = new ServerApi(ServerApiVersion.V1);

        MongoClient client = new(settings);

        // This app only has access to the ONBO Database kept on the Database Server
        IMongoDatabase database = client.GetDatabase("ONBO");

        return database;
    }
}