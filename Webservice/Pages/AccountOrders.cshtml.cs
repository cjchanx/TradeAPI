using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webservice.ContextHelpers;
using DatabaseLibrary.Helpers;
using DatabaseLibrary.Core;
using TradingLibrary.Models;
namespace TradingDB.Pages
{
    public class AccountOrdersModel : Controller
    {

        private readonly DatabaseContextHelper _context;

        public AccountOrdersModel(DatabaseContextHelper context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }

        public IActionResult Delete(int id) {
            OrdersHelper_db.Remove(id, _context.DBContext, out StatusResponse resp);
            return RedirectToPage("/AccountOrders");
        }

    }
}
