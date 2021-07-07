using AuthApp.Domian;
using AuthApp.Domian.Identity;
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
