﻿using Application.dto;
using Application.wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAccountServices
    {
        Task<Response<AuthenticationResponse>> AuthenticationAsync(AuthenticationRequest request, string ipAdress);

        Task<Response<string>> RegisterAsync(RegisterRequest request, string origin);

    }
}
