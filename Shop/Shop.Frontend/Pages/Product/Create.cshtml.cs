using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Context;
using Shop.Models;
using Shop.Services;

namespace Shop.Pages.Product
{
    public class CreateModel : PageModel
    {
        private readonly ShopContext _context;
        private readonly SnowflakeService _snowflakeService;

        public CreateModel(ShopContext context)
        {
            _context = context;
            _snowflakeService = new SnowflakeService();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ProductModel ProductModel { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid ||
                _context.Product == null ||
                ProductModel == null)
                return Page();
            
            SnowflakeIdViewModel snowflakeId = await _snowflakeService.SnowflakeId();
            ProductModel.Id = snowflakeId.id.Value;

            _context.Product.Add(ProductModel);
            await _context.SaveChangesWithIdentityInsertAsync<ProductModel>();

            return RedirectToPage("./Index");
        }
    }
}
