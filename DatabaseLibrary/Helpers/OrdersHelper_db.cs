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
    public class OrdersHelper_db
    {

        /// <summary>
        /// Add adds a new account entry into the database, assuming that it is active and using the current UTC time.
        /// </summary>
        /// <returns>Account_db object</returns>
        public static Orders_db Add(int Id, int AccountRef, int Action, DateTime DateCreated, int Quantity, int Status, string Symbol, DBContext context, out StatusResponse response)
        {
            try
            {
                // Validate current data
                

                // Create instance
                Orders_db inst = new Orders_db(
                    Id,
                    AccountRef,
                    Action,
                    DateCreated,
                    Quantity,
                    Status,
                    Symbol
                    );

                // Attempt to add to database
                int rowsAffected = context.ExecuteNonQueryCommand(
                    commandText: "INSERT INTO ORDERS (Id, AccountRef, Action, DateCreated, Quantity, Status, Symbol) VALUES (@id, @accountref, @action, @datecreated, @quantity, @status, @symbol)",
                    parameters: new Dictionary<string, object> {
                        {"@id", inst.Id },
                        {"@accountref", inst.AccountRef },
                        {"@action", inst.Action },
                        {"@datecreated", inst.DateCreated },
                        {"@quantity", inst.Quantity },
                        {"@status", inst.Status },
                        {"@symbol", inst.Symbol }
                    },
                    message: out string message
                );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return
                response = new StatusResponse("Order added successfully");
                return inst;
            }
            catch (Exception ex)
            {
                // Error occured.
                response = new StatusResponse(ex);
                return null;
            }
        }


        public static List<Orders_db> getCollection(DBContext context, out StatusResponse response)
        {
            try
            {
                // Attempt to get from the database
                DataTable table = context.ExecuteDataQueryCommand(
                    commandText: "SELECT * FROM ORDERS",
                    parameters: new Dictionary<string, object>
                    {

                    },
                    message: out string message
                );

                if (table == null)
                    throw new Exception(message);

                // Parse
                List<Orders_db> inst = new List<Orders_db>();
                foreach (DataRow row in table.Rows)
                {
                    inst.Add(new Orders_db(
                        Id: int.Parse(row["id"].ToString()),
                        AccountRef: int.Parse(row["accountref"].ToString()),
                        Action: int.Parse(row["action"].ToString()),
                        DateCreated: DateTime.Parse(row["datecreated"].ToString()),
                        Quantity: int.Parse(row["quantity"].ToString()),
                        Status: int.Parse(row["status"].ToString()),
                        Symbol: row["symbol"].ToString()
                        )
                    );
                }

                // Return
                response = new StatusResponse("Accounts successfully retreived.");
                return inst;
            }
            catch (Exception ex)
            {
                response = new StatusResponse(ex);
                return null;
            }
        }
    }
}
