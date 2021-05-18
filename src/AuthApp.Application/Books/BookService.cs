using AuthApp.Domian.IRepositories;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApp.Books
{
    public class BookService : IBookService
    {

        private readonly IBookRepository _teacherRepository;

        public BookService(IBookRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        //
        
    }
}
