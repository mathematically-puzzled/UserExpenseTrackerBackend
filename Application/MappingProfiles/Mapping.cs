using Application.Models.ExpenseCategories;
using Application.Models.Users;
using AutoMapper;
using Domain;

namespace Application.MappingProfiles
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<NewUser, User>();
            CreateMap<UpdateUser, User>();
            CreateMap<User, UserDto>();
            CreateMap<ForgotUser, User>();

            CreateMap<ExpenseCategory, UserExpenseCategory>();
            CreateMap<AddExpenseCategory, ExpenseCategory>();
        }
    }
}
