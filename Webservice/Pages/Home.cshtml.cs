using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TradingDB.Pages
{
    public class HomeModel : PageModel
    {
        private readonly ILogger<HomeModel> _logger;

        public HomeModel(ILogger<HomeModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            HttpContext.Session.SetString("AccountName", "-1");
            HttpContext.Session.SetString("AccountID", "-1");
        }

        public IActionResult OnPostAdminLogin()
        {
            return RedirectToPage("AdminAccountsView");
        }

        public IActionResult OnPostClientLogin()
        {
            return RedirectToPage("ClientPage");
        }
    }
}