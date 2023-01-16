using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Shop.Backend.Api.Context;
using Shop.Backend.Api.Infrastructure;
using Shop.Backend.Api.Models;

namespace Shop.Backend.Tests.RepositoryTests;

public class RepositoryTests
{
    private static ulong DefaultId = 1;
    private readonly DbContextOptions<ShopBackendContext> _options;

    public RepositoryTests()
    {
        _options = new DbContextOptionsBuilder<ShopBackendContext>()
                .UseInMemoryDatabase(databaseName: "TestingDb")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
    }

    [Fact]
    public async void SaveChangesWithIdentityInsertAsync_Should_Save_ProductModel_With_Explicit_Id()
    {
        using (var context = new ShopBackendContext(_options))
        {
            CleanDatabase(context);
            context.Database.EnsureDeleted();
            var sut = new Repository<ProductModel>(context);

            ulong explicitId = 2947301839;
            var entity = new ProductModel(
                explicitId,
                23,
                "Some item");

            await sut.Insert(entity);
            await sut.SaveChangesWithIdentityInsertAsync(IsDbAllowedToRunTransactions: false);

            Assert.Equal((int)DefaultId, context.Product.Count());
            Assert.True(context.Product.Any(e => e.Id == explicitId));
        }
    }

    [Fact]
    public async void Get_Should_Return_ProductModel_Filtered_By_Id()
    {
        using (var context = new ShopBackendContext(_options))
        {
            CleanDatabase(context);           
            var sut = new Repository<ProductModel>(context);

            var entity = new ProductModel(
                DefaultId,
                23,
                "Some Item");

            await sut.Insert(entity);
            await sut.Save();

            IEnumerable<ProductModel> result = await sut.Get(e => e.Id == DefaultId);

            Assert.Equal((int)DefaultId, result.Count());
            Assert.True(result.Any(e => e.Id == DefaultId));
        }
    }

    [Fact]
    public async void GetByIDAsync_Should_Return_ProductModel_Filtered_By_Id()
    {
        using (var context = new ShopBackendContext(_options))
        {
            CleanDatabase(context);           
            var sut = new Repository<ProductModel>(context);

            var entity = new ProductModel(
                DefaultId,
                23,
                "Some Item");
            await sut.Insert(entity);
            await sut.Save();

            ProductModel? result = await sut.GetByIDAsync(DefaultId);

            Assert.Equal(DefaultId, result?.Id);
            Assert.Equal("Some Item", result?.Name);

        }
    }

    [Fact]
    public async void GetByIDAsync_Should_Return_Null_If_Not_Find_Any_Matches()
    {
        using (var context = new ShopBackendContext(_options))
        {
            CleanDatabase(context);           
            var sut = new Repository<ProductModel>(context);

            ProductModel? result = await sut.GetByIDAsync(DefaultId);

            Assert.Null(result);
        }
    }

    [Fact]
    public async void GetByIDAsync_Should_Not_Found_Any_ProductModel()
    {
        using (var context = new ShopBackendContext(_options))
        {
            CleanDatabase(context);           
            var sut = new Repository<ProductModel>(context);

            ProductModel? result = await sut.GetByIDAsync(DefaultId);

            Assert.Null(result);
        }
    }

    [Fact]
    public async void Delete_Should_Remove_ProductModel_By_Entity()
    {
        using (var context = new ShopBackendContext(_options))
        {
            CleanDatabase(context);           
            var sut = new Repository<ProductModel>(context);

            var entity = new ProductModel(
                DefaultId,
                23,
                "Some Item");

            await sut.Insert(entity);
            await sut.Save();

            ProductModel? result = await sut.GetByIDAsync(DefaultId);
            sut.Delete(result);
            await sut.Save();

            Assert.Equal(0, context.Product.Count());
        }
    }

    [Fact]
    public async void Update_Should_alter_ProductModel_Name()
    {
        using (var context = new ShopBackendContext(_options))
        {
            CleanDatabase(context);           
            var sut = new Repository<ProductModel>(context);

            var entity = new ProductModel(
                DefaultId,
                23,
                "Some Item");

            await sut.Insert(entity);
            await sut.Save();

            ProductModel? result = await sut.GetByIDAsync(DefaultId);
            result.Name = "Another Name";

            sut.Update(result);
            await sut.Save();

            ProductModel? searchAgain = await sut.GetByIDAsync(DefaultId);           
            Assert.Equal("Another Name", searchAgain.Name);
        }
    }
    private void CleanDatabase(ShopBackendContext context) => 
        context.Database.EnsureDeleted();
}