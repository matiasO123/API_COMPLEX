using Application.Interfaces;
using Application.wrapper;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.feature.Clients.Commands.DeleteClientCommand
{
    public class DeleteClientCommand: IRequest<Response<int>>
    {
        public int id { get; set; }
        
        public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, Response<int>>
        {
            private readonly IrepositoryAsync<Client> _repositoryAsync;
            private readonly IMapper _mapper;

            public DeleteClientCommandHandler(IrepositoryAsync<Client> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<int>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
            {
                var existingClient = await _repositoryAsync.GetByIdAsync(request.id);

                if (existingClient == null)
                {
                    return new Response<int>(default, "Client not found");
                }
                await _repositoryAsync.DeleteAsync(existingClient);
                return new Response<int>(existingClient.id);
            }
        }
    }
}
