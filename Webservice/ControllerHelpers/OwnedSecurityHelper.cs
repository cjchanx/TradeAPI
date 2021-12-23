using DatabaseLibrary.Models;
using DatabaseLibrary.Core;
using DatabaseLibrary.Helpers;
using TradingLibrary.Models;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Webservice.ControllerHelpers
{
    public class OwnedSecurityHelper
    {
        #region Converters

        /// <summary>
        ///
        /// </summary>
        /// <param name="inst"></param>
        /// <returns>null if inst was null</returns>
        public static OwnedSecurity Convert(OwnedSecurity_db inst)
        {
            if (inst == null)
                return null;

            return new OwnedSecurity(inst.AccountRef, inst.Symbol, inst.Quantity, inst.AveragePrice);
        }

        #endregion

        // Methods
        public static ResponseMessage Add(JObject data, DBContext context, out HttpStatusCode stat, bool includeDetails = false)
        {
            // Extract parameters
            int accountref = (data.ContainsKey("accountref") ? data.GetValue("accountref").Value<int>() : 0);
            string symbol = (data.ContainsKey("symbol") ? data.GetValue("symbol").Value<string>() : null);
            int quantity = (data.ContainsKey("quantity") ? data.GetValue("quantity").Value<int>() : 0);
            double averageprice = (data.ContainsKey("averageprice") ? data.GetValue("averageprice").Value<double>() : 0);


            // Add instance to DB
            var inst = OwnedSecurityHelper_db.Add(accountref, symbol, quantity, averageprice, context, out StatusResponse statusResponse);

            // Process includeErrors
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError && !includeDetails)
            {
                statusResponse.Message = "Error occured adding new owned security.";
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
            var dbInst = OwnedSecurityHelper_db.GetCollection(context, out StatusResponse statusResponse);

            // Convert to object
            var inst = dbInst?.Select(x => Convert(x)).ToList();

            // Process includeErrors
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError && !includeDetailsErrors)
            {
                statusResponse.Message = "Something went wrong while retrieving the owned securities.";
            }

            // Return response
            var response = new ResponseMessage(inst != null, statusResponse.Message, inst);
            stat = statusResponse.StatusCode;
            return response;
        }

        public static ResponseMessage GetCollectionByAccount(int id, DBContext context, out HttpStatusCode stat, bool includeDetailsErrors = false)
        {
            // Get instances from DB
            var dbInst = OwnedSecurityHelper_db.GetOwnedByAccount(id, context, out StatusResponse statusResponse);

            // Convert to object
            var inst = dbInst?.Select(x => Convert(x)).ToList();

            // Process includeErrors
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError && !includeDetailsErrors)
            {
                statusResponse.Message = "Something went wrong while retrieving the owned securities.";
            }

            // Return response
            var response = new ResponseMessage(inst != null, statusResponse.Message, inst);
            stat = statusResponse.StatusCode;
            return response;
        }

        public static ResponseMessage Remove(int accountid, string symbol, DBContext context, out HttpStatusCode stat, bool includeDetailsErrors = false)
        {
            // Remove instance from DB
            var inst = OwnedSecurityHelper_db.Remove(accountid, symbol, context, out StatusResponse statusResponse);

            if (inst == 0)
            {
                statusResponse.Message = "Error occured removing owned security.";
            }

            // Process includeErrors
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError && !includeDetailsErrors)
            {
                statusResponse.Message = "Error occured removing owned security.";
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
    }
}
