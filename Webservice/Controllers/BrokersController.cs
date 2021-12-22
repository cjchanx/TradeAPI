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

    [Route("api/brokers")]
    [ApiController]
    public class BrokersController : ControllerBase
    {
        #region Intialization
        public BrokersController(IWebHostEnvironment h, AppSettingsHelper a, DatabaseContextHelper db)
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
        [Route("GetBrokers")]
        public ResponseMessage GetBrokers()
        {
            var response = BrokersHelper.GetCollection(Database.DBContext, out HttpStatusCode stat, HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)stat;
            return response;
        }

        [HttpPost]
        [Route("AddBrokers")]
        public ResponseMessage AddBroker([FromBody] JObject data)
        {
            var resp = BrokersHelper.Add(data, Database.DBContext, out HttpStatusCode stat, HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)stat;
            return resp;
        }

        [HttpDelete("{name:string}")]
        [Route("DeleteBroker/{name}")]
        public ResponseMessage DeleteBroker(string name)
        {
            var resp = BrokersHelper.Remove(name, Database.DBContext, out HttpStatusCode stat, HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)stat;
            return resp;
        }
    }
}
