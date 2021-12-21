
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Webservice.Pages
{
    public class AccountSummaryModel : PageModel
    {
        public void OnGet(){
        }

        public void OnGetSetAccount(string account) {
            
        }

        public IActionResult Logout() {
            HttpContext.Session.SetString("AccountID", "-1");
            return RedirectToPage("ClientPage");
        }

    }
}
