using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TradingDB.Pages
{
    public class CreateOrderModel : PageModel
    {
        [BindProperty]
        public CreateOrder CreateOrder { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            return RedirectToPage("AccountOrders");
        }
    }

    public class CreateOrder
    {
        [System.ComponentModel.DataAnnotations.Required]
        public int Action { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public float TargetPrice { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public int Quantity { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Symbol { get; set; }
    }
}
