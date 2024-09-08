using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Torty.Web.Apps.ObsNightBotOverlay.BusinessEntities;
using Torty.Web.Apps.ObsNightBotOverlay.BusinessLayers;
using Torty.Web.Apps.ObsNightBotOverlay.BusinessModels.User;
using Torty.Web.Apps.ObsNightBotOverlay.BusinessModels.User.Request;
using Torty.Web.Apps.ObsNightBotOverlay.Infrastructure.Exceptions;
using Torty.Web.Apps.ObsNightBotOverlay.Infrastructure.Managers;

namespace Torty.Web.Apps.ObsNightBotOverlay.WebServer.Controllers;

[Route("[controller]/[action]")]
public class UsersController(IUsersBL usersBL, IMapper mapper) : BaseController<IUsersBL>(usersBL, mapper) 
{
    /// <summary>
    /// Create a new user for the credentials provided
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="HttpException"></exception>
    [HttpPost]
    public async Task<ActionResult<UserBM>> Create([FromBody] CreateUserRequest request)
    {
        if (
            request is null
            || string.IsNullOrWhiteSpace(request.Username)
            || string.IsNullOrWhiteSpace(request.Password)
        )
        {
            throw new HttpException(HttpStatusCode.BadRequest);
        }

        UserBE entity = await base.BusinessLayer.Create(request.Username, request.Password);

        UserBM model = base.Mapper.Map<UserBM>(entity);

        _AddUserDataTokenCookie(model.Id);
        return model;
    }

    private void _AddUserDataTokenCookie(string userId)
    {
        // TODO: middleware to bump the expire time of the cookie so it stays active as long as it's used
        // TODO: move cookie management to a centralized location (static util?)
        string cookieValue = TokenManager.GenerateJwtForUser(userId);
        string cookieStr =
            $"{TokenManager.UserDataJwtCookieName}={cookieValue};max-age={TimeSpan.FromDays(7).TotalSeconds};path=/";
        HttpContext.Response.Headers.Append("set-cookie", cookieStr);
    }
}