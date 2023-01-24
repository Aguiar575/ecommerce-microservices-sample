using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;

namespace Shop.Backend.Api.Context;

public static class ShopContextHelper 
{
    public static async Task EnableIdentityInsert<T>(
        this DbContext context,
        bool IsDbAllowedToRunTransactions) => 
        await SetIdentityInsert<T>(context, enable: true, IsDbAllowedToRunTransactions);
    public static async Task DisableIdentityInsert<T>(
        this DbContext context,
        bool IsDbAllowedToRunTransactions) => 
        await SetIdentityInsert<T>(context, enable: false, IsDbAllowedToRunTransactions);

    public static async Task SaveChangesWithIdentityInsertAsync<T>(
        [NotNull] this DbContext context, 
        bool IsDbAllowedToRunTransactions = true)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        await using IDbContextTransaction transaction = await context.Database.BeginTransactionAsync();

        await context.EnableIdentityInsert<T>(IsDbAllowedToRunTransactions);
        await context.SaveChangesAsync();
        await context.DisableIdentityInsert<T>(IsDbAllowedToRunTransactions);
        await transaction.CommitAsync();
    }

    private static async Task SetIdentityInsert<T>(
        DbContext context,
        bool enable,
        bool IsDbAllowedToRunTransactions)
    {
        IEntityType? entityType = context.Model.FindEntityType(typeof(T));
        
        if (entityType == null) throw new ArgumentNullException(nameof(entityType));
        
        var value = enable ? "ON" : "OFF";

        if(IsDbAllowedToRunTransactions)
            await context.Database.ExecuteSqlRawAsync(
                $"SET IDENTITY_INSERT {entityType.GetSchema()}.{entityType.GetTableName()} {value}");
    }
}