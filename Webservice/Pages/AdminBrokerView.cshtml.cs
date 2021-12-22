using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webservice.ContextHelpers;
using DatabaseLibrary.Helpers;
using DatabaseLibrary.Core;
using TradingLibrary.Models;
namespace TradingDB.Pages
{
    public class AdminBrokerViewModel : Controller
    {


        private readonly DatabaseContextHelper _context;

        public AdminBrokerViewModel(DatabaseContextHelper context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }

        public IActionResult Delete(string name)
        {
            foreach (var item in AccountsHelper_db.getCollection(_context.DBContext)) {
                if (item.Broker == name) {
                    return RedirectToPage("/AdminBrokerView");
                }
            }
            CommissionsHelper_db.RemoveByBroker(name, _context.DBContext, out StatusResponse response);
            BrokersHelper_db.Remove(name, _context.DBContext, out StatusResponse statusResponse);
            return RedirectToPage("/AdminBrokerView");
        }
    }
}
