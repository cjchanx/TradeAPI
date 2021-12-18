using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DatabaseLibrary.Helpers;
namespace TradingDB.Pages
{
    public class ClientPageModel : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost() {
            HttpContext.Session.SetString("AccountName", Credential.UserName.ToString());
            return RedirectToPage("AccountSummary");
        }
    }

    public class Credential
    {

        [System.ComponentModel.DataAnnotations.Required]
        public string UserName { get; set; }
    }
}
