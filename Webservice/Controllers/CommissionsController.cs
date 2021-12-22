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
using DatabaseLibrary.Core;

namespace Webservice.Controllers
{
    [Route("api/commissions")]
    [ApiController]
    public class CommissionsController : Controller
    {
        #region Intialization
        public CommissionsController(IWebHostEnvironment h, AppSettingsHelper a, DatabaseContextHelper db)
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
        [Route("GetCommissions")]
        public ResponseMessage GetCommissions()
        {
            var response = CommissionsHelper.GetCollection(Database.DBContext, out HttpStatusCode stat, HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)stat;
            return response;
        }

        [HttpDelete]
        [Route("DeleteCommission")]
        public ResponseMessage DeleteCommission([FromBody] JObject data)
        {
            string name = (data.ContainsKey("broker") ? data.GetValue("broker").Value<string>() : null);
            int type = (data.ContainsKey("type") ? data.GetValue("type").Value<int>() : -1);
            if (name == null)
                return new ResponseMessage(false, "Invalid broker.", null);
            if (type < 0)
                return new ResponseMessage(false, "Invalid type.", null);

            var resp = CommissionsHelper.Remove(name ,type, Database.DBContext, out HttpStatusCode stat, HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)stat;
            return resp;
        }

        [HttpDelete]
        [Route("DeleteByBroker")]
        public ResponseMessage DeleteByBroker([FromBody] JObject data)
        {
            string name = (data.ContainsKey("broker") ? data.GetValue("broker").Value<string>() : null);
            if (name == null)
                return new ResponseMessage(false, "Invalid broker.", null);

            var resp = CommissionsHelper.Remove(name, Database.DBContext, out HttpStatusCode stat, HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)stat;
            return resp;
        }

        [HttpPost]
        [Route("AddCommission")]
        public ResponseMessage AddCommission([FromBody] JObject data)
        {
            var resp = CommissionsHelper.Add(data, Database.DBContext, out HttpStatusCode stat, HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)stat;
            return resp;
        }

    }
}
