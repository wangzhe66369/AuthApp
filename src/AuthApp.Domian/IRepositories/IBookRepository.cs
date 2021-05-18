using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthApp.Domian.IRepositories
{
    public interface IBookRepository : IRepositoryBase<Book, Guid>
    {
        Task<bool> IsExistAsync(Guid authorId, Guid bookId);

        Task<Book> GetBookAsync(Guid authorId, Guid bookId);

        Task<IEnumerable<Book>> GetBooksAsync(Guid authorId);
    }
}
