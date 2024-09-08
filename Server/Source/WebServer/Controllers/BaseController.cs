using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Torty.Web.Apps.ObsNightBotOverlay.WebServer.Controllers;

public class BaseController<TBusinessLayer> : Controller
{
    /// <summary>
    /// The Business Layer associated with the vertical stack the Controller represents.
    /// </summary>
    protected TBusinessLayer BusinessLayer { get; }
    
    /// <summary>
    /// Instance of AutoMapper for translating models.
    /// </summary>
    protected IMapper Mapper { get; }

    protected BaseController(TBusinessLayer businessLayer, IMapper mapper)
    {
        BusinessLayer = businessLayer;
        Mapper = mapper;
    }
}