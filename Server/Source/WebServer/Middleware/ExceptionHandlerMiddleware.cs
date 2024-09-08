using System.Text;
using Torty.Web.Apps.ObsNightBotOverlay.Infrastructure.Exceptions;

namespace Torty.Web.Apps.ObsNightBotOverlay.WebServer.Middleware
{
    /// <summary>
    /// Request middleware will wrap the request in a try/catch and look for an exception that is an
    /// instance of <see cref="HttpException"/> and parse and write its contents to the Response Body.
    /// </summary>
    /// <param name="next"></param>
    public class ExceptionHandlerMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext httpCtx)
        {
            try
            {
                await next(httpCtx);
            }
            catch (HttpException httpEx)
            {
                httpCtx.Response.ContentType = "text/plain";
                httpCtx.Response.StatusCode = (int)httpEx.ResponseCode;
                await httpCtx.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(httpEx.Message));
            }
        }
    }

    public static class ExceptionHandlerMiddlewareExtensions
    {
        /// <summary>
        /// Register an instance of the ExceptionHandlerMiddleware class with request-hook pipeline
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
