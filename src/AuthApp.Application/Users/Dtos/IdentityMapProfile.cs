using AuthApp.Domian;
using AuthApp.Identity.Users;
using AutoMapper;
namespace AuthApp.Users.Dto
{
    public class IdentityMapProfile : Profile
    {
        public IdentityMapProfile()
        {
            CreateMap<UserOutputDto, User>();
            CreateMap<UserInputDto, User>();
        }
    }
}
