using Application.Interfaces;
using Application.wrapper;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.feature.Clients.Commands.UpdateClientCommand
{
    public class UpdateClientCommand : IRequest<Response<int>>
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime birthDate { get; set; }
        public string phone_number { get; set; }
        public string email { get; set; }
        public string adress { get; set; }
        private int age;

        /*public UpdateClientCommand(int id, string name, DateTime birthDate, string phone_number, string email, string adress)
        {
            this.id = id;
            this.name = name;
            this.birthDate = birthDate;
            this.phone_number = phone_number;
            this.email = email;
            this.adress = adress;
            //this.age = age;
        }*/
    }

    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Response<int>>
    {
        private readonly IrepositoryAsync<Client> _repositoryAsync;
        private readonly IMapper _mapper;

        public UpdateClientCommandHandler(IrepositoryAsync<Client> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var existingClient = await _repositoryAsync.GetByIdAsync(request.id);

            if (existingClient == null)
            {
                return new Response<int>(default, "Client not found");
            }

            existingClient.birthDate = request.birthDate;
            existingClient.phone_number = request.phone_number;
            existingClient.email = request.email;   
            existingClient.adress = request.adress;
            existingClient.name = request.name;
            await _repositoryAsync.UpdateAsync(existingClient);
            return new Response<int>(existingClient.id);
        }
    }
}
