using System.Net;
using AutoMapper;
using Torty.Web.Apps.ObsNightBotOverlay.BusinessEntities;
using Torty.Web.Apps.ObsNightBotOverlay.Domain.Entities;
using Torty.Web.Apps.ObsNightBotOverlay.Domain.Repos;
using Torty.Web.Apps.ObsNightBotOverlay.Infrastructure;
using Torty.Web.Apps.ObsNightBotOverlay.Infrastructure.Exceptions;
using Torty.Web.Apps.ObsNightBotOverlay.Infrastructure.Utils;

namespace Torty.Web.Apps.ObsNightBotOverlay.BusinessLayers;

public interface IUsersBL
{
    /// <summary>
    /// Create a new user for the account credentials provided
    /// </summary>
    Task<UserBE> Create(string username, string password);
}

/// <inheritdoc cref="IUsersBL"/>
public class UsersBL(IUsersRepo repo, IMapper mapper) : BaseBL<IUsersRepo>(repo, mapper), IUsersBL
{
    public async Task<UserBE> Create(string username, string password)
    {
        User entity = await base.Repo.GetByUsername(username);
        
        if (entity is not null)
            throw new HttpException(HttpStatusCode.BadRequest, "Username unavailable");
        
        string pwShaVector = Environment.GetEnvironmentVariable(SystemConstants.EnvironmentVariables.ONBO_UPH)!;
        string passwordHash = EncryptionUtil.SHA256Hash(password, pwShaVector);
        entity = await base.Repo.Create(username, passwordHash);
        
        UserBE user = base.Mapper.Map<UserBE>(entity);
        return user;
    }
}