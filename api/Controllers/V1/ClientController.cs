using Application.feature.Clients.Commands.CreateClientCommand;
using Application.feature.Clients.Commands.DeleteClientCommand;
using Application.feature.Clients.Commands.UpdateClientCommand;
using Application.feature.Clients.queries.GetAllClients;
using Application.feature.Clients.queries.GetClientById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.V1
{
    //[Route("api/[controller]")]
    //[ApiController]
    [ApiVersion("1.0")]
    public class ClientController : BaseApiController
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreateClientCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put (int id, UpdateClientCommand command)
        {
            if (id != command.id) return BadRequest();
            return Ok(await Mediator.Send(command));
        }

        [Authorize(Roles = "Admin")]  
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (int id)
        {
            return Ok(await Mediator.Send(new DeleteClientCommand { id = id }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetClientByIdQuery { Id = id }));
        }

        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery]GetAllClientParameters filter)
        {
            return Ok(await Mediator.Send(new GetAllClientQuery {pageNumber = filter.PageNumber, pageSize = filter.PageSize, name = filter.name}));
        }
    }
}
