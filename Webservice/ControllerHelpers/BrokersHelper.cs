using DatabaseLibrary.Models;
using DatabaseLibrary.Core;
using DatabaseLibrary.Helpers;
using TradingLibrary.Models;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Webservice.ControllerHelpers
{
    public class BrokersHelper
    {

        public static Brokers Convert(Brokers_db inst)
        {
            if (inst == null)
                return null;

            return new Brokers(inst.Name, inst.Website);
        }

        public static ResponseMessage Add(JObject data, DBContext context, out HttpStatusCode stat, bool includeDetails = false)
        {
            // Extract parameters
            string name = (data.ContainsKey("name") ? data.GetValue("name").Value<string>() : null);
            string website = (data.ContainsKey("website") ? data.GetValue("website").Value<string>() : null);


            // Add instance to DB
            var inst = BrokersHelper_db.Add(name, website, context, out StatusResponse statusResponse);

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
            var dbInst = BrokersHelper_db.getCollection(context, out StatusResponse statusResponse);

            // Convert to object
            var inst = dbInst?.Select(x => BrokersHelper.Convert(x)).ToList();

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
