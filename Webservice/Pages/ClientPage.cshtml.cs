using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
            HttpContext.Session.SetString("AccountID", Credential.UserName.ToString());
            return RedirectToPage("AccountSummary", "SetAccount", new {account = Credential.UserName.ToString() });
        }
    }

    public class Credential
    {

        [System.ComponentModel.DataAnnotations.Required]
        public string UserName { get; set; }
    }
}
