using VCrisp.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace VCrisp.Infrastructure.Core
{
    /// <summary>
    /// 泛型仓储抽象基类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDbContext">EFContext实例</typeparam>
    public abstract class Repository<TEntity, TDbContext> : IRepository<TEntity> where TEntity : Entity, IAggregateRoot where TDbContext : EFContext
    {
        protected virtual TDbContext DbContext { get; set; }

        public Repository(TDbContext dbContext)
        {
            DbContext = dbContext;
        }

        /// <summary>
        /// 工作单元
        /// 因为 EFContext 实现了 IUnitOfWork，所以这里直接返回 EFContext 的实例即可
        /// </summary>
        public IUnitOfWork UnitOfWork => DbContext;

        public ITransaction Transaction => DbContext;

        //public EFContext EFContext => DbContext;

        public virtual TEntity Add(TEntity entity)
        {
            return DbContext.Add(entity).Entity;
        }
        public async virtual Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(Add(entity));
        }

        public virtual TEntity Update(TEntity entity)
        {
            return DbContext.Update(entity).Entity;
        }
        public async virtual Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(Update(entity));
        }

        public bool Remove(Entity entity)
        {
            DbContext.Remove(entity);
            return true;
        }
        public async Task<bool> RemoveAsync(Entity entity)
        {
            return await Task.FromResult(Remove(entity));
        }


        #region My 
        public async virtual Task<Task> AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(DbContext.AddRangeAsync(entities, cancellationToken));
        }

        public IQueryable<TEntity> GetAll() 
        {
            return DbContext.Set<TEntity>().AsQueryable();
        }
        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
            return await Task.FromResult(DbContext.Set<TEntity>().AsQueryable());
        }

        /// <summary>
        /// 功能描述:查询数据列表
        /// </summary>
        /// <param name="whereExpression">whereExpression</param>
        /// <returns>数据列表</returns>
        public Task<IEnumerable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression)
        {
            return Task.FromResult(DbContext.Set<TEntity>().Where(expression).AsEnumerable());
        }

        /// <summary>
        /// 功能描述:按照特定列查询数据列表
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IEnumerable<TResult> GetByCondition<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return DbContext.Set<TEntity>().Select(expression).AsQueryable();
        }
        public async Task<IEnumerable<TResult>> GetByConditionAsync<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return await DbContext.Set<TEntity>().Select(expression).ToListAsync();
        }

        #endregion My 

    }

    /// <summary>
    /// 泛型仓储抽象基类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">主键Id类型</typeparam>
    /// <typeparam name="TDbContext">EFContext实例</typeparam>
    public abstract class Repository<TEntity, TKey, TDbContext> : Repository<TEntity, TDbContext>, IRepository<TEntity, TKey>
                                                                  where TEntity : Entity<TKey>, IAggregateRoot 
                                                                  where TDbContext : EFContext
    {
        public Repository(TDbContext dbContext)
            : base(dbContext)
        {
        }

        public virtual bool Delete(TKey id)
        {
            var entity = DbContext.Find<TEntity>(id);
            if (entity == null)
            {
                return false;
            }
            DbContext.Remove(entity);
            return true;
        }
        public virtual async Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var entity = await DbContext.FindAsync<TEntity>(id, cancellationToken);
            if (entity == null)
            {
                return false;
            }
            DbContext.Remove(entity);
            return true;
        }
        public virtual TEntity Get(TKey id)
        {
            return DbContext.Find<TEntity>(id);
        }
        public virtual async Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return await DbContext.FindAsync<TEntity>(id, cancellationToken);
        }

        #region My

        /// <summary>
        /// 删除指定ID集合的数据(批量删除)
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        public async Task<bool> DeleteByIds(object[] ids,CancellationToken cancellationToken = default)
        {

            var entitys = await DbContext.FindAsync<TEntity>(ids, cancellationToken);
            if (entitys == null)
            {
                return false;
            }
            DbContext.RemoveRange(entitys);
            return true;
        }
        #endregion

    }
}
