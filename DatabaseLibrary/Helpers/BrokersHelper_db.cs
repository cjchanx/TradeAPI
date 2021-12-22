using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLibrary.Core;
using DatabaseLibrary.Models;
using System.Data;
using TradingLibrary.Models;

namespace DatabaseLibrary.Helpers
{
    public class BrokersHelper_db
    {
        public static Brokers_db Add(string name, string website, DBContext context, out StatusResponse response)
        {
            try
            {
                // Validate current data
                if (string.IsNullOrEmpty(name?.Trim()))
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid name entered.");
                if (string.IsNullOrEmpty(website?.Trim()))
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid website entered.");

                // Create instance
                Brokers_db inst = new Brokers_db(
                    name,
                    website
                    );

                // Attempt to add to database
                int rowsAffected = context.ExecuteNonQueryCommand(
                    commandText: "INSERT INTO Brokers (Name, Website) VALUES (@name, @website)",
                    parameters: new Dictionary<string, object> {
                        {"@name", inst.Name },
                        {"@website", inst.Website }
                    },
                    message: out string message
                );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return
                response = new StatusResponse("Broker added successfully");
                return inst;
            }
            catch (Exception ex)
            {
                // Error occured.
                response = new StatusResponse(ex);
                return null;
            }
        }

        public static List<Brokers_db> getCollection(DBContext context, out StatusResponse response)
        {
            try
            {
                // Attempt to get from the database
                DataTable table = context.ExecuteDataQueryCommand(
                    commandText: "SELECT * FROM Brokers",
                    parameters: new Dictionary<string, object>
                    {

                    },
                    message: out string message
                );

                if (table == null)
                    throw new Exception(message);

                // Parse
                List<Brokers_db> inst = new List<Brokers_db>();
                foreach (DataRow row in table.Rows)
                {
                    inst.Add(new Brokers_db(
                        name: row["name"].ToString(),
                        website: row["website"].ToString()
                        )
                    );
                }

                // Return
                response = new StatusResponse("Brokers successfully retreived.");
                return inst;
            }
            catch (Exception ex)
            {
                response = new StatusResponse(ex);
                return null;
            }
        }

        public static List<Brokers_db> getCollection(DBContext context)
        {
            try
            {
                // Attempt to get from the database
                DataTable table = context.ExecuteDataQueryCommand(
                    commandText: "SELECT * FROM Brokers",
                    parameters: new Dictionary<string, object>
                    {

                    },
                    message: out string message
                );

                if (table == null)
                    throw new Exception(message);

                // Parse
                List<Brokers_db> inst = new List<Brokers_db>();
                foreach (DataRow row in table.Rows)
                {
                    inst.Add(new Brokers_db(
                        name: row["name"].ToString(),
                        website: row["website"].ToString()
                        )
                    );
                }

                // Return
                return inst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Attempts to remove relevant Broker by the Broker Name
        /// </summary>
        /// <param name="context"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static int Remove(string name, DBContext context, out StatusResponse response)
        {
            try
            {
                Console.WriteLine("Removing " + name);
                // Attempt to remove from database
                int rowsAffected = context.ExecuteNonQueryCommand(
                    commandText: "DELETE FROM `Brokers` WHERE Name=@name",
                    parameters: new Dictionary<string, object> {
                        {"@name", name },
                    },
                    message: out string message
                );

                Console.WriteLine("Rows Affected = " + rowsAffected);

                if (rowsAffected == -1)
                    throw new Exception(message);
                if (rowsAffected == 0)
                {
                    response = new StatusResponse("Deletion unsucessful.");
                    throw new Exception(message);
                }

                // Return
                response = new StatusResponse("Deleted entry successfully.");
                return rowsAffected;
            }
            catch (Exception ex)
            {
                // Error occured.
                response = new StatusResponse(ex);
                return 0;
            }
        }
    }
}
