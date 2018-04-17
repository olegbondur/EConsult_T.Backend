using AutoMapper;
using EConsult_T.Api.Models;
using EConsult_T.DAL.Entities;

namespace EConsult_T.Api.Services
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegistrationUserCredentials, UserRegistrationDto>();
            CreateMap<UserRegistrationDto, User>();
        }
    }
}
