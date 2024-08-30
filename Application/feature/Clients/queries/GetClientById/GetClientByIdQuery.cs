using Application.dto;
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

namespace Application.feature.Clients.queries.GetClientById
{
    public class GetClientByIdQuery: IRequest<Response<ClientDTO>>
    {
        public int Id { get; set; }        
    }

    public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, Response<ClientDTO>>
    {
        private readonly IrepositoryAsync<Client> repository;
        private readonly IMapper mapper;

        public GetClientByIdQueryHandler(IrepositoryAsync<Client> irepositoryAsync, IMapper mapper)
        {
            this.repository = irepositoryAsync;
            this.mapper = mapper;
        }

        /*async Task<Response<ClientDTO>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var client = await repository.GetByIdAsync(request.Id);
            if (client == null) throw new KeyNotFoundException("Client not found!");
            else
            {
                var dto = mapper.Map<ClientDTO>(client);
                return new Response<ClientDTO>(dto);
            }
        }*/

        public async Task<Response<ClientDTO>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var client = await repository.GetByIdAsync(request.Id);
            if (client == null) throw new KeyNotFoundException("Client not found!");
            else
            {
                var dto = mapper.Map<ClientDTO>(client);
                return new Response<ClientDTO>(dto);
            }
        }
    }
}
