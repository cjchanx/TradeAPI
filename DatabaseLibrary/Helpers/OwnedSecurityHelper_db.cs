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
    public class OwnedSecurityHelper_db
    {
        public static OwnedSecurity_db Add(int accountRef, string symbol, int quantity, double avg, DBContext context, out StatusResponse response)
        {
            try
            {
                // Validate data
                if (string.IsNullOrEmpty(symbol?.Trim()))
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid symbol entered.");

                // Create instance
                OwnedSecurity_db inst = new OwnedSecurity_db(
                    accountRef,
                    symbol,
                    quantity,
                    avg
                );

                // Attempt to add to database
                int rowsAffected = context.ExecuteNonQueryCommand(
                    commandText: "INSERT INTO `OwnedSecurity`(`AccountRef`, `Symbol`, `Quantity`, `AveragePrice`) VALUES (@ref,@sym,@qty,@avg)",
                    parameters: new Dictionary<string, object> {
                        {"@ref", inst.AccountRef},
                        {"@sym", inst.Symbol },
                        {"@avg", inst.AveragePrice },
                        {"@qty", inst.Quantity }
                    },
                    message: out string message
                );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return
                response = new StatusResponse("Owned security added successfully.");
                return inst;
            }
            catch (Exception ex)
            {
                // Error occured.
                response = new StatusResponse(ex);
                return null;
            }
        }

        public static List<OwnedSecurity_db> GetCollection(DBContext context, out StatusResponse response)
        {
            try
            {
                // Attempt to get from the database
                DataTable table = context.ExecuteDataQueryCommand(
                    commandText: "SELECT * FROM OwnedSecurity",
                    parameters: new Dictionary<string, object>
                    {

                    },
                    message: out string message
                );

                if (table == null)
                    throw new Exception(message);

                // Parse
                List<OwnedSecurity_db> inst = new List<OwnedSecurity_db>();
                foreach (DataRow row in table.Rows)
                {
                    inst.Add(new OwnedSecurity_db(
                        AccountRef: int.Parse(row["accountref"].ToString()),
                        Symbol: row["symbol"].ToString(),
                        Quantity: int.Parse(row["quantity"].ToString()),
                        AveragePrice: double.Parse(row["averageprice"].ToString())
                        )
                    );
                }

                // Return
                response = new StatusResponse("All OwnedSecurities successfully retrieved.");
                return inst;
            }
            catch (Exception ex)
            {
                response = new StatusResponse(ex);
                return null;
            }
        }

        public static List<OwnedSecurity_db> GetOwnedByAccount(int AccountRef, DBContext context, out StatusResponse response)
        {
            try
            {
                // Attempt to get from the database
                DataTable table = context.ExecuteDataQueryCommand(
                    commandText: "SELECT * FROM OwnedSecurity WHERE AccountRef=@accountref",
                    parameters: new Dictionary<string, object>
                    {
                        {"@accountref", AccountRef }
                    },
                    message: out string message
                );

                if (table == null)
                    throw new Exception(message);

                // Parse
                List<OwnedSecurity_db> inst = new List<OwnedSecurity_db>();
                foreach (DataRow row in table.Rows)
                {
                    inst.Add(new OwnedSecurity_db(
                        AccountRef: int.Parse(row["accountref"].ToString()),
                        Symbol: row["symbol"].ToString(),
                        Quantity: int.Parse(row["quantity"].ToString()),
                        AveragePrice: double.Parse(row["averageprice"].ToString())
                        )
                    );
                }

                // Return
                response = new StatusResponse("All OwnedSecurities successfully retrieved.");
                return inst;
            }
            catch (Exception ex)
            {
                response = new StatusResponse(ex);
                return null;
            }
        }

        public static List<OwnedSecurity_db> GetOwnedByAccount(int AccountRef, DBContext context)
        {
            try
            {
                // Attempt to get from the database
                DataTable table = context.ExecuteDataQueryCommand(
                    commandText: "SELECT * FROM OwnedSecurity WHERE AccountRef=@accountref",
                    parameters: new Dictionary<string, object>
                    {
                        {"@accountref", AccountRef }
                    },
                    message: out string message
                );

                if (table == null)
                    throw new Exception(message);

                // Parse
                List<OwnedSecurity_db> inst = new List<OwnedSecurity_db>();
                foreach (DataRow row in table.Rows)
                {
                    inst.Add(new OwnedSecurity_db(
                        AccountRef: int.Parse(row["accountref"].ToString()),
                        Symbol: row["symbol"].ToString(),
                        Quantity: int.Parse(row["quantity"].ToString()),
                        AveragePrice: double.Parse(row["averageprice"].ToString())
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
        /// Remove removes the relevant OwnedSecurity from the database by Id and symbol. Returns rowsAffected.
        /// </summary>
        /// <returns></returns>
        public static int Remove(int Id, string Symbol, DBContext context, out StatusResponse response)
        {
            try
            {
                // Attempt to remove from database
                int rowsAffected = context.ExecuteNonQueryCommand(
                    commandText: "DELETE FROM `OwnedSecurity` WHERE AccountRef=@Id AND Symbol=@symbol",
                    parameters: new Dictionary<string, object> {
                        {"@Id", Id },
                        {"@symbol", Symbol}
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
