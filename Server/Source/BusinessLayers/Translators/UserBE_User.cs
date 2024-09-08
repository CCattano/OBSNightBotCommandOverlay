using AutoMapper;
using Torty.Web.Apps.ObsNightBotOverlay.BusinessEntities;
using Torty.Web.Apps.ObsNightBotOverlay.Domain.Entities;
using Torty.Web.Apps.ObsNightBotOverlay.Infrastructure.UtilityTypes;

namespace Torty.Web.Apps.ObsNightBotOverlay.BusinessLayers.Translators;

public class UserBE_User: BaseTranslator<UserBE, User>
{
    public override User Convert(UserBE source, User destination, ResolutionContext context)
    {
        throw new NotImplementedException();
    }

    public override UserBE Convert(User source, UserBE destination, ResolutionContext context)
    {
        UserBE result = new()
        {
            Id = source.Id.ToString(),
            Username = source.Username,
            PasswordHash = source.PasswordHash
        };
        return result;
    }
}