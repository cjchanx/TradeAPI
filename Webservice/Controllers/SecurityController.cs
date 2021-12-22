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
    [Route("api/security")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        #region Intialization
        public SecurityController(IWebHostEnvironment h, AppSettingsHelper a, DatabaseContextHelper db)
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
        [Route("GetSecurity")]
        public ResponseMessage GetSecurity()
        {
            var response = SecurityHelper.GetCollection(Database.DBContext, out HttpStatusCode stat, HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)stat;
            return response;
        }

        [HttpPost]
        [Route("AddSecurity")]
        public ResponseMessage AddSecurity([FromBody] JObject data)
        {
            var resp = SecurityHelper.Add(data, Database.DBContext, out HttpStatusCode stat, HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)stat;
            return resp;
        }

        [HttpDelete]
        [Route("DeleteSecurity")]
        public ResponseMessage DeleteSecurity([FromBody] JObject data)
        {
            string name = (data.ContainsKey("symbol") ? data.GetValue("symbol").Value<string>() : null);
            if (name == null)
                return new ResponseMessage(false, "Invalid symbol.", null);

            var resp = SecurityHelper.Remove(name, Database.DBContext, out HttpStatusCode stat, HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)stat;
            return resp;
        }

        [HttpPut]
        [Route("EditSecurity")]
        public ResponseMessage EditSecurity([FromBody] JObject data)
        {
            var resp = SecurityHelper.UpdateFull(data, Database.DBContext, out HttpStatusCode stat, HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)stat;
            return resp;
        }

        [HttpPut]
        [Route("EditPrice")]
        public ResponseMessage EditPrice([FromBody] JObject data)
        {
            var resp = SecurityHelper.UpdatePrice(data, Database.DBContext, out HttpStatusCode stat, HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)stat;
            return resp;
        }

        [HttpGet]
        [Route("GetPrice")]
        public ResponseMessage GetPrice([FromBody] JObject data)
        {
            string name = (data.ContainsKey("symbol") ? data.GetValue("symbol").Value<string>() : null);
            var response = SecurityHelper.GetPrice(name, Database.DBContext, out HttpStatusCode stat, HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)stat;
            return response;
        }
    }
}
