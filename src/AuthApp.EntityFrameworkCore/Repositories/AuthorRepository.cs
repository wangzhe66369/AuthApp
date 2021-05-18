using AuthApp.Domian;
using AuthApp.Domian.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace AuthApp.EntityFrameworkCore.Repositories
{
    public class AuthorRepository : RepositoryBase<Author, Guid>, IAuthorRepository
    {
        public AuthorRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
