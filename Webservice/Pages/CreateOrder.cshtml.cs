using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webservice.ContextHelpers;
using DatabaseLibrary.Helpers;
using DatabaseLibrary.Core;
using DatabaseLibrary.Models;

namespace TradingDB.Pages
{
    public class CreateOrderModel : PageModel
    {
        private readonly DatabaseContextHelper _context;

        public List<Security_db> Symbols { get; set; }

        public CreateOrderModel(DatabaseContextHelper context)
        {
            _context = context;
        }

        [BindProperty]
        public CreateOrder CreateOrder { get; set; }

        public void OnGet()
        {
            Symbols = new List<string>();
            Symbols.Add("hi");
        }

        public IActionResult OnPost()
        {
            foreach (var item in AccountsHelper_db.getCollection(_context.DBContext))
            {
                if (HttpContext.Session.GetString("AccountID") == item.Id.ToString())
                {
                    foreach (var summary in Account_SummaryHelper_db.getCollection(_context.DBContext)) {
                        if (summary.AccountRef == item.Id) {
                            if (summary.AvailableFunds >= (CreateOrder.TargetPrice * CreateOrder.Quantity) ){
                                Account_SummaryHelper_db.UpdateFunds(item.Id, summary.AvailableFunds - (CreateOrder.TargetPrice * CreateOrder.Quantity), _context.DBContext, out StatusResponse ressp);
                                OrdersHelper_db.Add(item.Id, CreateOrder.Action, CreateOrder.TargetPrice, CreateOrder.Quantity, 1, CreateOrder.Symbol, item.Broker.ToString(), _context.DBContext);
                                OrdersHelper_db.UpdateOrders(_context.DBContext, out StatusResponse resp);
                                return RedirectToPage("AccountOrders");
                            }
                        }
                    }
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
