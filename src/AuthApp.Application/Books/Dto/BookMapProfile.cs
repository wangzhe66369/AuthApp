using AuthApp.Domian;
using AutoMapper;
namespace AuthApp.Books.Dto
{
    public class BookMapProfile : Profile
    {
        public BookMapProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<BookForCreationDto, Book>();
            CreateMap<BookForUpdateDto, Book>();
        }
    }
}
