using System.Linq.Expressions;

namespace Shop.Backend.Api.Infrastructure;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>>? filter = null);
    Task<TEntity?> GetByIDAsync(object id);
    Task Insert(TEntity entity);
    void Delete(object id);
    void Delete(TEntity entityToDelete);
    void Update(TEntity entityToUpdate);
    Task Save();
    Task SaveChangesWithIdentityInsertAsync(bool IsDbAllowedToRunTransactions);
}