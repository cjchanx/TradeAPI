using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webservice.ContextHelpers;
using DatabaseLibrary.Helpers;
using DatabaseLibrary.Core;
using TradingLibrary.Models;

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
            if (CreateOrder.TargetPrice <= 0) {
                return RedirectToPage("CreateOrder");
            }
            foreach (var item in AccountsHelper_db.getCollection(_context.DBContext))
            {
                if (HttpContext.Session.GetString("AccountID") == item.Id.ToString())
                {
                    foreach (var summary in Account_SummaryHelper_db.getCollection(_context.DBContext)) {
                        if (summary.AccountRef == item.Id) {
                            if (summary.AvailableFunds >= (CreateOrder.TargetPrice * CreateOrder.Quantity) && (CreateOrder.Action == 2 || CreateOrder.Action == 3))
                            {
                                foreach (var security in SecurityHelper_db.getCollection(_context.DBContext))
                                {
                                    if (security.Symbol == CreateOrder.Symbol)
                                    {
                                        OrdersHelper_db.Add(item.Id, CreateOrder.Action, CreateOrder.TargetPrice, CreateOrder.Quantity, 1, CreateOrder.Symbol, item.Broker.ToString(), _context.DBContext);
                                        OrdersHelper_db.UpdateOrders(_context.DBContext, out StatusResponse resp);
                                        return RedirectToPage("AccountOrders");
                                    }
                                }
                                return RedirectToPage("CreateOrder");

                            }
                            else {
                                foreach (var security in SecurityHelper_db.getCollection(_context.DBContext))
                                {
                                    if(security.Symbol == CreateOrder.Symbol)
                                    {
                                        foreach (var owned in OwnedSecurityHelper_db.GetOwnedByAccount(summary.AccountRef, _context.DBContext)) {
                                            if (owned.Symbol == CreateOrder.Symbol) {
                                                if (owned.Quantity >= CreateOrder.Quantity) {
                                                    OrdersHelper_db.Add(item.Id, CreateOrder.Action, CreateOrder.TargetPrice, CreateOrder.Quantity, 1, CreateOrder.Symbol, item.Broker.ToString(), _context.DBContext);
                                                    OrdersHelper_db.UpdateOrders(_context.DBContext, out StatusResponse resp);
                                                    return RedirectToPage("AccountOrders");
                                                }
                                                return RedirectToPage("CreateOrder");
                                            }
                                        }
                                        return RedirectToPage("AccountOrders");
                                    }
                                }
                                return RedirectToPage("CreateOrder");
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
