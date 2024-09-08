namespace Torty.Web.Apps.ObsNightBotOverlay.BusinessEntities;

public record UserBE
{
    /// <summary>
    /// MongoId that represents the User
    /// </summary>
    public string Id;

    /// <summary>
    /// The username the User uses to sign in
    /// </summary>
    public string Username;

    /// <summary>
    /// The one-way hashed password the User uses to sign in
    /// </summary>
    public string PasswordHash;
}