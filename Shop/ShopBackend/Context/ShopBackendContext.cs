using Microsoft.EntityFrameworkCore;
using ShopBackend.Models;

namespace ShopBackend.Context;

public class ShopBackendContext : DbContext
{
    public ShopBackendContext(DbContextOptions<ShopBackendContext> options) : base(options) { }

    public DbSet<ProductModel> Product { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductModel>().ToTable("Product");
    }
}