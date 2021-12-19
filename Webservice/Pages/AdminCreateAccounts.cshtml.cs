using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webservice.ContextHelpers;
using DatabaseLibrary.Helpers;
using DatabaseLibrary.Core;
using TradingLibrary.Models;

namespace TradingDB.Pages
{
    public class AdminCreateAccountsModel : PageModel
    {

        private readonly DatabaseContextHelper _context;

        public AdminCreateAccountsModel(DatabaseContextHelper context)
        {
            _context = context;
        }

        [BindProperty]
        public CreateAccount CreateAccount { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            AccountsHelper_db.Add(0, CreateAccount.Broker, CreateAccount.Name, CreateAccount.Description, _context.DBContext, out StatusResponse resp);
            Account_SummaryHelper_db.Add(AccountsHelper_db.getCollection(_context.DBContext, out StatusResponse collection).ToArray().Last().Id, 10000, 0, 10000, _context.DBContext, out StatusResponse resps);
            return RedirectToPage("AdminAccountsView");
        }
    }

    public class CreateAccount
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string Broker { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Description { get; set; }


    }
}
