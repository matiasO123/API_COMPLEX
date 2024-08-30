using Application.Interfaces;
using Application.wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.feature.Authenticate.Commands
{
    public  class RegisterCommand : IRequest<Response<string>>
    {
        public string name { get; set; }
        public string email { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
        public string origin {  get; set; } 


    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Response<string>>
    {

        private readonly IAccountServices _accountServices;

        public RegisterCommandHandler(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }

        public async Task<Response<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await _accountServices.RegisterAsync(new dto.RegisterRequest
            {
                email = request.email,
                userName = request.userName,
                password = request.password,
                confirmPassword = request.confirmPassword,
                name = request.name,
            }, request.origin);
        }
    }
}
