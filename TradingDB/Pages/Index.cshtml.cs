using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TradingDB.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPostAdminLogin() {
            return RedirectToPage("AdminPage");
        }

        public IActionResult OnPostClientLogin()
        {
            return RedirectToPage("ClientPage");
        }
    }
}