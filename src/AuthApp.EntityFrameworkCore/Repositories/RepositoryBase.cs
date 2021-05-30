using AuthApp.Application.Common;
using AuthApp.Domian.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AuthApp.EntityFrameworkCore.Repositories
{
    public class RepositoryBase<T, TId> : IRepositoryBase<T, TId> where T : class
    {
        public DbContext _dbContext { get; set; }
        public RepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Create(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void Delelte(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(_dbContext.Set<T>().AsEnumerable());
        }

        public Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return Task.FromResult(_dbContext.Set<T>().Where(expression).AsEnumerable());
        }

        public async Task<bool> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public async Task<T> GetByIdAsync(TId id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<bool> IsExistAsync(TId id)
        {
            return await _dbContext.Set<T>().FindAsync(id) != null;
        }

        public  async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            //await GetByConditionAsync();
            var totalCount = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var list = new PagedList<T>(items, totalCount, pageNumber, pageSize);
            return await Task.FromResult(list);
        }
    }
}
