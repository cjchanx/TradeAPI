using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webservice.ContextHelpers;
using DatabaseLibrary.Helpers;
using DatabaseLibrary.Core;
using TradingLibrary.Models;
namespace TradingDB.Pages
{
    public class AdminPageModel : PageModel
    {
        private readonly DatabaseContextHelper _context;
        public AdminPageModel(DatabaseContextHelper context)
        {
            _context = context;
        }

        [BindProperty]
        public AdminCredential AdminCredential { get; set; }
        public void OnGet()
        {
            HttpContext.Session.SetString("AccountName", "-1");
            HttpContext.Session.SetString("AccountID", "-1");
        }

        public IActionResult OnPost()
        {

            if (AdminCredential.Username == "admin" && AdminCredential.Password == "password") {
                return RedirectToPage("AdminAccountsView");
            }
            return RedirectToPage("Home");
        }
    }

    public class AdminCredential
    {

        [System.ComponentModel.DataAnnotations.Required]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}