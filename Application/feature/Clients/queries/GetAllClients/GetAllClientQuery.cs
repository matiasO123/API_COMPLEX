using Application.dto;
using Application.Interfaces;
using Application.Specifications;
using Application.wrapper;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace Application.feature.Clients.queries.GetAllClients
{
    public class GetAllClientQuery : IRequest<PageResponse<List<ClientDTO>>>
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }  
        public string name { get; set; }


        public class GetAllClientQueryHandler : IRequestHandler<GetAllClientQuery, PageResponse<List<ClientDTO>>>
        {

            private readonly IrepositoryAsync<Client> _repositoryAsync;
            private readonly IDistributedCache _distributedCache;
            private readonly IMapper _mapper;

            public GetAllClientQueryHandler(IrepositoryAsync<Client> irepository, IMapper mapper, IDistributedCache distributedCache)
            {
                _repositoryAsync = irepository;
                _mapper = mapper;
                _distributedCache = distributedCache;
            }

            public async Task<PageResponse<List<ClientDTO>>> Handle(GetAllClientQuery request, CancellationToken cancellationToken)
            {
                var cachingKey = $"list_of_clients_{request.pageSize}_{request.pageNumber}_{request.name}";
                string serialize_clients_list;
                var redis_clients_list = await _distributedCache.GetAsync(cachingKey);
                var clients_list = new List<Client>();
                if (redis_clients_list != null)
                {
                    serialize_clients_list = Encoding.UTF8.GetString(redis_clients_list);
                    clients_list = JsonConvert.DeserializeObject<List<Client>>(serialize_clients_list);
                }
                else
                {
                    clients_list = await _repositoryAsync.ListAsync(new PageClientSpecification(request.pageSize, request.pageNumber, request.name));
                    serialize_clients_list = JsonConvert.SerializeObject(clients_list);
                    redis_clients_list = Encoding.UTF8.GetBytes(serialize_clients_list);

                    var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMinutes(10));
                    await _distributedCache.SetAsync(cachingKey,redis_clients_list,options);
                }




                var clientsDto = _mapper.Map<List<ClientDTO>>(clients_list);

                return new PageResponse<List<ClientDTO>>(clientsDto, request.pageNumber, request.pageSize);
            }
        }
    }

    
}
