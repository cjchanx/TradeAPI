using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webservice.ContextHelpers;
using DatabaseLibrary.Helpers;
using DatabaseLibrary.Core;
using TradingLibrary.Models;
namespace TradingDB.Pages
{
    public class ClientPageModel : PageModel
    {
        private readonly DatabaseContextHelper _context;
        public ClientPageModel(DatabaseContextHelper context)
        {
            _context = context;
        }

        [BindProperty]
        public Credential Credential { get; set; }
        public void OnGet()
        {
            HttpContext.Session.SetString("AccountName", "-1");
            HttpContext.Session.SetString("AccountID", "-1");
        }

        public IActionResult OnPost() {

            foreach (var item in AccountsHelper_db.getCollection(_context.DBContext))
            {
                if (item.Name == Credential.Username.ToString() && item.Password == Credential.Password.ToString()) {
                    HttpContext.Session.SetString("AccountName", Credential.Username.ToString());
                    HttpContext.Session.SetString("AccountID", item.Id.ToString());
                    return RedirectToPage("AccountSummary");
                }
            }
            return RedirectToPage("ClientPage");
        }
    }

    public class Credential
    {

        [System.ComponentModel.DataAnnotations.Required]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
