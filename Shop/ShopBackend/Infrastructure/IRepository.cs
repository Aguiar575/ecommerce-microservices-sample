using System.Linq.Expressions;

namespace ShopBackend.Infrastructure;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "");

    Task<TEntity?> GetByIDAsync(object id);
    Task Insert(TEntity entity);
    void Delete(object id);
    void Delete(TEntity entityToDelete);
    void Update(TEntity entityToUpdate);
    Task Save();
    Task SaveChangesWithIdentityInsertAsync();
}