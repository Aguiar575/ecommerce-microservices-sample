using System.Data;
using Shop.Backend.Api.Context;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Shop.Backend.Api.Infrastructure;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    internal ShopBackendContext context;
    internal DbSet<TEntity> dbSet;
    public Repository(ShopBackendContext context)
    {
        this.context = context;
        dbSet = context.Set<TEntity>();
    }

    public virtual async Task<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>>? filter = null)
    {
        IQueryable<TEntity> query = dbSet;

        if (filter != null)
            query = query.Where(filter);
        
        return await query.ToListAsync();
    }

    public virtual async Task<TEntity?> GetByIDAsync(object id) => 
        await dbSet.FindAsync(id);

    public virtual async Task<TEntity?> Insert(TEntity entity) {
        var result = await dbSet.AddAsync(entity);
        return result.Entity;
    }

    public virtual void Delete(object id)
    {
        TEntity? entityToDelete = dbSet.Find(id);

        if(entityToDelete != null)
            Delete(entityToDelete); 
    }

    public virtual void Delete(TEntity entityToDelete)
    {
        if (context.Entry(entityToDelete).State == EntityState.Detached)
            dbSet.Attach(entityToDelete);

        dbSet.Remove(entityToDelete);
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        dbSet.Attach(entityToUpdate);
        context.Entry(entityToUpdate).State = EntityState.Modified;
    }

    public virtual async Task Save() => await context.SaveChangesAsync();
    
    public virtual async Task SaveChangesWithIdentityInsertAsync(bool IsDbAllowedToRunTransactions = true) => 
        await context.SaveChangesWithIdentityInsertAsync<TEntity>(IsDbAllowedToRunTransactions);
}