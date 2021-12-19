using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webservice.ContextHelpers;
using DatabaseLibrary.Helpers;
using DatabaseLibrary.Core;


namespace TradingDB.Pages
{
    public class AdminCreateBrokersModel : PageModel
    {

        private readonly DatabaseContextHelper _context;

        public AdminCreateBrokersModel(DatabaseContextHelper context)
        {
            _context = context;
        }

        [BindProperty]
        public CreateBroker CreateBroker { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            BrokersHelper_db.Add(CreateBroker.Name, CreateBroker.Website, _context.DBContext, out StatusResponse resp);
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
