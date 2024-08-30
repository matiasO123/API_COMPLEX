using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.wrapper;
using Application.Interfaces;
using Domain.Entities;
using AutoMapper;

namespace Application.feature.Clients.Commands.CreateClientCommand
{
    public class CreateClientCommand : IRequest<Response<int>>
    {
        public string name { get; set; }
        public DateTime birthDate { get; set; }
        public string phone_number { get; set; }
        public string email { get; set; }
        public string adress { get; set; }
        private int age;
      
        public CreateClientCommand(string name, DateTime birthDate, string phone_number, string email, string adress)
        {
            this.name = name;
            this.birthDate = birthDate;
            this.phone_number = phone_number;
            this.email = email;
            this.adress = adress;
            //this.age = age;
        }
    }

    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Response<int>>
    {
        private readonly IrepositoryAsync<Client> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateClientCommandHandler(IrepositoryAsync<Client> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var newReg = _mapper.Map<Client>(request);
            var data = await _repositoryAsync.AddAsync(newReg);
            return new Response<int>(data.id);
        }
    } 
}
