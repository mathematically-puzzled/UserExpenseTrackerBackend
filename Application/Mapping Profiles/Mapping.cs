using Application.Models;
using AutoMapper;
using Domain;

namespace Application.Mapping_Profiles
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<NewUser, User>();
            CreateMap<UpdateUser, User>();
        }
    }
}
