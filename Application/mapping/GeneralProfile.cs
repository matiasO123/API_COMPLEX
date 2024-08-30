using Application.dto;
using Application.feature.Clients.Commands.CreateClientCommand;
using AutoMapper;
using Domain.Entities;

namespace Application.mapping
{
    public class GeneralProfile:Profile
    {
        public GeneralProfile()
        {
            #region Commands
            CreateMap<CreateClientCommand, Client>();
            #endregion

            #region
            CreateMap<Client, ClientDTO>();
            #endregion
        }
    }
}
