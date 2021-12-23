using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webservice.ContextHelpers;
using DatabaseLibrary.Helpers;
using DatabaseLibrary.Core;
using TradingLibrary.Models;
namespace TradingDB.Pages
{
        public class AccountAddFundsModel : PageModel
    {

        private readonly DatabaseContextHelper _context;

        public AccountAddFundsModel(DatabaseContextHelper context)
        {
            _context = context;
        }

        [BindProperty]
        public AddFunds AddFunds { get; set; }
        public void OnGet()
        {
            AddFunds = new AddFunds();
            AddFunds.Amount = 0;
        }

        public IActionResult OnPostAdd()
        {
            Console.WriteLine(AddFunds.Amount.ToString());
            foreach (var item in Account_SummaryHelper_db.getCollection(_context.DBContext))
            {
                if (int.Parse(HttpContext.Session.GetString("AccountID")) == @item.AccountRef && AddFunds.Amount > 0)
                {
                    Account_SummaryHelper_db.UpdateFunds(int.Parse(HttpContext.Session.GetString("AccountID")), @item.AvailableFunds + AddFunds.Amount, _context.DBContext, out StatusResponse resp);
                    return RedirectToPage("/AccountSummary");
                }
            }
            return RedirectToPage("/AccountAddFunds");
        }

        public IActionResult OnPostWithdraw()
        {
            foreach (var item in Account_SummaryHelper_db.getCollection(_context.DBContext))
            {
                if (int.Parse(HttpContext.Session.GetString("AccountID")) == @item.AccountRef && (@item.AvailableFunds - AddFunds.Amount) >= 0)
                {
                    Account_SummaryHelper_db.UpdateFunds(int.Parse(HttpContext.Session.GetString("AccountID")), @item.AvailableFunds - AddFunds.Amount, _context.DBContext, out StatusResponse resp);
                    return RedirectToPage("/AccountSummary");
                }
            }
            return RedirectToPage("/AccountAddFunds");
        }

    }

    public class AddFunds
    {
        [System.ComponentModel.DataAnnotations.Required]
        public double Amount { get; set; }

    }

}
