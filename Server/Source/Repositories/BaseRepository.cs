using MongoDB.Driver;

namespace Torty.Web.Apps.ObsNightBotOverlay.Domain;

public abstract class BaseRepository<TModel>(IDatabaseService dbSvc)
{
    /// <summary>
    /// The name of the Collection in the Db this Repository represents
    /// </summary>
    protected abstract string CollectionName { get; }
    
    private IMongoCollection<TModel> _collection;
    
    /// <summary>
    /// Instance of the Collection this Repository represents
    /// </summary>
    protected  IMongoCollection<TModel> Collection => _collection ??= dbSvc.GetCollection<TModel>(CollectionName);
}
