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
        public static Account_Summary_db Add(int AccountRef, double AvailableFunds, double GrossPositionValue, double NetLiquidation, DBContext context, out StatusResponse response)
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
                if (NetLiquidation < 0)
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid NetLiquidation entered.");

                // Create instance
                Account_Summary_db inst = new Account_Summary_db(
                    AccountRef,
                    AvailableFunds,
                    GrossPositionValue,
                    NetLiquidation
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
    }
}
