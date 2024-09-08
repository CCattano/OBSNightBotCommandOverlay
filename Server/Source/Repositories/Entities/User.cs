using MongoDB.Bson;

namespace Torty.Web.Apps.ObsNightBotOverlay.Domain.Entities;

public record User
{
    /// <summary>
    /// MongoId that represents the User
    /// </summary>
    public ObjectId Id;

    /// <summary>
    /// The username the User uses to sign in
    /// TODO: Needs a unq_idx in the Db
    /// </summary>
    public string Username;

    /// <summary>
    /// The one-way hashed password the User uses to sign in
    /// </summary>
    public string PasswordHash;
}