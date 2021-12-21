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
            foreach (var item in AccountsHelper_db.getCollection(_context.DBContext))
            {
                if (HttpContext.Session.GetString("AccountID") == item.Id.ToString())
                {
                    OrdersHelper_db.Add(item.Id, CreateOrder.Action, CreateOrder.TargetPrice, CreateOrder.Quantity, 1, CreateOrder.Symbol, item.Broker.ToString(), _context.DBContext);
                    return RedirectToPage("AccountOrders");
                }
            }
            return RedirectToPage("CreateOrder");
        }
    }

    public class CreateOrder
    {
        [System.ComponentModel.DataAnnotations.Required]
        public int Action { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public double TargetPrice { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public int Quantity { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Symbol { get; set; }
    }
}
