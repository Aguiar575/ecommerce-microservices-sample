using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Models;
using Shop.Services;

namespace Shop.Pages.Product
{
    public class EditModel : PageModel
    {
        private readonly IShopBackendApiService _shopBackendApiService;

        public EditModel(IShopBackendApiService shopBackendApiService) =>
            _shopBackendApiService = shopBackendApiService;

        [BindProperty]
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _shopBackendApiService.UpdateProduct(Product);

            return RedirectToPage("./Index");
        }
    }
}
