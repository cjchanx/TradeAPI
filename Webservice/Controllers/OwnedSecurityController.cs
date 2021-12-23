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
    [Route("api/ownedsecurities")]
    [ApiController]
    public class OwnedSecurityController : Controller
    {
        #region Intialization
        public OwnedSecurityController(IWebHostEnvironment h, AppSettingsHelper a, DatabaseContextHelper db)
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
        [Route("GetAll")]
        public ResponseMessage GetAll()
        {
            var response = OwnedSecurityHelper.GetCollection(Database.DBContext, out HttpStatusCode stat, HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)stat;
            return response;
        }

        [HttpGet("{id:int}")]
        [Route("GetByAccount/{id}")]
        public ResponseMessage GetByAccount(int id)
        {
            var response = OwnedSecurityHelper.GetCollectionByAccount(id, Database.DBContext, out HttpStatusCode stat, HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)stat;
            return response;
        }

        [HttpPost]
        [Route("AddOwnedSecurity")]
        public ResponseMessage AddOrders([FromBody] JObject data)
        {
            var resp = OwnedSecurityHelper.Add(data, Database.DBContext, out HttpStatusCode stat, HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)stat;
            return resp;
        }

        [HttpDelete]
        [Route("DeleteOwned")]
        public ResponseMessage DeleteOwned([FromBody] JObject data)
        {
            int id = (data.ContainsKey("accountref") ? data.GetValue("accountref").Value<int>() : -1);
            string name = (data.ContainsKey("symbol") ? data.GetValue("symbol").Value<string>() : null);
            if (name == null)
                return new ResponseMessage(false, "Invalid name.", null);
            if (id == -1)
                return new ResponseMessage(false, "Invalid AccountRef.", null);

            var resp = OwnedSecurityHelper.Remove(id, name, Database.DBContext, out HttpStatusCode stat, HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)stat;
            return resp;
        }
    }
}
