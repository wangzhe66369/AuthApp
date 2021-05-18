using AuthApp.Domian;
using AuthApp.ServiceExtensions;
using AutoMapper;

namespace AuthApp.Authors.Dto
{
    public class AuthorMapProfile : Profile
    {
        public AuthorMapProfile()
        {
            CreateMap<Author, AuthorDto>()
                .ForMember(dest => dest.Age, config =>
                    config.MapFrom(src => src.BirthDate.GetCurrentAge()));
            CreateMap<AuthorForCreationDto, Author>();
        }
    }
}
