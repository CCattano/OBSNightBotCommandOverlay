using MongoDB.Driver;
using Torty.Web.Apps.ObsNightBotOverlay.Domain.Entities;

namespace Torty.Web.Apps.ObsNightBotOverlay.Domain.Repos;

public interface IUsersRepo
{
    /// <summary>
    /// Create a new user for the username and password has provided
    /// </summary>
    /// <param name="username"></param>
    /// <param name="passwordHash"></param>
    /// <returns></returns>
    Task<User> Create(string username, string passwordHash);
    
    /// <summary>
    /// Fetch a user who has the username specified
    /// </summary>
    Task<User> GetByUsername(string username);
}

/// <inheritdoc cref="IUsersRepo"/>
public class UsersRepo(IDatabaseService dbSvc) : BaseRepository<User>(dbSvc), IUsersRepo
{
    protected override string CollectionName => "users.Users";
    
    //#region CREATE
    
    public async Task<User> Create(string username, string passwordHash)
    {
        User entity = new()
        {
            Username = username,
            PasswordHash = passwordHash
        };
        await base.Collection.InsertOneAsync(entity);
        return entity;
    }
    
    //#endregion
    
    //#region READ
    
    public async Task<User> GetByUsername(string username)
    {
        FilterDefinition<User> query =
            Builders<User>.Filter.Eq(nameof(User.Username), username)
            | Builders<User>.Filter.Eq(nameof(User.Username), username.ToLower())
            | Builders<User>.Filter.Eq(nameof(User.Username), username.ToUpper());
        
        IAsyncCursor<User> cursor = await base.Collection.FindAsync(query);
        User entity = await cursor.SingleOrDefaultAsync();
        
        return entity;
    }
    
    //#endregion
    
    //#region UPDATE
    //#endregion
    
    //#region DELETE
    //#endregion


}