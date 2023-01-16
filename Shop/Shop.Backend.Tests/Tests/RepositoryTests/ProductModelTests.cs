using Shop.Backend.Api.Models;

namespace Shop.Backend.Tests.RepositoryTests;

public class ProductModelTests
{
    [Fact]
    public void ProductModelTests_Should_Throw_InvalidOperationException_On_Changing_Id_Not_Null()
    {
        var product = new ProductModel(09090909, 300, "Product name");
        Assert.Throws<InvalidOperationException>(() => product.Id = 122332);
    }
}