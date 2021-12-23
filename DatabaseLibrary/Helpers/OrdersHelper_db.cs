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
        /// Edits existing AccountRef of Order object based on the input data.
        /// </summary>
        /// <returns>Account_db object edited </returns>
        public static Orders_db Edit(int Id, int AccountRef, int Action, double TargetPrice, DateTime DateCreated, int Quantity, int Status, string Symbol, string Broker, DBContext context, out StatusResponse response)
        {
            try
            {
                // Validate current data
                if (TargetPrice < 0)
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid target price.");
                if (DateCreated == null)
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid date created.");
                if (Quantity < 0)
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid quantity.");
                if (!Enum.IsDefined(typeof(OrderAction), Status))
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid order action.");

                // Create instance
                Orders_db inst = new Orders_db(
                    Id,
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
                    commandText: "UPDATE Orders SET AccountRef=@accountref, Action=@action, TargetPrice=@targetprice, DateCreated=@datecreated, Quantity=@quantity, Status=@status, Symbol=@symbol, BrokerName=@broker WHERE Id=@Id",
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
                else if(rowsAffected == 0)
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "No existing order.");

                // Return
                response = new StatusResponse("Order updated sucessfully.");
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
        /// Add adds a new account entry into the database, assuming that it is active and using the current UTC time.
        /// </summary>
        /// <returns>Account_db object</returns>
        public static Orders_db Add(int AccountRef, int Action, double TargetPrice, DateTime DateCreated, int Quantity, int Status, string Symbol, string Broker, DBContext context, out StatusResponse response)
        {
            try
            {
                // Validate current data
                if (TargetPrice < 0)
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid target price.");
                if (DateCreated == null)
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid date created.");
                if (Quantity < 0)
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid quantity.");
                if (!Enum.IsDefined(typeof(OrderAction), Status))
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid order action.");


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
                    commandText: "INSERT INTO Orders (AccountRef, Action, TargetPrice, DateCreated, Quantity, Status, Symbol, BrokerName) VALUES (@accountref, @action, @targetprice, @datecreated, @quantity, @status, @symbol, @broker)",
                    parameters: new Dictionary<string, object> {
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
                    commandText: "SELECT * FROM Orders AS O, Security AS S WHERE(O.Symbol = S.Symbol AND((O.ACTION = 0 OR O.ACTION = 2) AND O.TargetPrice <= S.Price) OR (O.Symbol = S.Symbol AND(O.ACTION = 1 OR O.ACTION = 3) AND O.TargetPrice >= S.Price))",
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

                        double oldquantity = 0;
                        string oldsymbol = symbol;
                        double oldavgprice = 0;
                        foreach (var owned in OwnedSecurityHelper_db.GetOwnedByAccount(AccountRef, context))
                        {
                            if (owned.Symbol == symbol)
                            {
                                oldquantity = owned.Quantity;
                                oldsymbol = owned.Symbol;
                                oldavgprice = owned.AveragePrice;
                                OwnedSecurityHelper_db.Remove(AccountRef, symbol, context, out StatusResponse resp2);
                            }
                        }
                        double totalprice = oldavgprice * oldquantity;
                        totalprice += Quantity * price;

                        if (Action == 2 || Action == 3)
                        {
                            OwnedSecurityHelper_db.Add(AccountRef, symbol, Quantity + (int)oldquantity, totalprice / (Quantity + oldquantity), context, out StatusResponse resp3);
                            Account_SummaryHelper_db.UpdateFundsDiff(AccountRef, -(Quantity * price), context, out StatusResponse resp4);


                            double gross = 0;
                            double avail = 0;
                            foreach (var sum in Account_SummaryHelper_db.GetSummaryByAccount(AccountRef, context, out StatusResponse resp5)) {
                                avail = sum.AvailableFunds;
                            }

                            foreach (var owned in OwnedSecurityHelper_db.GetOwnedByAccount(AccountRef, context))
                            {
                                gross += (owned.Quantity * owned.AveragePrice);
                            }
                            Account_SummaryHelper_db.Update(AccountRef, avail, gross, context, out StatusResponse resp6);


                        }
                        else {
                            realizedPnL = (price * Quantity) - (oldavgprice * Quantity);
                            OwnedSecurityHelper_db.Add(AccountRef, symbol, (int)oldquantity - Quantity, (totalprice- (2* oldquantity*oldavgprice)) / (Quantity - oldquantity), context, out StatusResponse resp3);
                            Account_SummaryHelper_db.UpdateFundsDiff(AccountRef, (Quantity * price), context, out StatusResponse resp4);

                            double gross = 0;
                            double avail = 0;
                            foreach (var sum in Account_SummaryHelper_db.GetSummaryByAccount(AccountRef, context, out StatusResponse resp7))
                            {
                                avail = sum.AvailableFunds;
                            }
                            foreach (var owned in OwnedSecurityHelper_db.GetOwnedByAccount(AccountRef, context))
                            {
                                gross += owned.Quantity * owned.AveragePrice;
                            }

                            Account_SummaryHelper_db.Update(AccountRef, avail, gross, context, out StatusResponse resp8);
                        }
                        
                        TransactionsHelper_db.Add(AccountRef, Action, price, (double)(price * (rate / 100)), DateTime.Now, Quantity, price * Quantity, realizedPnL, context, out StatusResponse resp1);


                        
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
