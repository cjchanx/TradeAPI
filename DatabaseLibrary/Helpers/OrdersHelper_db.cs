using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLibrary.Core;
using DatabaseLibrary.Models;
using System.Data;
using TradingLibrary.Models;
using DatabaseLibrary.Helpers;
using TradingLibrary.Enums;

namespace DatabaseLibrary.Helpers
{
    public class OrdersHelper_db
    {

      
        /// <summary>
        /// Add adds a new account entry into the database, assuming that it is active and using the current UTC time.
        /// </summary>
        /// <returns>Account_db object</returns>
        public static Orders_db Add(int AccountRef, int Action, double TargetPrice, DateTime DateCreated, int Quantity, int Status, string Symbol, string Broker, DBContext context, out StatusResponse response)
        {
            try
            {
                // Validate current data
                Console.WriteLine("STUFF!");

                // Create instance
                Orders_db inst = new Orders_db(
                    -1,
                    AccountRef,
                    Action,
                    TargetPrice,
                    DateCreated,
                    Quantity,
                    Status,
                    Symbol,
                    Broker
                    );

                // Attempt to add to database
                int rowsAffected = context.ExecuteNonQueryCommand(
                    commandText: "INSERT INTO Orders (Id, AccountRef, Action, TargetPrice, DateCreated, Quantity, Status, Symbol, BrokerName) VALUES (@Id, @accountref, @action, @targetprice, @datecreated, @quantity, @status, @symbol, @broker)",
                    parameters: new Dictionary<string, object> {
                        {"@Id", inst.Id},
                        {"@accountref", inst.AccountRef },
                        {"@action", inst.Action },
                        {"@targetprice", inst.TargetPrice },
                        {"@datecreated", inst.DateCreated },
                        {"@quantity", inst.Quantity },
                        {"@status", inst.Status },
                        {"@symbol", inst.Symbol },
                        {"@broker", inst.Broker }
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

        /// <summary>
        /// Remove removes the relevant Order from the database by Id, returns rowsAffected.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="context"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static int Remove(int Id, DBContext context, out StatusResponse response)
        {
            Console.WriteLine("Running REMOVE.");
            try
            { 
                // Attempt to remove from database
                int rowsAffected = context.ExecuteNonQueryCommand(
                    commandText: "DELETE FROM `Orders` WHERE Id = @Id",
                    parameters: new Dictionary<string, object> {
                        {"@Id", Id },
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


        public static List<Orders_db> getCollection(DBContext context, out StatusResponse response)
        {
            try
            {
                // Attempt to get from the database
                DataTable table = context.ExecuteDataQueryCommand(
                    commandText: "SELECT * FROM Orders",
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
                        TargetPrice: double.Parse(row["targetprice"].ToString()),
                        DateCreated: DateTime.Parse(row["datecreated"].ToString()),
                        Quantity: int.Parse(row["quantity"].ToString()),
                        Status: int.Parse(row["status"].ToString()),
                        Symbol: row["symbol"].ToString(),
                        Broker: row["brokername"].ToString()
                        )
                    );
                }

                // Return
                response = new StatusResponse("Orders successfully retreived.");
                return inst;
            }
            catch (Exception ex)
            {
                response = new StatusResponse(ex);
                return null;
            }
        }

        public static List<Orders_db> getCollection(DBContext context)
        {
            try
            {
                // Attempt to get from the database
                DataTable table = context.ExecuteDataQueryCommand(
                    commandText: "SELECT * FROM Orders",
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
                        TargetPrice: double.Parse(row["targetprice"].ToString()),
                        DateCreated: DateTime.Parse(row["datecreated"].ToString()),
                        Quantity: int.Parse(row["quantity"].ToString()),
                        Status: int.Parse(row["status"].ToString()),
                        Symbol: row["symbol"].ToString(),
                        Broker: row["brokername"].ToString()
                        )
                    );
                }

                // Return
                return inst;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// UpdateOrders processes an update on the database for every order that had the security price
        /// reach its fulfilled price.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static int UpdateOrders(DBContext context, out StatusResponse response)
        {
            try
            {
                // Get all entries from the DB for orders which need to be fulfilled
                DataTable table = context.ExecuteDataQueryCommand(
                    commandText: "SELECT * FROM Orders AS O, Security AS S WHERE(O.Symbol = S.Symbol AND(O.ACTION = 0 OR O.ACTION = 2 AND O.TargetPrice <= S.Price) OR (O.Symbol = S.Symbol AND(O.ACTION = 1 OR O.ACTION = 3) AND O.TargetPrice >= S.Price)",
                    parameters: new Dictionary<string, object>
                    {

                    },
                    message: out string message
                );

                int total = 0;

                if (table == null)
                {
                    response = new StatusResponse("No orders to fulfill.");
                    return 0;
                }
                else
                {

                    // For every order that needs to be fulfilled
                    List<Security_db> inst = new List<Security_db>();
                    total = inst.Count();
                    foreach (DataRow row in table.Rows)
                    {
                        int Id = int.Parse(row["id"].ToString());
                        int AccountRef = int.Parse(row["accountref"].ToString());
                        int Action = int.Parse(row["action"].ToString());
                        DateTime date = DateTime.Parse(row["datecreated"].ToString());
                        double price = double.Parse(row["Price"].ToString());
                        int Quantity = int.Parse(row["quantity"].ToString());
                        string symbol = row["symbol"].ToString();
                        double rate = CommissionsHelper_db.GetCommission(row["BrokerName"].ToString(), Action, context, out StatusResponse resp);

                        // Delete this entry
                        Remove(Id, context, out StatusResponse statusResponse);

                        // Realized PnL
                        double realizedPnL = 0;
                        if (Action == (int)OrderAction.Upper_limit_sell)
                        {
                            realizedPnL = price;
                        }
                        else if (Action == (int)OrderAction.Lower_limit_sell)
                        {
                            realizedPnL = price;
                        }

                        // Add new entry to Transactions table
                        TransactionsHelper_db.Add(Id, AccountRef, Action, price, (double)(price * (rate / 100)), DateTime.Now, price, Quantity, realizedPnL, context, out StatusResponse resp1);
                    }
                }

                // Return
                response = new StatusResponse("Sucessfully fulfilled " + total + " orders.");
                return total;
            }
            catch (Exception ex)
            {
                response = new StatusResponse(ex);
                return -1;
            }
        }

        /// <summary>
        /// Add adds a new account entry into the database, assuming that it is active and using the current UTC time.
        /// </summary>
        /// <returns>Account_db object</returns>
        public static void Add(int AccountRef, int Action, double TargetPrice, int Quantity, int Status, string Symbol, string Broker, DBContext context)
        {
            try
            {
                // Validate current data


                // Create instance
                Orders_db inst = new Orders_db(
                    0,
                    AccountRef,
                    Action,
                    TargetPrice,
                    DateTime.Now,
                    Quantity,
                    Status,
                    Symbol,
                    Broker
                    );
                
                // Attempt to add to database
                int rowsAffected = context.ExecuteNonQueryCommand(
                    commandText: "INSERT INTO Orders (AccountRef, Action, TargetPrice, DateCreated, Quantity, Status, Symbol, BrokerName) VALUES (@accountref, @action, @targetprice, @datecreated, @quantity, @status, @symbol, @brokername)",
                    parameters: new Dictionary<string, object> {
                        {"@accountref", inst.AccountRef },
                        {"@action", inst.Action },
                        {"@targetprice", inst.TargetPrice },
                        {"@datecreated", inst.DateCreated },
                        {"@quantity", inst.Quantity },
                        {"@status", inst.Status },
                        {"@symbol", inst.Symbol },
                        {"@brokername", inst.Broker }
                    },
                    message: out string message
                );
                if (rowsAffected == -1)
                    throw new Exception(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                throw new Exception(ex.Message);
            }
        }

        public static List<Orders_db> GetCollectionByAccount(int accountId, DBContext context, out StatusResponse response)
        {
            try
            {
                // Attempt to get from the database
                DataTable table = context.ExecuteDataQueryCommand(
                    commandText: "SELECT * FROM Orders WHERE AccountRef = ?",
                    parameters: new Dictionary<string, object>
                    {
                        {"@accountref", accountId }
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
                        TargetPrice: double.Parse(row["targetprice"].ToString()),
                        DateCreated: DateTime.Parse(row["datecreated"].ToString()),
                        Quantity: int.Parse(row["quantity"].ToString()),
                        Status: int.Parse(row["status"].ToString()),
                        Symbol: row["symbol"].ToString(),
                        Broker: row["brokername"].ToString()
                        )
                    );
                }

                // Return
                response = new StatusResponse("Orders successfully retreived.");
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
