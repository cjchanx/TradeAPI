using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Webservice.Configuration;
using Webservice.ContextHelpers;
using Webservice.ControllerHelpers;
using TradingLibrary.Models;
using DatabaseLibrary.Helpers;
using System.Net;

namespace Webservice.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {


        #region Intialization
        public AccountsController(IWebHostEnvironment h, AppSettingsHelper a, DatabaseContextHelper db)
        {
            HostingEnvironment = h;
            AppSettings = a;
            Database = db;
        }

        /// <summary>
        /// Reference to hosting environment instance.
        /// </summary>
        private readonly IWebHostEnvironment HostingEnvironment;

        /// <summary>
        /// Reference to app settings helper.
        /// </summary>
        private readonly AppSettingsHelper AppSettings;

        /// <summary>
        /// Reference to the DBContextHelper instance.
        /// </summary>
        private readonly DatabaseContextHelper Database;

        #endregion

        // API

        [HttpGet]
        [Route("GetAccounts")]
        public ResponseMessage GetAccounts()
        {
            var response = AccountsHelper.GetCollection(Database.DBContext, out HttpStatusCode stat, HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)stat;
            return response;
        }

        [HttpPost]
        [Route("AddAccount")]
        public ResponseMessage AddAccount([FromBody] JObject data)
        {
            var resp = AccountsHelper.Add(data, Database.DBContext, out HttpStatusCode stat, HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)stat;
            return resp;
        }

        [HttpPut]
        [Route("EditAccount")]
        public ResponseMessage EditAccount([FromBody] JObject data)
        {
            var resp = AccountsHelper.Update(data, Database.DBContext, out HttpStatusCode stat, HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)stat;
            return resp;
        }


        [HttpGet("{id:int}")]
        [Route("GetAccountById/{id}")]
        public ResponseMessage GetAccount_SummaryByAccount(int id)
        {
            var response = AccountsHelper.GetAccountById(id, Database.DBContext, out HttpStatusCode stat, HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)stat;
            return response;
        }

    }
}
