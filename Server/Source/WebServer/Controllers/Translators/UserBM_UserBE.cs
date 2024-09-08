using AutoMapper;
using Torty.Web.Apps.ObsNightBotOverlay.BusinessEntities;
using Torty.Web.Apps.ObsNightBotOverlay.BusinessModels.User;
using Torty.Web.Apps.ObsNightBotOverlay.Infrastructure.UtilityTypes;

namespace Torty.Web.Apps.ObsNightBotOverlay.WebServer.Controllers.Translators;

public class UserBM_UserBE : BaseTranslator<UserBM, UserBE>
{
    public override UserBE Convert(UserBM source, UserBE destination, ResolutionContext context)
    {
        throw new NotImplementedException();
    }
    
    public override UserBM Convert(UserBE source, UserBM destination, ResolutionContext context)
    {
        UserBM result = new()
        {
            Id = source.Id,
            Username = source.Username
        };
        return result;
    }
}