using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Models;
using Shop.Services;

namespace Shop.Pages.Product
{
    public class CreateModel : PageModel
    {
        private readonly IShopBackendApiService _shopBackendApiService;

        public CreateModel(IShopBackendApiService shopBackendApiService) =>
            _shopBackendApiService = shopBackendApiService;

        public IActionResult OnGet() => Page();

        [BindProperty]
        public ProductCreate Product { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid ||
                Product == null)
                return Page();

            await _shopBackendApiService.CreateProduct(Product);

            return RedirectToPage("./Index");
        }
    }
}
