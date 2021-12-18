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
        public static Security_db Add(string symbol, string description, float price, DBContext context, out StatusResponse response)
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
                        Price: float.Parse(row["price"].ToString())
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
                        Price: float.Parse(row["price"].ToString())
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
    }

}
