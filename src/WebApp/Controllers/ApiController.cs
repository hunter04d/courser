using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Filters;

namespace WebApp.Controllers
{
    /// <summary>
    /// Marker class for all api controllers in project
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [ApiExceptionFilter]
    public abstract class ApiController : ControllerBase
    {
        private IMediator _mediator = null!;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
