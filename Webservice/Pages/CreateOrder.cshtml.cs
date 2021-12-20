using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webservice.ContextHelpers;
using DatabaseLibrary.Helpers;
using DatabaseLibrary.Core;


namespace TradingDB.Pages
{
    public class CreateOrderModel : PageModel
    {
        private readonly DatabaseContextHelper _context;

        public CreateOrderModel(DatabaseContextHelper context)
        {
            _context = context;
        }

        [BindProperty]
        public CreateOrder CreateOrder { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            OrdersHelper_db.Add(int.Parse(HttpContext.Session.GetString("AccountID")), CreateOrder.Action, CreateOrder.TargetPrice, DateTime.Now, CreateOrder.Quantity, 0, CreateOrder.Symbol, _context.DBContext);
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
