using DatabaseLibrary.Models;
using DatabaseLibrary.Core;
using DatabaseLibrary.Helpers;
using TradingLibrary.Models;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Webservice.ControllerHelpers
{
    public class CommissionsHelper
    {
        public static Commission Convert(Commission_db inst)
        {
            if (inst == null)
                return null;

            return new Commission(inst.Broker, inst.Type, inst.Rate);
        }

        public static ResponseMessage Add(JObject data, DBContext context, out HttpStatusCode stat, bool includeDetails = false)
        {
            // Extract parameters
            string name = (data.ContainsKey("broker") ? data.GetValue("broker").Value<string>() : null);
            int type = (data.ContainsKey("type") ? data.GetValue("type").Value<int>() : 0);
            double rate = (data.ContainsKey("rate") ? data.GetValue("rate").Value<double>() : 0);


            // Add instance to DB
            var inst = CommissionsHelper_db.Add(name, type, rate, context, out StatusResponse statusResponse);

            // Process includeErrors
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError && !includeDetails)
            {
                statusResponse.Message = "Error occured adding new broker.";
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
            var dbInst = CommissionsHelper_db.GetCollection(context, out StatusResponse statusResponse);

            // Convert to object
            var inst = dbInst?.Select(x => CommissionsHelper.Convert(x)).ToList();

            // Process includeErrors
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError && !includeDetailsErrors)
            {
                statusResponse.Message = "Something went wrong while retrieving the Commission.";
            }

            // Return response
            var response = new ResponseMessage(inst != null, statusResponse.Message, inst);
            stat = statusResponse.StatusCode;
            return response;
        }

        public static ResponseMessage Remove(string name, int type, DBContext context, out HttpStatusCode stat, bool includeDetailsErrors = false)
        {
            // Remove instance from DB
            var inst = CommissionsHelper_db.Remove(name, type, context, out StatusResponse statusResponse);

            if (inst == 0)
            {
                statusResponse.Message = "Error occured removing broker.";
            }

            // Process includeErrors
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError && !includeDetailsErrors)
            {
                statusResponse.Message = "Error occured removing broker.";
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

        public static ResponseMessage Remove(string name, DBContext context, out HttpStatusCode stat, bool includeDetailsErrors = false)
        {
            // Remove instance from DB
            var inst = CommissionsHelper_db.RemoveByBroker(name, context, out StatusResponse statusResponse);

            if (inst == 0)
            {
                statusResponse.Message = "Error occured removing broker.";
            }

            // Process includeErrors
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError && !includeDetailsErrors)
            {
                statusResponse.Message = "Error occured removing broker.";
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
