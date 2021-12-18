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
        /// Returns a Account from an instance of an Account_db
        /// </summary>
        /// <param name="inst"></param>
        /// <returns>null if inst was null</returns>
        public static Orders Convert(Orders_db inst)
        {
            if (inst == null)
                return null;

            return new Orders(inst.Id, inst.AccountRef, inst.Action, inst.DateCreated, inst.Quantity, inst.Status, inst.Symbol);
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
            int Id = (data.ContainsKey("id") ? data.GetValue("id").Value<int>() : 0);
            int accountref = (data.ContainsKey("accountref") ? data.GetValue("accountref").Value<int>() : 0);
            int action = (data.ContainsKey("action") ? data.GetValue("action").Value<int>() : 0);
            DateTime datecreated = (data.ContainsKey("datecreated") ? data.GetValue("datecreated").Value<DateTime>() : DateTime.UnixEpoch);
            int quantity = (data.ContainsKey("quantity") ? data.GetValue("quantity").Value<int>() : 0);
            int status = (data.ContainsKey("status") ? data.GetValue("status").Value<int>() : 0);
            string symbol = (data.ContainsKey("symbol") ? data.GetValue("symbol").Value<string>() : null);


            // Add instance to DB
            var inst = OrdersHelper_db.Add(Id, accountref, action, datecreated, quantity, status, symbol, context, out StatusResponse statusResponse);

            // Process includeErrors
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError && !includeDetails)
            {
                statusResponse.Message = "Error occured adding new account.";
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
                statusResponse.Message = "Something went wrong while retrieving the Account.";
            }

            // Return response
            var response = new ResponseMessage(inst != null, statusResponse.Message, inst);
            stat = statusResponse.StatusCode;
            return response;
        }
    }
}
