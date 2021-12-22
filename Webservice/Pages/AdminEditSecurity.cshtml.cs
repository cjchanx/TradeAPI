using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webservice.ContextHelpers;
using DatabaseLibrary.Helpers;
using DatabaseLibrary.Core;
using TradingLibrary.Models;
namespace TradingDB.Pages
{
    public class AdminEditSecurityModel : PageModel
    {
        private readonly DatabaseContextHelper _context;

        public AdminEditSecurityModel(DatabaseContextHelper context)
        {
            _context = context;
        }

        [BindProperty]
        public UpdateSecurity UpdateSecurity { get; set; }

        public void OnGet(string symbol)
        {

            foreach (var item in SecurityHelper_db.getCollection(_context.DBContext))
            {
                if (@item.Symbol == symbol)
                {
                    UpdateSecurity = new UpdateSecurity();
                    UpdateSecurity.Symbol = symbol;
                    UpdateSecurity.Description = @item.Description;
                    UpdateSecurity.Price = @item.Price;
                }
            }
        }

        public IActionResult OnPost()
        {
            if (UpdateSecurity.Price > 0) {
                Console.WriteLine("Update");
                return RedirectToPage("AdminSecurityPage");

            }
            return RedirectToPage("AdminSecurityPage");
        }
    }

    public class UpdateSecurity
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string Symbol { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Description { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public double Price { get; set; }
    }
}
