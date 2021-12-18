﻿using DatabaseLibrary.Core;
using DatabaseLibrary.Models;
using System.Data;

namespace DatabaseLibrary.Helpers
{
    public class TransactionsHelper_db
    {

        /// <summary>
        /// Add adds a new account entry into the database, assuming that it is active and using the current UTC time.
        /// </summary>
        /// <returns>Account_db object</returns>
        public static Transactions_db Add_id(int Id, int AccountRef, int Action, float AveragePrice, float Commission, DateTime DateCreated, int Quantity, float RealizedPNL, DBContext context, out StatusResponse response)
        {
            try
            {
                // Validate current data


                // Create instance
                Transactions_db inst = new Transactions_db(
                    Id,
                    AccountRef,
                    Action,
                    AveragePrice,
                    Commission,
                    DateCreated,
                    Quantity,
                    RealizedPNL
                    );

                // Attempt to add to database
                int rowsAffected = context.ExecuteNonQueryCommand(
                    commandText: "INSERT INTO TRANSACTIONS (Id, AccountRef, Action, AveragePrice, Commission, DateCreated, OrderRef, Price, Quantity, RealizedPNL) VALUES (@id, @accountref, @action, @averageprice, @commission, @datecreated, @orderref, @price, @quantity, @realizedpnl)",
                    parameters: new Dictionary<string, object> {
                        {"@id", inst.Id },
                        {"@accountref", inst.AccountRef },
                        {"@action", inst.Action },
                        {"@averageprice", inst.AveragePrice },
                        {"@commission", inst.Commission },
                        {"@datecreated", inst.DateCreated },
                        {"@price", inst.Price },
                        {"@quantity", inst.Quantity },
                        {"@realizedpnl", inst.RealizedPNL }
                    },
                    message: out string message
                );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return
                response = new StatusResponse("Transaction added successfully");
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
        /// Relies on Id AUTO_INCREMENT
        /// </summary>
        /// <returns>Account_db object</returns>
        public static Transactions_db Add(int AccountRef, int Action, float AveragePrice, float Commission, DateTime DateCreated, int Quantity, float RealizedPNL, DBContext context, out StatusResponse response)
        {
            try
            {
                // Validate current data


                // Create instance
                Transactions_db inst = new Transactions_db(
                    0,
                    AccountRef,
                    Action,
                    AveragePrice,
                    Commission,
                    DateCreated,
                    Quantity,
                    RealizedPNL
                    );

                // Attempt to add to database
                int rowsAffected = context.ExecuteNonQueryCommand(
                    commandText: "INSERT INTO Transactions (AccountRef, Action, AveragePrice, Commission, DateCreated, Price, Quantity, RealizedPNL) VALUES (@accountref, @action, @averageprice, @commission, @datecreated, @price, @quantity, @realizedpnl)",
                    parameters: new Dictionary<string, object> {
                        {"@accountref", inst.AccountRef },
                        {"@action", inst.Action },
                        {"@averageprice", inst.AveragePrice },
                        {"@commission", inst.Commission },
                        {"@datecreated", inst.DateCreated },
                        {"@price", inst.Price },
                        {"@quantity", inst.Quantity },
                        {"@realizedpnl", inst.RealizedPNL }
                    },
                    message: out string message
                );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return
                response = new StatusResponse("Transaction added successfully");
                return inst;
            }
            catch (Exception ex)
            {
                // Error occured.
                response = new StatusResponse(ex);
                return null;
            }
        }


        public static List<Transactions_db> getCollection(DBContext context, out StatusResponse response)
        {
            try
            {
                // Attempt to get from the database
                DataTable table = context.ExecuteDataQueryCommand(
                    commandText: "SELECT * FROM Transactions",
                    parameters: new Dictionary<string, object>
                    {

                    },
                    message: out string message
                );

                if (table == null)
                    throw new Exception(message);

                // Parse
                List<Transactions_db> inst = new List<Transactions_db>();
                foreach (DataRow row in table.Rows)
                {
                    inst.Add(new Transactions_db(
                        Id: int.Parse(row["id"].ToString()),
                        AccountRef: int.Parse(row["accountref"].ToString()),
                        Action: int.Parse(row["action"].ToString()),
                        AveragePrice: float.Parse(row["averageprice"].ToString()),
                        Commission: float.Parse(row["commission"].ToString()),
                        DateCreated: DateTime.Parse(row["datecreated"].ToString()),
                        Quantity: int.Parse(row["quantity"].ToString()),
                        RealizedPNL: float.Parse(row["realizedpnl"].ToString())
                        )
                    );
                }

                // Return
                response = new StatusResponse("Transactions successfully retreived.");
                return inst;
            }
            catch (Exception ex)
            {
                response = new StatusResponse(ex);
                return null;
            }
        }

        public static List<Transactions_db> getCollection(DBContext context)
        {
            try
            {
                // Attempt to get from the database
                DataTable table = context.ExecuteDataQueryCommand(
                    commandText: "SELECT * FROM Transactions",
                    parameters: new Dictionary<string, object>
                    {

                    },
                    message: out string message
                );

                if (table == null)
                    throw new Exception(message);

                // Parse
                List<Transactions_db> inst = new List<Transactions_db>();
                foreach (DataRow row in table.Rows)
                {
                    inst.Add(new Transactions_db(
                        Id: int.Parse(row["id"].ToString()),
                        AccountRef: int.Parse(row["accountref"].ToString()),
                        Action: int.Parse(row["action"].ToString()),
                        AveragePrice: float.Parse(row["averageprice"].ToString()),
                        Commission: float.Parse(row["commission"].ToString()),
                        DateCreated: DateTime.Parse(row["datecreated"].ToString()),
                        Quantity: int.Parse(row["quantity"].ToString()),
                        RealizedPNL: float.Parse(row["realizedpnl"].ToString())
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
