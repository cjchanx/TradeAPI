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
    public class SecurityHelper_db
    {
        public static Security_db Add(string symbol, string description, double price, DBContext context, out StatusResponse response)
        {
            try
            {
                // Validate current data
                if (string.IsNullOrEmpty(symbol?.Trim()))
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid symbol entered.");
                if (string.IsNullOrEmpty(description?.Trim()))
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid description entered.");

                // Create instance
                Security_db inst = new Security_db(
                    symbol,
                    description,
                    price
                    );

                // Attempt to add to database
                int rowsAffected = context.ExecuteNonQueryCommand(
                    commandText: "INSERT INTO Security (Symbol, Description, Price) VALUES (@symbol, @description, @price)",
                    parameters: new Dictionary<string, object> {
                        {"@symbol", inst.Symbol },
                        {"@description", inst.Description },
                        {"@price", inst.Price }
                    },
                    message: out string message
                );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return
                response = new StatusResponse("Security added successfully");
                return inst;
            }
            catch (Exception ex)
            {
                // Error occured.
                response = new StatusResponse(ex);
                return null;
            }
        }

        /// <summary>
        /// Updates a security based on the SYMBOL for it's DESCRIPTION and PRICE
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <param name="context"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static Security_db Update(string symbol, string description, double price, DBContext context, out StatusResponse response)
        {
            try
            {
                // Validate current data
                if (string.IsNullOrEmpty(symbol?.Trim()))
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid symbol entered.");
                if (string.IsNullOrEmpty(description?.Trim()))
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid description entered.");

                // Create instance
                Security_db inst = new Security_db(
                    symbol,
                    description,
                    price
                    );

                // Attempt to add to database
                int rowsAffected = context.ExecuteNonQueryCommand(
                    commandText: "UPDATE `Security` SET `Description`=@description,`Price`=@price WHERE `Symbol`=@symbol",
                    parameters: new Dictionary<string, object> {
                        {"@symbol", inst.Symbol },
                        {"@description", inst.Description },
                        {"@price", inst.Price }
                    },
                    message: out string message
                );
                if (rowsAffected == -1 || rowsAffected == 0)
                    throw new Exception(message);

                // Return
                response = new StatusResponse("Security updated successfully");
                return inst;
            }
            catch (Exception ex)
            {
                // Error occured.
                response = new StatusResponse(ex);
                return null;
            }
        }

        /// <summary>
        /// Updates a security based on the SYMBOL for it's PRICE
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <param name="context"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static Security_db UpdatePrice(string symbol, double price, DBContext context, out StatusResponse response)
        {
            try
            {
                // Validate current data
                if (string.IsNullOrEmpty(symbol?.Trim()))
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid symbol entered.");

                // Create instance
                Security_db inst = new Security_db(
                    symbol,
                    null,
                    price
                    );

                // Attempt to add to database
                int rowsAffected = context.ExecuteNonQueryCommand(
                    commandText: "UPDATE `Security` SET `Price`=@price WHERE `Symbol`=@symbol",
                    parameters: new Dictionary<string, object> {
                        {"@symbol", inst.Symbol },
                        {"@price", inst.Price }
                    },
                    message: out string message
                );
                if (rowsAffected == -1 || rowsAffected == 0)
                    throw new Exception(message);

                // Return
                response = new StatusResponse("Security updated successfully");
                return inst;
            }
            catch (Exception ex)
            {
                // Error occured.
                response = new StatusResponse(ex);
                return null;
            }
        }

        public static List<Security_db> GetCollection(DBContext context, out StatusResponse response)
        {
            try
            {
                // Attempt to get from the database
                DataTable table = context.ExecuteDataQueryCommand(
                    commandText: "SELECT * FROM Security",
                    parameters: new Dictionary<string, object>
                    {

                    },
                    message: out string message
                );

                if (table == null)
                    throw new Exception(message);

                // Parse
                List<Security_db> inst = new List<Security_db>();
                foreach (DataRow row in table.Rows)
                {
                    inst.Add(new Security_db(
                        Symbol: row["symbol"].ToString(),
                        Description: row["description"].ToString(),
                        Price: double.Parse(row["price"].ToString())
                        )
                    );
                }

                // Return
                response = new StatusResponse("Security successfully retreived.");
                return inst;
            }
            catch (Exception ex)
            {
                response = new StatusResponse(ex);
                return null;
            }
        }

        public static List<Security_db> GetPrice(string symbol, DBContext context, out StatusResponse response)
        {
            try
            {
                if(symbol == null)
                {
                    response = new StatusResponse("Symbol invalid.");
                    return null;
                }
                // Attempt to get from the database
                DataTable table = context.ExecuteDataQueryCommand(
                    commandText: "SELECT `Price`, `Description` FROM Security WHERE `Symbol`=@symbol",
                    parameters: new Dictionary<string, object>
                    {
                        {"@symbol", symbol }
                    },
                    message: out string message
                );

                if (table == null)
                    throw new Exception(message);

                // Parse
                List<Security_db> inst = new List<Security_db>();
                foreach (DataRow row in table.Rows)
                {
                    inst.Add(new Security_db(
                        Symbol: symbol,
                        Description: row["description"].ToString(),
                        Price: double.Parse(row["price"].ToString())
                        )
                    ); 
                }

                // Return
                response = new StatusResponse("Security successfully retreived.");
                return inst;
            }
            catch (Exception ex)
            {
                response = new StatusResponse(ex);
                return null;
            }
        }

        public static List<Security_db> getCollection(DBContext context)
        {
            try
            {
                // Attempt to get from the database
                DataTable table = context.ExecuteDataQueryCommand(
                    commandText: "SELECT * FROM Security",
                    parameters: new Dictionary<string, object>
                    {

                    },
                    message: out string message
                );

                if (table == null)
                    throw new Exception(message);

                // Parse
                List<Security_db> inst = new List<Security_db>();
                foreach (DataRow row in table.Rows)
                {
                    inst.Add(new Security_db(
                        Symbol: row["symbol"].ToString(),
                        Description: row["description"].ToString(),
                        Price: double.Parse(row["price"].ToString())
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

        public static int Remove(string symbol, DBContext context, out StatusResponse response)
        {
            try
            {
                // Validate current data
                if (string.IsNullOrEmpty(symbol?.Trim()))
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid symbol entered.");

                // Create instance
                Security_db inst = new Security_db(
                    symbol,
                    null,
                    0
                    );

                // Attempt to add to database
                int rowsAffected = context.ExecuteNonQueryCommand(
                    commandText: "DELETE FROM `Security` WHERE Symbol=@symbol",
                    parameters: new Dictionary<string, object> {
                        {"@symbol", inst.Symbol },
                    },
                    message: out string message
                );

                if (rowsAffected == -1)
                    throw new Exception(message);
                if (rowsAffected == 0)
                {
                    response = new StatusResponse("Deletion unsucessful.");
                    throw new Exception(message);
                }

                // Return
                response = new StatusResponse("Security removed successfully");
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
