using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private IMediator mediator;
        protected IMediator Mediator => mediator??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
