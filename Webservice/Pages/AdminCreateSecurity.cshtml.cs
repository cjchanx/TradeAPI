using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webservice.ContextHelpers;
using DatabaseLibrary.Helpers;
using DatabaseLibrary.Core;

namespace TradingDB.Pages
{
    public class AdminCreateSecurityModel : PageModel
    {
        private readonly DatabaseContextHelper _context;

        public AdminCreateSecurityModel(DatabaseContextHelper context)
        {
            _context = context;
        }

        [BindProperty]
        public CreateSecurity CreateSecurity { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            SecurityHelper_db.Add(CreateSecurity.Symbol, CreateSecurity.Description, CreateSecurity.Price, _context.DBContext, out StatusResponse resp);
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
        public double Price { get; set; }
    }
}
