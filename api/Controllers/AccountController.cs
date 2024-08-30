using api.Controllers.V1;
using Application.dto;
using Application.feature.Authenticate.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        [HttpPost("Authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await Mediator.Send(new AuthenticateCommand
            {
                email = request.email,
                password = request.password,
                ipAdress = GenerateIpAdress()
            }));
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            return Ok(await Mediator.Send(new RegisterCommand
            {
                email = request.email,
                password = request.password,
                origin = Request.Headers["origin"],
                name = request.name,
                confirmPassword = request.confirmPassword,
                userName = request.userName
            }));
        }


        private string GenerateIpAdress()
        {
            if (Request.Headers.ContainsKey("X-Forwared-For"))
                return Request.Headers["X-forwarded-For"];
            else 
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

    }
}
