using System.Net;
using Microsoft.OpenApi.Models;
using Torty.Web.Apps.ObsNightBotOverlay.BusinessLayers;
using Torty.Web.Apps.ObsNightBotOverlay.BusinessLayers.Translators;
using Torty.Web.Apps.ObsNightBotOverlay.Domain;
using Torty.Web.Apps.ObsNightBotOverlay.Domain.Repos;
using Torty.Web.Apps.ObsNightBotOverlay.Infrastructure;
using Torty.Web.Apps.ObsNightBotOverlay.WebServer.Controllers.Translators;
using Torty.Web.Apps.ObsNightBotOverlay.WebServer.Middleware;

namespace Torty.Web.Apps.ObsNightBotOverlay.WebServer;

using AppSettings = SystemConstants.AppSettings;

public class Startup(IWebHostEnvironment env)
{
    private readonly IConfiguration _configuration = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false)
        .AddEnvironmentVariables()
        .Build();

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        #region FRAMEWORK

        services.AddControllers();

        #endregion
        
        #region BUSINESS LAYERS
        
        services.AddScoped<IUsersBL, UsersBL>();
        
        #endregion

        #region DOMAIN
        
        services.AddScoped<IDatabaseService, DatabaseService>(_ =>
        {
            string connStr = _configuration.GetConnectionString(AppSettings.ConnStrings.MongoDb);
            return new DatabaseService(connStr);
        });
        services.AddScoped<IUsersRepo, UsersRepo>();

        #endregion
        
        #region EXTERNAL

        services.AddAutoMapper(
            typeof(BusinessModel_BusinessEntity),
            typeof(BusinessEntity_DomainEntity)
        );
        
        services.AddSwaggerGen(cfg =>
            cfg.SwaggerDoc("v1", new OpenApiInfo { Title = "ObsNightBotOverlay", Version = "v1" }));
        
        #endregion
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.MapWhen(
            // When request path is /status/isalive.
            path => path.Request.Path.Value == "/status/is-alive",
            builder => builder.Run(async context =>
            {
                const string response = "ObsNightBotOverlay Lambda is currently running.";
                Console.WriteLine(response); // WriteLine for AWS Lambda Application Log
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                // Return this message.
                await context.Response.WriteAsync(response);
            })
        );
        
        app.UseExceptionHandlerMiddleware();
        
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ObsNightBotOverlay v1"));
        }
        
        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}