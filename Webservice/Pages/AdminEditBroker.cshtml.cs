using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webservice.ContextHelpers;
using DatabaseLibrary.Helpers;
using DatabaseLibrary.Core;
using TradingLibrary.Models;
namespace TradingDB.Pages
{
    public class AdminEditBrokerModel : PageModel
    {
        private readonly DatabaseContextHelper _context;

        public AdminEditBrokerModel(DatabaseContextHelper context)
        {
            _context = context;
        }

        [BindProperty]
        public UpdateBroker UpdateBroker { get; set; }

        public void OnGet(string name)
        {
            foreach (var broker in BrokersHelper_db.getCollection(_context.DBContext)) {
                if (broker.Name == name) {
                    UpdateBroker = new UpdateBroker();
                    UpdateBroker.Name = name;
                    UpdateBroker.Website = broker.Website;
                    UpdateBroker.Upper_limit_sell = 0;
                    UpdateBroker.Lower_limit_sell = 0;
                    UpdateBroker.Upper_limit_buy = 0;
                    UpdateBroker.Lower_limit_buy = 0;
                }
            }

            foreach (var item in CommissionsHelper_db.GetCollection(_context.DBContext, out StatusResponse resp))
            {
                if (@item.Broker == name && item.Type == 0)
                {
                    UpdateBroker.Upper_limit_sell = item.Rate;
                } else if (@item.Broker == name && item.Type == 1) {
                    UpdateBroker.Lower_limit_sell = item.Rate;
                }
                else if (@item.Broker == name && item.Type == 2)
                {
                    UpdateBroker.Upper_limit_buy = item.Rate;
                }
                else if (@item.Broker == name && item.Type == 3)
                {
                    UpdateBroker.Lower_limit_buy = item.Rate;
                }
            }

        }

        public IActionResult OnPost()
        {
            if (UpdateBroker.Upper_limit_sell >= 0 && UpdateBroker.Lower_limit_sell >= 0 && UpdateBroker.Upper_limit_buy >= 0 && UpdateBroker.Lower_limit_buy >= 0)
            {
                CommissionsHelper_db.RemoveByBroker(UpdateBroker.Name, _context.DBContext, out StatusResponse response);
                CommissionsHelper_db.Add(UpdateBroker.Name, 0, UpdateBroker.Upper_limit_sell, _context.DBContext, out StatusResponse response1);
                CommissionsHelper_db.Add(UpdateBroker.Name, 1, UpdateBroker.Lower_limit_sell, _context.DBContext, out StatusResponse response2);
                CommissionsHelper_db.Add(UpdateBroker.Name, 2, UpdateBroker.Upper_limit_buy, _context.DBContext, out StatusResponse response3);
                CommissionsHelper_db.Add(UpdateBroker.Name, 3, UpdateBroker.Lower_limit_buy, _context.DBContext, out StatusResponse response4);
                BrokersHelper_db.UpdateWebsite(UpdateBroker.Name, UpdateBroker.Website, _context.DBContext, out StatusResponse statusResponse);
                
            }

            return RedirectToPage("AdminBrokerView");
        }
    }



    public class UpdateBroker
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Website { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public double Upper_limit_sell { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public double Lower_limit_sell { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public double Upper_limit_buy { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public double Lower_limit_buy { get; set; }




    }
}
