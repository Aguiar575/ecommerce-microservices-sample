using Shop.Models;
using Shop.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shop.Pages.Product
{
    public class IndexModel : PageModel
    {
        private readonly IShopBackendApiService _shopBackendApiService;

        public IndexModel(IShopBackendApiService shopBackendApiService) =>
            _shopBackendApiService = shopBackendApiService;

        public IList<ProductViewModel> Product { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Product = new List<ProductViewModel>(
                await _shopBackendApiService.GetProducts());
        }
    }
}
