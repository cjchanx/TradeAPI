using DatabaseLibrary.Models;
using DatabaseLibrary.Core;
using DatabaseLibrary.Helpers;
using TradingLibrary.Models;
using System.Net;
using Newtonsoft.Json.Linq;


namespace Webservice.ControllerHelpers
{
    public class TransactionsHelper
    {
        #region Converters

        /// <summary>
        /// Returns a Account_Summary from an instance of an Account_Summary_db
        /// </summary>
        /// <param name="inst"></param>
        /// <returns>null if inst was null</returns>
        public static Transactions Convert(Transactions_db inst)
        {
            if (inst == null)
                return null;

            return new Transactions(inst.Id, inst.AccountRef, inst.Action, inst.AveragePrice, inst.Commission, inst.DateCreated, inst.Price, inst.Quantity, inst.RealizedPNL);
        }

        #endregion

        // Methods
        /// <summary>
        /// Sets up a new account_summary based on the given object data.
        /// </summary>
        /// <param name="includeDetails">Whether to include detailed internal server message.</param>
        /// <returns></returns>
        public static ResponseMessage Add(JObject data, DBContext context, out HttpStatusCode stat, bool includeDetails = false)
        {
            // Extract parameters
            int id = (data.ContainsKey("id") ? data.GetValue("id").Value<int>() : 0);
            int accountref = (data.ContainsKey("accountref") ? data.GetValue("accountref").Value<int>() : 0);
            int action = (data.ContainsKey("action") ? data.GetValue("action").Value<int>() : 0);
            double averageprice = (data.ContainsKey("averageprice") ? data.GetValue("averageprice").Value<double>() : 0);
            double commission = (data.ContainsKey("commission") ? data.GetValue("commission").Value<double>() : 0);
            DateTime datecreated = (data.ContainsKey("datecreated") ? data.GetValue("datecreated").Value<DateTime>() : DateTime.UnixEpoch);
            double price = (data.ContainsKey("price") ? data.GetValue("price").Value<double>() : 0);
            int quantity = (data.ContainsKey("quantity") ? data.GetValue("quantity").Value<int>() : 0);
            double realizedpnl = (data.ContainsKey("realizedpnl") ? data.GetValue("realizedpnl").Value<double>() : 0);

            // Add instance to DB
            var inst = TransactionsHelper_db.Add(id, accountref, action, averageprice, commission, datecreated, price, quantity, realizedpnl, context, out StatusResponse statusResponse);

            // Process includeErrors
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError && !includeDetails)
            {
                statusResponse.Message = "Error occured adding new transaction.";
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
            var dbInst = TransactionsHelper_db.getCollection(context, out StatusResponse statusResponse);

            // Convert to object
            var inst = dbInst?.Select(x => TransactionsHelper.Convert(x)).ToList();

            // Process includeErrors
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError && !includeDetailsErrors)
            {
                statusResponse.Message = "Something went wrong while retrieving the transaction.";
            }

            // Return response
            var response = new ResponseMessage(inst != null, statusResponse.Message, inst);
            stat = statusResponse.StatusCode;
            return response;
        }
    }
}
