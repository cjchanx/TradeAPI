using DatabaseLibrary.Models;
using DatabaseLibrary.Core;
using DatabaseLibrary.Helpers;
using TradingLibrary.Models;
using System.Net;
using Newtonsoft.Json.Linq;


namespace Webservice.ControllerHelpers
{
    public class OrdersHelper
    {
        #region Converters

        /// <summary>
        /// Returns a Order from an instance of an Order_db
        /// </summary>
        /// <param name="inst"></param>
        /// <returns>null if inst was null</returns>
        public static Orders Convert(Orders_db inst)
        {
            if (inst == null)
                return null;

            return new Orders(inst.Id, inst.AccountRef, inst.Action, inst.TargetPrice, inst.DateCreated, inst.Quantity, inst.Status, inst.Symbol, inst.Broker);
        }

        #endregion

        // Methods
        /// <summary>
        /// Sets up a new account based on the given object data.
        /// </summary>
        /// <param name="includeDetails">Whether to include detailed internal server message.</param>
        /// <returns></returns>
        public static ResponseMessage Add(JObject data, DBContext context, out HttpStatusCode stat, bool includeDetails = false)
        {
            // Extract parameters
            int accountref = (data.ContainsKey("accountref") ? data.GetValue("accountref").Value<int>() : 0);
            int action = (data.ContainsKey("action") ? data.GetValue("action").Value<int>() : 0);
            double targetprice = (data.ContainsKey("targetprice") ? data.GetValue("targetprice").Value<double>() : 0);
            DateTime datecreated = (data.ContainsKey("datecreated") ? data.GetValue("datecreated").Value<DateTime>() : DateTime.UnixEpoch);
            int quantity = (data.ContainsKey("quantity") ? data.GetValue("quantity").Value<int>() : 0);
            int status = (data.ContainsKey("status") ? data.GetValue("status").Value<int>() : 0);
            string symbol = (data.ContainsKey("symbol") ? data.GetValue("symbol").Value<string>() : null);
            string broker = (data.ContainsKey("broker") ? data.GetValue("broker").Value<string>() : null);


            // Add instance to DB
            var inst = OrdersHelper_db.Add(accountref, action, targetprice, datecreated, quantity, status, symbol, broker, context, out StatusResponse statusResponse);

            // Process includeErrors
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError && !includeDetails)
            {
                statusResponse.Message = "Error occured adding new order.";
            }

            // Setup and return response
            var response = new ResponseMessage(
                inst != null,
                statusResponse.Message,
                Convert(inst)
            );
            stat = statusResponse.StatusCode;
            return response;
        }

        public static ResponseMessage GetCollection(DBContext context, out HttpStatusCode stat, bool includeDetailsErrors = false)
        {
            // Get instances from DB
            var dbInst = OrdersHelper_db.getCollection(context, out StatusResponse statusResponse);

            // Convert to object
            var inst = dbInst?.Select(x => OrdersHelper.Convert(x)).ToList();

            // Process includeErrors
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError && !includeDetailsErrors)
            {
                statusResponse.Message = "Something went wrong while retrieving the order.";
            }

            // Return response
            var response = new ResponseMessage(inst != null, statusResponse.Message, inst);
            stat = statusResponse.StatusCode;
            return response;
        }

        public static ResponseMessage GetCollectionByAccount(int id, DBContext context, out HttpStatusCode stat, bool includeDetailsErrors = false)
        {
            // Get instances from DB
            var dbInst = OrdersHelper_db.GetCollectionByAccount(id, context, out StatusResponse statusResponse);

            // Convert to object
            var inst = dbInst?.Select(x => OrdersHelper.Convert(x)).ToList();

            // Process includeErrors
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError && !includeDetailsErrors)
            {
                statusResponse.Message = "Something went wrong while retrieving the orders.";
            }

            // Return response
            var response = new ResponseMessage(inst != null, statusResponse.Message, inst);
            stat = statusResponse.StatusCode;
            return response;
        }

        public static ResponseMessage Remove(DBContext context, int id, out HttpStatusCode stat, bool includeDetailsErrors = false)
        {
            // Remove instance from DB
            var inst = OrdersHelper_db.Remove(id, context, out StatusResponse statusResponse);

            if(inst == 0)
            {
                statusResponse.Message = "Error occured removing order.";
            }

            // Process includeErrors
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError && !includeDetailsErrors)
            {
                statusResponse.Message = "Error occured removing order.";
            }

            // Setup and return response
            var response = new ResponseMessage(
                inst != 0,
                statusResponse.Message,
                null
            );
            stat = statusResponse.StatusCode;
            return response;
        }

        /// <summary>
        /// Updates an order based on the given Id
        /// </summary>
        /// <param name="includeDetails">Whether to include detailed internal server message.</param>
        /// <returns></returns>
        public static ResponseMessage Edit(int Id, JObject data, DBContext context, out HttpStatusCode stat, bool includeDetails = false)
        {
            // Extract parameters
            int accountref = (data.ContainsKey("accountref") ? data.GetValue("accountref").Value<int>() : 0);
            int action = (data.ContainsKey("action") ? data.GetValue("action").Value<int>() : 0);
            double targetprice = (data.ContainsKey("targetprice") ? data.GetValue("action").Value<double>() : 0);
            DateTime datecreated = (data.ContainsKey("datecreated") ? data.GetValue("datecreated").Value<DateTime>() : DateTime.UnixEpoch);
            int quantity = (data.ContainsKey("quantity") ? data.GetValue("quantity").Value<int>() : 0);
            int status = (data.ContainsKey("status") ? data.GetValue("status").Value<int>() : 0);
            string symbol = (data.ContainsKey("symbol") ? data.GetValue("symbol").Value<string>() : null);
            string broker = (data.ContainsKey("broker") ? data.GetValue("broker").Value<string>() : null);


            // Add instance to DB
            var inst = OrdersHelper_db.Edit(Id, accountref, action, targetprice, datecreated, quantity, status, symbol, broker, context, out StatusResponse statusResponse);

            // Process includeErrors
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError && !includeDetails)
            {
                statusResponse.Message = "Error occured editing order.";
            }

            // Setup and return response
            var response = new ResponseMessage(
                inst != null,
                statusResponse.Message,
                Convert(inst)
            );
            stat = statusResponse.StatusCode;
            return response;
        }

    }
}
