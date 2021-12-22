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
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        #region Intialization
        public OrdersController(IWebHostEnvironment h, AppSettingsHelper a, DatabaseContextHelper db)
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
        [Route("GetOrders")]
        public ResponseMessage GetOrders()
        {
            var response = OrdersHelper.GetCollection(Database.DBContext, out HttpStatusCode stat, HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)stat;
            return response;
        }

        [HttpGet("{id:int}")]
        [Route("GetOrdersByAccount/{id}")]
        public ResponseMessage GetOrdersByAccount(int id)
        {
            var response = OrdersHelper.GetCollectionByAccount(id, Database.DBContext, out HttpStatusCode stat, HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)stat;
            return response;
        }

        [HttpPost]
        [Route("UpdateOrders")]
        public ResponseMessage UpdateOrders()
        {
            // Call for update
            int rows = OrdersHelper_db.UpdateOrders(Database.DBContext, out StatusResponse statusResponse);

            // Process includeErrors
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError && !HostingEnvironment.IsDevelopment())
            {
                statusResponse.Message = "Something went wrong while attempting update.";
            }

            // Return response
            var response = new ResponseMessage(rows != -1, statusResponse.Message, rows);
            return response;
        }

        [HttpPost]
        [Route("AddOrders")]
        public ResponseMessage AddOrders([FromBody] JObject data)
        {
            var resp = OrdersHelper.Add(data, Database.DBContext, out HttpStatusCode stat, HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)stat;
            return resp;
        }

        [HttpDelete("{id:int}")]
        [Route("DeleteOrder/{id}")]
        public ResponseMessage DeleteOrder(int id)
        {
            var resp = OrdersHelper.Remove(Database.DBContext, id, out HttpStatusCode stat, HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)stat;
            return resp;
        }


    }
}
