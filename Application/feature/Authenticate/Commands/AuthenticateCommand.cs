using Application.dto;
using Application.Interfaces;
using Application.wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.feature.Authenticate.Commands
{
    public class AuthenticateCommand :IRequest<Response<AuthenticationResponse>>
    {
        public string email { get; set; }

        public string password { get; set; }

        public string ipAdress { get; set; }
    }

    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, Response<AuthenticationResponse>>
    {
        private readonly IAccountServices _accountServices;

        public AuthenticateCommandHandler(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }


        public async Task<Response<AuthenticationResponse>> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            return await _accountServices.AuthenticationAsync(new AuthenticationRequest
            {
                email = request.email,
                password = request.password
            }, request.ipAdress);
        }
    }

}
