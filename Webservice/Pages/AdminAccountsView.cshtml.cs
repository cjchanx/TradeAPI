using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DatabaseLibrary.Helpers;
using DatabaseLibrary.Core;
namespace Webservice.Pages
{
    public class AdminAccountsViewModel : PageModel
    {
        public void OnGet()
        {
        }

        public ActionResult Create() {
            AccountsHelper_db test = new AccountsHelper_db();
      
            return null;
        }
    }
}
