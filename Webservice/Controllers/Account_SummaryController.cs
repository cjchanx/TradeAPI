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
    [Route("api/account_summary")]
    [ApiController]
    public class Account_SummaryController : ControllerBase
    {
        #region Intialization
        public Account_SummaryController(IWebHostEnvironment h, AppSettingsHelper a, DatabaseContextHelper db)
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
        [Route("GetAccount_Summary")]
        public ResponseMessage GetAccount_Summary()
        {
            var response = Account_SummaryHelper.GetCollection(Database.DBContext, out HttpStatusCode stat, HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)stat;
            return response;
        }

        [HttpPost]
        [Route("AddAccount_Summary")]
        public ResponseMessage AddAccount_Summary([FromBody] JObject data)
        {
            var resp = Account_SummaryHelper.Add(data, Database.DBContext, out HttpStatusCode stat, HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)stat;
            return resp;
        }

        [HttpDelete("{id:int}")]
        [Route("DeleteAccount_Summary/{id}")]
        public ResponseMessage DeleteSummary(int id)
        {
            var resp = Account_SummaryHelper.DeleteAccountSummary(id, Database.DBContext, out HttpStatusCode stat, HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)stat;
            return resp;
        }
        

        [HttpGet("{id:int}")]
        [Route("GetAccount_Summary/{id}")]
        public ResponseMessage GetAccount_SummaryByAccount(int id)
        {
            var response = Account_SummaryHelper.GetSummaryByAccount(id, Database.DBContext, out HttpStatusCode stat, HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)stat;
            return response;
        }

        [HttpPut("{id:int}")]
        [Route("EditAccountSummary/{id}")]
        public ResponseMessage EditOrder(int id, [FromBody] JObject data)
        {
            var resp = Account_SummaryHelper.Edit(id, data, Database.DBContext, out HttpStatusCode stat, HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)stat;
            return resp;
        }
    }
}
