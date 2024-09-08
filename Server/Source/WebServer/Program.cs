using Torty.Web.Apps.ObsNightBotOverlay.WebServer;

await Host
    .CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
    .Build()
    .RunAsync();