using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webservice.ContextHelpers;
using DatabaseLibrary.Helpers;
using DatabaseLibrary.Core;
using TradingLibrary.Models;
namespace TradingDB.Pages
{
    public class AdminSecurityPageModel : Controller
    {

        private readonly DatabaseContextHelper _context;

        public AdminSecurityPageModel(DatabaseContextHelper context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }

        public IActionResult Delete(string symbol)
        {
            Console.WriteLine("Remove " + symbol);
            return RedirectToPage("/AdminSecurityPage");
        }
    }
}
