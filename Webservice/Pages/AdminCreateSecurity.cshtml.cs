using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TradingDB.Pages
{
    public class AdminCreateSecurityModel : PageModel
    {

        [BindProperty]
        public CreateSecurity CreateSecurity { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            return RedirectToPage("AdminSecurityPage");
        }
    }

    public class CreateSecurity
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string Symbol { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Description { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public float Price { get; set; }



    }
}
