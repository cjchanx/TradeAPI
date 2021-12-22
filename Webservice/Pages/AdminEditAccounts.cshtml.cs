using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webservice.ContextHelpers;
using DatabaseLibrary.Helpers;
using DatabaseLibrary.Core;
using TradingLibrary.Models;
namespace TradingDB.Pages
{
    public class AdminEditAccountsModel : PageModel
    {
        private readonly DatabaseContextHelper _context;

        public AdminEditAccountsModel(DatabaseContextHelper context)
        {
            _context = context;
        }

        [BindProperty]
        public UpdateAdminAccount UpdateAdminAccount { get; set; }
        public void OnGet(int id)
        {
            foreach (var item in AccountsHelper_db.getCollection(_context.DBContext))
            {
                if (@item.Id == id)
                {
                    UpdateAdminAccount = new UpdateAdminAccount();
                    UpdateAdminAccount.Id = id;
                    UpdateAdminAccount.Active = @item.Active;
                    UpdateAdminAccount.Broker = @item.Broker;
                    UpdateAdminAccount.DateCreated = @item.Date;
                    UpdateAdminAccount.Name = @item.Name;
                    UpdateAdminAccount.Description = @item.Description;
                    UpdateAdminAccount.Password = @item.Password;
                }
            }
        }

        public IActionResult OnPost()
        {
            return RedirectToPage("AdminAccountsView");
        }
    }

    public class UpdateAdminAccount
    {
        [System.ComponentModel.DataAnnotations.Required]
        public int Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public bool Active { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Broker { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public DateTime DateCreated { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Description { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Password { get; set; }

    }
}
