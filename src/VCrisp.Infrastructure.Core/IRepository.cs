using VCrisp.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace VCrisp.Infrastructure.Core
{
    /// <summary>
    /// 泛型仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
        
        TEntity Add(TEntity entity); 
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        TEntity Update(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        // 当前接口未指定主键类型，所以这里需要根据实体对象去删除
        bool Remove(Entity entity);
        Task<bool> RemoveAsync(Entity entity);

        #region My
        ITransaction Transaction { get; }
        Task<Task> AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken = default);

        IQueryable<TEntity> GetAll() ;
        Task<IQueryable<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression);
        IEnumerable<TResult> GetByCondition<TResult>(Expression<Func<TEntity, TResult>> expression);
        Task<IEnumerable<TResult>> GetByConditionAsync<TResult>(Expression<Func<TEntity, TResult>> expression);
        //Task<int> AddReturnIdentityAsync(TEntity entity, CancellationToken cancellationToken = default);
        #endregion

    }

    /// <summary>
    /// 泛型仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">主键Id类型</typeparam>
    public interface IRepository<TEntity, TKey> : IRepository<TEntity> where TEntity : Entity<TKey>, IAggregateRoot
    {
        bool Delete(TKey id);
        Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken = default);
        TEntity Get(TKey id);
        Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken = default);

        #region
        Task<bool> DeleteByIds(object[] ids, CancellationToken cancellationToken = default);
        #endregion
    }

}
