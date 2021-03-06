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
    public class Account_SummaryHelper_db
    {

        /// <summary>
        /// Add adds a new account entry into the database, assuming that it is active and using the current UTC time.
        /// </summary>
        /// <returns>Account_db object</returns>
        public static Account_Summary_db Add(int AccountRef, double AvailableFunds, double GrossPositionValue, DBContext context, out StatusResponse response)
        {
            try
            {
                // Validate current data
                if (AccountRef < 1)
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid accountref entered.");
                if (AvailableFunds < 0)
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid funds entered.");
                if (GrossPositionValue < 0)
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid GrossPositionValue entered.");

                // Create instance
                Account_Summary_db inst = new Account_Summary_db(
                    AccountRef,
                    AvailableFunds,
                    GrossPositionValue,
                    AvailableFunds+GrossPositionValue
                    );

                // Attempt to add to database
                int rowsAffected = context.ExecuteNonQueryCommand(
                    commandText: "INSERT INTO Account_Summary (AccountRef, AvailableFunds, GrossPositionValue, NetLiquidation) VALUES (@accountref, @availablefunds, @grosspositionvalue, @netliquidation)",
                    parameters: new Dictionary<string, object> {
                        {"@accountref", inst.AccountRef },
                        {"@availablefunds", inst.AvailableFunds },
                        {"@grosspositionvalue", inst.GrossPositionValue },
                        {"@netliquidation", inst.NetLiquidation }
                    },
                    message: out string message
                );
                if (rowsAffected == -1)
                    throw new Exception(message);
                if (rowsAffected == 0)
                    throw new Exception(message);

                // Return
                response = new StatusResponse("Account_Summary added successfully");
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
        /// Updates existing Account Summary in the DB.
        /// </summary>
        /// <returns>Account_db object</returns>
        public static Account_Summary_db Update(int AccountRef, double AvailableFunds, double GrossPositionValue, DBContext context, out StatusResponse response)
        {
            try
            {
                // Validate current data
                if (AccountRef < 1)
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid accountref entered.");
                if (AvailableFunds < 0)
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid funds entered.");
                if (GrossPositionValue < 0)
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid GrossPositionValue entered.");

                // Create instance
                Account_Summary_db inst = new Account_Summary_db(
                    AccountRef,
                    AvailableFunds,
                    GrossPositionValue,
                    AvailableFunds+GrossPositionValue
                    );

                // Attempt to add to database
                int rowsAffected = context.ExecuteNonQueryCommand(
                    commandText: "UPDATE Account_Summary SET AvailableFunds=@availablefunds, GrossPositionValue=@grosspositionvalue, NetLiquidation=@netliquidation WHERE AccountRef=@accountref",
                    parameters: new Dictionary<string, object> {
                        {"@accountref", inst.AccountRef },
                        {"@availablefunds", inst.AvailableFunds },
                        {"@grosspositionvalue", inst.GrossPositionValue },
                        {"@netliquidation", inst.NetLiquidation }
                    },
                    message: out string message
                );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return
                response = new StatusResponse("Account_Summary added successfully");
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
        /// Deletes a Account Summary from the DB
        /// Warning : If the summary is deleted deleting the account itself is highly recommended.
        /// </summary>
        /// <returns>Account_db object</returns>
        public static int DeleteSummary(int AccountRef, DBContext context, out StatusResponse response)
        {
            try
            {
                // Validate current data
                if (AccountRef < 1)
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid accountref entered.");

                // Get the summary by account
                List<Account_Summary_db> list = GetSummaryByAccount(AccountRef, context, out StatusResponse resp);
                if (list.Count == 0)
                {
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Account summary does not exist.");
                }

                // Attempt to delete from database
                int rowsAffected = context.ExecuteNonQueryCommand(
                    commandText: "DELETE FROM `Account_Summary` WHERE AccountRef=@accountref",
                    parameters: new Dictionary<string, object> {
                        {"@accountref", AccountRef},
                    },
                    message: out string message
                );
                if (rowsAffected == -1)
                    throw new Exception(message);
                if (rowsAffected == 0)
                    throw new Exception("No relevant Account_Summary to delete.");

                // Return
                response = new StatusResponse("Account_Summary deleted successfully");
                return rowsAffected;
            }
            catch (Exception ex)
            {
                // Error occured.
                response = new StatusResponse(ex);
                return -1;
            }
        }

        /// <summary>
        /// Updates existing Account Summary's available funds in the DB
        /// </summary>
        /// <returns>Account_db object</returns>
        public static Account_Summary_db UpdateFunds(int AccountRef, double AvailableFunds, DBContext context, out StatusResponse response)
        {
            try
            {
                // Validate current data
                if (AccountRef < 1)
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid accountref entered.");
                if (AvailableFunds < 0)
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid funds entered.");

                // Get the summary by account
                List<Account_Summary_db> list = GetSummaryByAccount(AccountRef, context, out StatusResponse resp);
                if(list.Count == 0)
                {
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Account summary does not exist.");
                }
                Account_Summary_db original = list[0];

                double originalNetLiq = original.NetLiquidation;
                double difference = AvailableFunds - original.AvailableFunds;
                originalNetLiq += difference;

                // Create instance
                Account_Summary_db inst = new Account_Summary_db(
                    AccountRef,
                    AvailableFunds,
                    0,
                    originalNetLiq
                    );

                // Attempt to add to database
                int rowsAffected = context.ExecuteNonQueryCommand(
                    commandText: "UPDATE Account_Summary SET AvailableFunds=@availablefunds, NetLiquidation=@netliquidation WHERE AccountRef=@accountref",
                    parameters: new Dictionary<string, object> {
                        {"@accountref", inst.AccountRef },
                        {"@availablefunds", inst.AvailableFunds },
                        {"@grosspositionvalue", inst.GrossPositionValue },
                        {"@netliquidation", inst.NetLiquidation }
                    },
                    message: out string message
                );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return
                response = new StatusResponse("Account_Summary added successfully");
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
        /// Updates existing Account Summary's available funds in the DB.
        /// Intentionally does NOT protect against difference beyond bounds, since accounts can technically go below avail funds
        /// if there are held funds for commisions.
        /// </summary>
        /// <returns>Account_db object</returns>
        public static Account_Summary_db UpdateFundsDiff(int AccountRef, double difference, DBContext context, out StatusResponse response)
        {
            try
            {
                // Validate current data
                if (AccountRef < 1)
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid accountref entered.");

                // Get the summary by account
                List<Account_Summary_db> list = GetSummaryByAccount(AccountRef, context, out StatusResponse resp);
                if (list.Count == 0)
                {
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Account summary does not exist.");
                }
                Account_Summary_db original = list[0];

                double newNetLiq = original.NetLiquidation + difference;

                double newAvailableFunds = original.AvailableFunds + difference;

                // Create instance
                Account_Summary_db inst = new Account_Summary_db(
                    AccountRef,
                    newAvailableFunds,
                    0,
                    newNetLiq
                    );

                // Attempt to add to database
                int rowsAffected = context.ExecuteNonQueryCommand(
                    commandText: "UPDATE Account_Summary SET AvailableFunds=@availablefunds, NetLiquidation=@netliquidation WHERE AccountRef=@accountref",
                    parameters: new Dictionary<string, object> {
                        {"@accountref", inst.AccountRef },
                        {"@availablefunds", inst.AvailableFunds },
                        {"@grosspositionvalue", inst.GrossPositionValue },
                        {"@netliquidation", inst.NetLiquidation }
                    },
                    message: out string message
                );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return
                response = new StatusResponse("Account_Summary added successfully");
                return inst;
            }
            catch (Exception ex)
            {
                // Error occured.
                response = new StatusResponse(ex);
                return null;
            }
        }


        public static List<Account_Summary_db> getCollection(DBContext context, out StatusResponse response)
        {
            try
            {
                // Attempt to get from the database
                DataTable table = context.ExecuteDataQueryCommand(
                    commandText: "SELECT * FROM Account_Summary",
                    parameters: new Dictionary<string, object>
                    {

                    },
                    message: out string message
                );

                if (table == null)
                    throw new Exception(message);

                // Parse
                List<Account_Summary_db> inst = new List<Account_Summary_db>();
                foreach (DataRow row in table.Rows)
                {
                    inst.Add(new Account_Summary_db(
                        AccountRef: int.Parse(row["accountref"].ToString()),
                        AvailableFunds: double.Parse(row["availablefunds"].ToString()),
                        GrossPositionValue: double.Parse(row["grosspositionvalue"].ToString()),
                        NetLiquidation: double.Parse(row["netliquidation"].ToString())
                        )
                    );
                }

                // Return
                response = new StatusResponse("Account_Summary successfully retreived.");
                return inst;
            }
            catch (Exception ex)
            {
                response = new StatusResponse(ex);
                return null;
            }
        }

        public static List<Account_Summary_db> getCollection(DBContext context)
        {
            try
            {
                // Attempt to get from the database
                DataTable table = context.ExecuteDataQueryCommand(
                    commandText: "SELECT * FROM Account_Summary",
                    parameters: new Dictionary<string, object>
                    {

                    },
                    message: out string message
                );

                if (table == null)
                    throw new Exception(message);

                // Parse
                List<Account_Summary_db> inst = new List<Account_Summary_db>();
                foreach (DataRow row in table.Rows)
                {
                    inst.Add(new Account_Summary_db(
                        AccountRef: int.Parse(row["accountref"].ToString()),
                        AvailableFunds: double.Parse(row["availablefunds"].ToString()),
                        GrossPositionValue: double.Parse(row["grosspositionvalue"].ToString()),
                        NetLiquidation: double.Parse(row["netliquidation"].ToString())
                        )
                    );
                }


                return inst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<Account_Summary_db> GetSummaryByAccount(int Id, DBContext context, out StatusResponse response)
        {
            try
            {
                // Attempt to get from the database
                DataTable table = context.ExecuteDataQueryCommand(
                    commandText: "SELECT * FROM Account_Summary WHERE AccountRef=@accountref",
                    parameters: new Dictionary<string, object>
                    {
                        {"@accountref", Id}
                    },
                    message: out string message
                );

                if (table == null)
                    throw new Exception(message);

                // Parse
                List<Account_Summary_db> inst = new List<Account_Summary_db>();
                foreach (DataRow row in table.Rows)
                {
                    inst.Add(new Account_Summary_db(
                        AccountRef: int.Parse(row["accountref"].ToString()),
                        AvailableFunds: double.Parse(row["availablefunds"].ToString()),
                        GrossPositionValue: double.Parse(row["grosspositionvalue"].ToString()),
                        NetLiquidation: double.Parse(row["netliquidation"].ToString())
                        )
                    );
                }

                // Return
                response = new StatusResponse("Account_Summary successfully retreived.");
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
