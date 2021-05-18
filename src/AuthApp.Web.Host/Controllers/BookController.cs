using AuthApp.Books.Dto;
using AuthApp.Domian;
using AuthApp.Domian.IRepositories;
using AuthApp.Filters;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApp.Web.Host.Controllers
{
    [Route("api/authors/{authorId}/books")]
    [ApiController]
    //[ServiceFilter(typeof(CheckAuthorExistFilterAttribute))]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public BookController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            Mapper = mapper;
        }

        public IMapper Mapper { get; }

        [HttpPost()]
        public async Task<IActionResult> AddBookAsync(Guid authorId, BookForCreationDto bookForCreationDto)
        {
            var book = Mapper.Map<Book>(bookForCreationDto);

            book.AuthorId = authorId;
            _bookRepository.Create(book);
            if (!await _bookRepository.SaveAsync())
            {
                throw new Exception("创建资源Book失败");
            }

            var bookDto = Mapper.Map<BookDto>(book);
            return CreatedAtRoute(nameof(GetBookAsync), new { bookId = bookDto.Id }, bookDto);
        }

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteBookAsync(Guid authorId, Guid bookId)
        {
            var book = await _bookRepository.GetBookAsync(authorId, bookId);
            if (book == null)
            {
                return NotFound();
            }

            _bookRepository.Delelte(book);
            if (!await _bookRepository.SaveAsync())
            {
                throw new Exception("删除资源Book失败");
            }
            return NoContent();
        }

        [HttpGet("{bookId}", Name = nameof(GetBookAsync))]
        public async Task<ActionResult<BookDto>> GetBookAsync(Guid authorId, Guid bookId)
        {
            var book = await _bookRepository.GetBookAsync(authorId, bookId);
            if (book == null)
            {
                return NotFound();
            }

            var bookDto = Mapper.Map<BookDto>(book);

            return bookDto;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooksAsync(Guid authorId)
        {
            var books = await _bookRepository.GetBooksAsync(authorId);
            var bookDtoList = Mapper.Map<IEnumerable<BookDto>>(books);

            return bookDtoList.ToList();
        }

        [HttpPatch("{bookId}")]
        public async Task<IActionResult> ParticallyUpdateBookAsync(Guid authorId, Guid bookId, JsonPatchDocument<BookForUpdateDto> patchDocument)
        {
            var book = await _bookRepository.GetBookAsync(authorId, bookId);
            if (book == null)
            {
                return NotFound();
            }

            var bookUpdateDto = Mapper.Map<BookForUpdateDto>(book);
            patchDocument.ApplyTo(bookUpdateDto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map(bookUpdateDto, book, typeof(BookForUpdateDto), typeof(Book));

            _bookRepository.Update(book);
            if (!await _bookRepository.SaveAsync())
            {
                throw new Exception("更新资源Book失败");
            }
            return NoContent();
        }

        [HttpPut("{bookId}")]
        public async Task<IActionResult> UpdateBookAsync(Guid authorId, Guid bookId, BookForUpdateDto updatedBook)
        {
            var book = await _bookRepository.GetBookAsync(authorId, bookId);
            if (book == null)
            {
                return NotFound();
            }

            Mapper.Map(updatedBook, book, typeof(BookForUpdateDto), typeof(Book));
            _bookRepository.Update(book);
            if (!await _bookRepository.SaveAsync())
            {
                throw new Exception("更新资源Book失败");
            }
            return NoContent();
        }
    }
}