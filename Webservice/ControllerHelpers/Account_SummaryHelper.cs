using DatabaseLibrary.Models;
using DatabaseLibrary.Core;
using DatabaseLibrary.Helpers;
using TradingLibrary.Models;
using System.Net;
using Newtonsoft.Json.Linq;


namespace Webservice.ControllerHelpers
{
    public class Account_SummaryHelper
    {
        #region Converters

        /// <summary>
        /// Returns a Account_Summary from an instance of an Account_Summary_db
        /// </summary>
        /// <param name="inst"></param>
        /// <returns>null if inst was null</returns>
        public static Account_Summary Convert(Account_Summary_db inst)
        {
            if (inst == null)
                return null;

            return new Account_Summary(inst.AccountRef, inst.AvailableFunds, inst.GrossPositionValue, inst.NetLiquidation);
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
            int accountref = (data.ContainsKey("accountref") ? data.GetValue("accountref").Value<int>() : 0);
            float availablefunds = (data.ContainsKey("availablefunds") ? data.GetValue("availablefunds").Value<float>() : 0);
            float grosspositionvalue = (data.ContainsKey("grosspositionvalue") ? data.GetValue("grosspositionvalue").Value<float>() : 0);
            float netliquidation = (data.ContainsKey("netliquidation") ? data.GetValue("netliquidation").Value<float>() : 0);

            // Add instance to DB
            var inst = Account_SummaryHelper_db.Add(accountref, availablefunds, grosspositionvalue, netliquidation, context, out StatusResponse statusResponse);

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
            var dbInst = Account_SummaryHelper_db.getCollection(context, out StatusResponse statusResponse);

            // Convert to object
            var inst = dbInst?.Select(x => Account_SummaryHelper.Convert(x)).ToList();

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
