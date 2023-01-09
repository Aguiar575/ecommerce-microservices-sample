using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ShopBackend.Context;

public static class ShopContextHelper 
{
    private static async Task EnableIdentityInsert<T>(this DbContext context) => 
        await SetIdentityInsert<T>(context, enable: true);
    private static async Task DisableIdentityInsert<T>(this DbContext context) => 
        await SetIdentityInsert<T>(context, enable: false);

    private static async Task SetIdentityInsert<T>(DbContext context, bool enable)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        IEntityType? entityType = context.Model.FindEntityType(typeof(T));
        
        if (entityType == null) throw new ArgumentNullException(nameof(entityType));
        
        var value = enable ? "ON" : "OFF";

        await context.Database.ExecuteSqlRawAsync(
            $"SET IDENTITY_INSERT {entityType.GetSchema()}.{entityType.GetTableName()} {value}");
    }

    public static async Task SaveChangesWithIdentityInsertAsync<T>([NotNull] this DbContext context)
    {
        await using var transaction = await context.Database.BeginTransactionAsync();
        if (context == null) throw new ArgumentNullException(nameof(context));
        await context.EnableIdentityInsert<T>();
        await context.SaveChangesAsync();
        await context.DisableIdentityInsert<T>();
        await transaction.CommitAsync();
    }
}