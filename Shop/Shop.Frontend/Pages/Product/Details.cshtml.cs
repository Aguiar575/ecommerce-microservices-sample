using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Models;
using Shop.Services;

namespace Shop.Pages.Product
{
    public class DetailsModel : PageModel
    {
        private readonly IShopBackendApiService _shopBackendApiService;

        public DetailsModel(IShopBackendApiService shopBackendApiService) =>
            _shopBackendApiService = shopBackendApiService;

      public ProductViewModel Product { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(ulong? id)
        {
            ProductViewModel? product = 
                await _shopBackendApiService.GetProduct(id.Value);

            if (product == null)
                return NotFound();
            
            Product = product;
            return Page();
        }
    }
}
