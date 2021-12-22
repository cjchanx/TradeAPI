using DatabaseLibrary.Models;
using DatabaseLibrary.Core;
using DatabaseLibrary.Helpers;
using TradingLibrary.Models;
using System.Net;
using Newtonsoft.Json.Linq;


namespace Webservice.ControllerHelpers
{
    public class AccountsHelper
    {
        #region Converters

        /// <summary>
        /// Returns a Account from an instance of an Account_db
        /// </summary>
        /// <param name="inst"></param>
        /// <returns>null if inst was null</returns>
        public static Accounts Convert(Accounts_db inst)
        {
            if (inst == null)
                return null;

            return new Accounts(inst.Id, inst.Active, inst.Broker, inst.Date, inst.Name, inst.Description, inst.Password);
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
            int id = (data.ContainsKey("id") ? data.GetValue("id").Value<int>() : 0);
            bool active = (data.ContainsKey("active") ? data.GetValue("active").Value<bool>() : false);
            string broker = (data.ContainsKey("broker") ? data.GetValue("broker").Value<string>() : null);
            DateTime date = (data.ContainsKey("datecreated") ? data.GetValue("datecreated").Value<DateTime>() : DateTime.UnixEpoch);
            string name = (data.ContainsKey("name") ? data.GetValue("name").Value<string>() : null);
            string desc = (data.ContainsKey("description") ? data.GetValue("description").Value<string>() : null);
            string pass = (data.ContainsKey("password") ? data.GetValue("password").Value<string>() : null);

            // Add instance to DB
            var inst = AccountsHelper_db.Add(id, broker, name, desc, pass, context, out StatusResponse statusResponse);

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

        // Methods
        /// <summary>
        /// Updates an existing account based on the given object data
        /// </summary>
        /// <param name="includeDetails">Whether to include detailed internal server message.</param>
        /// <returns></returns>
        public static ResponseMessage Update(JObject data, DBContext context, out HttpStatusCode stat, bool includeDetails = false)
        {
            // Extract parameters
            int id = (data.ContainsKey("id") ? data.GetValue("id").Value<int>() : 0);
            bool active = (data.ContainsKey("active") ? data.GetValue("active").Value<bool>() : false);
            string broker = (data.ContainsKey("broker") ? data.GetValue("broker").Value<string>() : null);
            DateTime date = (data.ContainsKey("datecreated") ? data.GetValue("datecreated").Value<DateTime>() : DateTime.UnixEpoch);
            string name = (data.ContainsKey("name") ? data.GetValue("name").Value<string>() : null);
            string desc = (data.ContainsKey("description") ? data.GetValue("description").Value<string>() : null);
            string pass = (data.ContainsKey("password") ? data.GetValue("password").Value<string>() : null);

            // Add instance to DB
            var inst = AccountsHelper_db.Update(id, broker, active, name, desc, pass, context, out StatusResponse statusResponse);

            // Process includeErrors
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError && !includeDetails)
            {
                statusResponse.Message = "Error occured updating account.";
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
            Console.WriteLine("Something went wrong while retrieving the Account. 1");
            // Get instances from DB
            var dbInst = AccountsHelper_db.getCollection(context, out StatusResponse statusResponse);

            // Convert to object
            var inst = dbInst?.Select(x => AccountsHelper.Convert(x)).ToList();

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

        public static ResponseMessage GetAccountById(int id, DBContext context, out HttpStatusCode stat, bool includeDetailsErrors = false)
        {
            Console.WriteLine("Something went wrong while retrieving the Account.");
            // Get instances from DB
            var dbInst = AccountsHelper_db.getAccountById(id, context, out StatusResponse statusResponse);

            // Convert to object
            var inst = dbInst?.Select(x => AccountsHelper.Convert(x)).ToList();

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
