using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webservice.ContextHelpers;
using DatabaseLibrary.Helpers;
using DatabaseLibrary.Core;

namespace TradingDB.Pages
{
    public class AccountEditOrdersModel : PageModel
    {
        private readonly DatabaseContextHelper _context;

        public AccountEditOrdersModel(DatabaseContextHelper context)
        {
            _context = context;
        }

        [BindProperty]
        public UpdateOrder updateorder { get; set; }
        public void OnGet(int id)
        {
                foreach (var item in OrdersHelper_db.getCollection(_context.DBContext))
                {
                 if (@item.Id == id && int.Parse(HttpContext.Session.GetString("AccountID")) == @item.AccountRef) {
                        updateorder = new UpdateOrder();
                        updateorder.Action = @item.Action;
                        updateorder.Quantity = @item.Quantity;
                        updateorder.Symbol = @item.Symbol;
                        updateorder.TargetPrice = @item.TargetPrice;
                }
            }
        }

        public IActionResult OnPost()
        {
            foreach (var item in AccountsHelper_db.getCollection(_context.DBContext))
            {
                if (HttpContext.Session.GetString("AccountID") == item.Id.ToString())
                {
      
                    return RedirectToPage("AccountOrders");
                }
            }
            return RedirectToPage("AccountEditOrders");
        }
    }

    public class UpdateOrder
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