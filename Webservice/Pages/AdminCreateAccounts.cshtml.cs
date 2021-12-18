using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TradingDB.Pages
{
    public class AdminCreateAccountsModel : PageModel
    {

        [BindProperty]
        public CreateAccount CreateAccount { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
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
