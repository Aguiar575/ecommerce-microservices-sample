using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shop.Pages
{
    public class IndexModel : PageModel
    {
        public Task OnGetAsync() =>
            Task.CompletedTask;
    }
}
