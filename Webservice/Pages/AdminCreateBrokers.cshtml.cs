using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TradingDB.Pages
{
    public class AdminCreateBrokersModel : PageModel
    {

        [BindProperty]
        public CreateBroker CreateBroker { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            return RedirectToPage("AdminBrokerView");
        }
    }

    public class CreateBroker
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Website { get; set; }



    }
}
