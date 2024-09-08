using AutoMapper;
using Torty.Web.Apps.ObsNightBotOverlay.BusinessEntities;
using Torty.Web.Apps.ObsNightBotOverlay.BusinessModels.User;
using Torty.Web.Apps.ObsNightBotOverlay.Infrastructure.Extensions;

namespace Torty.Web.Apps.ObsNightBotOverlay.WebServer.Controllers.Translators;

public class BusinessModel_BusinessEntity : Profile
{
    public BusinessModel_BusinessEntity()
    {
        this.RegisterTranslator<UserBM, UserBE, UserBM_UserBE>();
    }
}