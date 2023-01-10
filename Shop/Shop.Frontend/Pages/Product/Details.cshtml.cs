using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Shop.Context;
using Shop.Models;

namespace Shop.Pages.Product
{
    public class DetailsModel : PageModel
    {
        private readonly Shop.Context.ShopContext _context;

        public DetailsModel(Shop.Context.ShopContext context)
        {
            _context = context;
        }

      public ProductModel ProductModel { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(ulong? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var productmodel = await _context.Product.FirstOrDefaultAsync(m => m.Id == id);
            if (productmodel == null)
            {
                return NotFound();
            }
            else 
            {
                ProductModel = productmodel;
            }
            return Page();
        }
    }
}
