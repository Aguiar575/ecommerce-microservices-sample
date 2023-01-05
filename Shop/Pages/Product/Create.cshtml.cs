using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Context;
using Shop.Models;

namespace Shop.Pages.Product
{
    public class CreateModel : PageModel
    {
        private readonly Shop.Context.ShopContext _context;

        public CreateModel(Shop.Context.ShopContext context)
        {
            _context = context;
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
          if (!ModelState.IsValid || _context.Product == null || ProductModel == null)
            {
                return Page();
            }

            _context.Product.Add(ProductModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
