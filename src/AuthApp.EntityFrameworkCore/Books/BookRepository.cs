using AuthApp.Domian;
using AuthApp.Domian.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApp.EntityFrameworkCore.Repositories
{
    public class BookRepository : RepositoryBase<Book, Guid>, IBookRepository
    {
        public BookRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Book> GetBookAsync(Guid authorId, Guid bookId)
        {
            return await _dbContext.Set<Book>()
                .SingleOrDefaultAsync(book => book.AuthorId == authorId && book.Id == bookId);
        }

        public Task<IEnumerable<Book>> GetBooksAsync(Guid authorId)
        {
            return Task.FromResult(_dbContext.Set<Book>().Where(book => book.AuthorId == authorId).AsEnumerable());
        }

        public async Task<bool> IsExistAsync(Guid authorId, Guid bookId)
        {
            return await _dbContext.Set<Book>().
                AnyAsync(book => book.AuthorId == authorId && book.Id == bookId);
        }
    }
}
