using DatabaseLibrary.Models;
using DatabaseLibrary.Core;
using DatabaseLibrary.Helpers;
using TradingLibrary.Models;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Webservice.ControllerHelpers
{
    public class AccountHelper
    {
        #region Converters

        /// <summary>
        /// Returns a Account from an instance of an Account_db
        /// </summary>
        /// <param name="inst"></param>
        /// <returns>null if inst was null</returns>
        public static Account Convert(Account_db inst)
        {
            if(inst == null)
                return null;

            return new Account(inst.Active, inst.Broker, inst.Date, inst.Name);
        }

        #endregion

        // Methods
        /// <summary>
        /// Sets up a new account based on the given object data.
        /// </summary>
        /// <param name="includeDetails">Whether to include detailed internal server message.</param>
        /// <returns></returns>
        public static ResponseMessage Add(JObject data ,DBContext context, out HttpStatusCode stat, bool includeDetails = false)
        {
            // Extract parameters
            bool active = (data.ContainsKey("active") ? data.GetValue("active").Value<bool>() : false);
            string broker = (data.ContainsKey("broker") ? data.GetValue("broker").Value<string>() : null);
            DateTime date = (data.ContainsKey("date") ? data.GetValue("date").Value<DateTime>() : DateTime.UnixEpoch);
            string name = (data.ContainsKey("name") ? data.GetValue("name").Value<string>() : null);
            string desc = (data.ContainsKey("description") ? data.GetValue("description").Value<string>(): null);

            // Add instance to DB
            var inst = AccountHelper_db.Add(broker, name, desc, context, out StatusResponse statusResponse);

            // Process includeErrors
            if(statusResponse.StatusCode == HttpStatusCode.InternalServerError && !includeDetails)
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
    }
}
