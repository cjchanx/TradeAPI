using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webservice.ContextHelpers;
using DatabaseLibrary.Helpers;
using DatabaseLibrary.Core;
using TradingLibrary.Models;
namespace TradingDB.Pages
{
    public class AdminAccountsViewModel : Controller
    {

        private readonly DatabaseContextHelper _context;

        public AdminAccountsViewModel(DatabaseContextHelper context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }

        public IActionResult Delete(int id)
        {
            AccountsHelper_db.ForceDelete(_context.DBContext, out StatusResponse response, id);
            return RedirectToPage("/AdminAccountsView");
        }
    }
}
