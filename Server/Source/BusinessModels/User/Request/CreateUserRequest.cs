namespace Torty.Web.Apps.ObsNightBotOverlay.BusinessModels.User.Request;

public record CreateUserRequest
{
    /// <summary>
    /// The username the user will use to login
    /// </summary>
    public string Username;
    
    /// <summary>
    /// The plaintext password the user will use to login
    /// </summary>
    public string Password;
}