using DatabaseLibrary.Models;
using DatabaseLibrary.Core;
using DatabaseLibrary.Helpers;
using TradingLibrary.Models;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Webservice.ControllerHelpers
{
    public class SecurityHelper
    {

        public static Security Convert(Security_db inst)
        {
            if (inst == null)
                return null;

            return new Security(inst.Symbol, inst.Description, inst.Price);
        }

        public static ResponseMessage Add(JObject data, DBContext context, out HttpStatusCode stat, bool includeDetails = false)
        {
            // Extract parameters
            string symbol = (data.ContainsKey("symbol") ? data.GetValue("symbol").Value<string>() : null);
            string description = (data.ContainsKey("description") ? data.GetValue("description").Value<string>() : null);
            double price = (data.ContainsKey("price") ? data.GetValue("price").Value<double>() : 0);


            // Add instance to DB
            var inst = SecurityHelper_db.Add(symbol, description, price, context, out StatusResponse statusResponse);

            // Process includeErrors
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError && !includeDetails)
            {
                statusResponse.Message = "Error occured adding new Security.";
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
            var dbInst = SecurityHelper_db.GetCollection(context, out StatusResponse statusResponse);

            // Convert to object
            var inst = dbInst?.Select(x => SecurityHelper.Convert(x)).ToList();

            // Process includeErrors
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError && !includeDetailsErrors)
            {
                statusResponse.Message = "Something went wrong while retrieving the Security.";
            }

            // Return response
            var response = new ResponseMessage(inst != null, statusResponse.Message, inst);
            stat = statusResponse.StatusCode;
            return response;
        }

        public static ResponseMessage Remove(string symbol, DBContext context, out HttpStatusCode stat, bool includeDetailsErrors = false)
        {
            // Remove instance from DB
            var inst = SecurityHelper_db.Remove(symbol, context, out StatusResponse statusResponse);

            if (inst == 0)
            {
                statusResponse.Message = "Error occured removing security.";
            }

            // Process includeErrors
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError && !includeDetailsErrors)
            {
                statusResponse.Message = "Error occured removing security.";
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

        public static ResponseMessage UpdateFull(JObject data, DBContext context, out HttpStatusCode stat, bool includeDetails = false)
        {
            // Extract parameters
            string symbol = (data.ContainsKey("symbol") ? data.GetValue("symbol").Value<string>() : null);
            string description = (data.ContainsKey("description") ? data.GetValue("description").Value<string>() : null);
            double price = (data.ContainsKey("price") ? data.GetValue("price").Value<double>() : 0);


            // Add instance to DB
            var inst = SecurityHelper_db.Update(symbol, description, price, context, out StatusResponse statusResponse);

            // Process includeErrors
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError && !includeDetails)
            {
                statusResponse.Message = "Error occured updating security.";
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

        public static ResponseMessage UpdatePrice(JObject data, DBContext context, out HttpStatusCode stat, bool includeDetails = false)
        {
            // Extract parameters
            string symbol = (data.ContainsKey("symbol") ? data.GetValue("symbol").Value<string>() : null);
            double price = (data.ContainsKey("price") ? data.GetValue("price").Value<double>() : 0);


            // Add instance to DB
            var inst = SecurityHelper_db.UpdatePrice(symbol, price, context, out StatusResponse statusResponse);

            // Process includeErrors
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError && !includeDetails)
            {
                statusResponse.Message = "Error occured updating security.";
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

        public static ResponseMessage GetPrice(string symbol, DBContext context, out HttpStatusCode stat, bool includeDetailsErrors = false)
        {
            // Get instances from DB
            var dbInst = SecurityHelper_db.GetPrice(symbol, context, out StatusResponse statusResponse);

            // Convert to object
            var inst = dbInst?.Select(x => SecurityHelper.Convert(x)).ToList();

            // Process includeErrors
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError && !includeDetailsErrors)
            {
                statusResponse.Message = "Something went wrong while retrieving the Security.";
            }

            // Return response
            var response = new ResponseMessage(inst != null, statusResponse.Message, inst);
            stat = statusResponse.StatusCode;
            return response;
        }
    }
}
