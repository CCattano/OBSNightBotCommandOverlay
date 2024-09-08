using AutoMapper;

namespace Torty.Web.Apps.ObsNightBotOverlay.BusinessLayers;

public class BaseBL<TRepo>
{
    /// <summary>
    /// The default Repository associated with the Business Layer
    /// </summary>
    protected TRepo Repo { get; }

    /// <summary>
    /// AutoMapper instance
    /// </summary>
    protected IMapper Mapper { get; }
    
    protected BaseBL(TRepo repo, IMapper mapper)
    {
        Repo = repo;
        Mapper = mapper;
    }
}