namespace Torty.Web.Apps.ObsNightBotOverlay.BusinessModels.User;

public record UserBM
{
    /// <summary>
    /// MongoId that represents the User
    /// </summary>
    public string Id;

    /// <summary>
    /// The username the User uses to sign in
    /// </summary>
    public string Username;
}