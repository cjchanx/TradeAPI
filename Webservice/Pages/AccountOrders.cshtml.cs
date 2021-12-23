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
            OrdersHelper_db.UpdateOrders(_context.DBContext, out StatusResponse resp);
        }

        public IActionResult Delete(int id) {
            foreach (var order in OrdersHelper_db.getCollection(_context.DBContext)) {
                if (order.Id == id) {
                    foreach (var summary in Account_SummaryHelper_db.getCollection(_context.DBContext))
                    {
                        if (summary.AccountRef == order.AccountRef)
                        {
                            Account_SummaryHelper_db.UpdateFunds(summary.AccountRef, summary.AvailableFunds + (order.TargetPrice * order.Quantity), _context.DBContext, out StatusResponse ressp);
                            OrdersHelper_db.Remove(id, _context.DBContext, out StatusResponse resp);
                            return RedirectToPage("/AccountOrders");
                        }
                    }
                }
            }
            return RedirectToPage("/AccountOrders");
        }

    }
}
