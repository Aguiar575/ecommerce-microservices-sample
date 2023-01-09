using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Context;

public class ShopContext : DbContext
{
    public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }

    public DbSet<ProductModel> Product { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductModel>().ToTable("Product");
    }
}