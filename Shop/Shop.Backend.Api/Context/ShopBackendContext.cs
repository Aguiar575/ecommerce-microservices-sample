using Microsoft.EntityFrameworkCore;
using Shop.Backend.Api.Models;

namespace Shop.Backend.Api.Context;

public class ShopBackendContext : DbContext
{
    public ShopBackendContext(DbContextOptions<ShopBackendContext> options) : base(options) { }

    public DbSet<ProductModel> Product { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductModel>().ToTable("Product");
    }
}