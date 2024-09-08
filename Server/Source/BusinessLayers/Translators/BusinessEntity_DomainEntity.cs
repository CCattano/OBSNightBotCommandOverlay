using AutoMapper;
using Torty.Web.Apps.ObsNightBotOverlay.BusinessEntities;
using Torty.Web.Apps.ObsNightBotOverlay.Domain.Entities;
using Torty.Web.Apps.ObsNightBotOverlay.Infrastructure.Extensions;

namespace Torty.Web.Apps.ObsNightBotOverlay.BusinessLayers.Translators;

public class BusinessEntity_DomainEntity : Profile
{
    public BusinessEntity_DomainEntity()
    {
        this.RegisterTranslator<UserBE, User, UserBE_User>();
    }
}