using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DatabaseLibrary.Helpers;
using Webservice.ControllerHelpers;
using TradingLibrary.Models;
namespace Webservice.Pages
{
    public class AdminAccountsViewModel : PageModel
    {
        public void OnGet()
        {
        }

        public ActionResult Create() {
            List<Accounts> list = new List<Accounts>();
            AccountsHelper accountsHelper = new AccountsHelper();
            list = accountsHelper.data;

            return ViewComponent(list);
        }
    }
}
