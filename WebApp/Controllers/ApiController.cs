using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    /// <summary>
    /// Marker class for all api controllers in project
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController: ControllerBase
    {

    }
}
