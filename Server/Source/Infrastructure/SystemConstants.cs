namespace Torty.Web.Apps.ObsNightBotOverlay.Infrastructure;

public struct SystemConstants
{
    /// <summary>
    /// The environment variables that are used in the application
    /// </summary>
    public struct EnvironmentVariables
    {
        public const string ONBO_SIV = "ONBO_SIV";
        public const string ONBO_USH = "ONBO_USH";
        public const string ONBO_UPH = "ONBO_UPH";
    }
    
    /// <summary>
    /// A struct structure that mirrors the layout of our appsettings.json files
    /// </summary>
    /// <remarks>
    /// This gives us a structured/centralized way to navigate our Configuration
    /// object without using hardcoded strings sporadically around the application 
    /// </remarks>
    public struct AppSettings
    {
        public struct ConnStrings
        {
            public const string MongoDb = "MongoDb";
        }

        public struct APIs
        {
            public struct NightBot
            {
            }
        }
    }
}