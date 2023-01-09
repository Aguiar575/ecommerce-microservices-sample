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
    public class IndexModel : PageModel
    {
        private readonly Shop.Context.ShopContext _context;

        public IndexModel(Shop.Context.ShopContext context)
        {
            _context = context;
        }

        public IList<ProductModel> ProductModel { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Product != null)
            {
                ProductModel = await _context.Product.ToListAsync();
            }
        }
    }
}
