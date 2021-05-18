using AuthApp.Domian.IRepositories;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApp.Authors
{
    public class AuthorService : IAuthorService
    {

        private readonly IAuthorRepository _teacherRepository;

        public AuthorService(IAuthorRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }
        //

       
    }
}
