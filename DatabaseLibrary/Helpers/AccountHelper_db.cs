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
    public class AccountHelper_db
    {

        /// <summary>
        /// Add adds a new account entry into the database, assuming that it is active and using the current UTC time.
        /// </summary>
        /// <returns>Account_db object</returns>
        public static Account_db Add(string broker, string name, string desc, DBContext context, out StatusResponse response)
        {
            try
            {
                // Validate current data
                if (string.IsNullOrEmpty(name?.Trim()))
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid name entered.");
                if (string.IsNullOrEmpty(broker?.Trim()))
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid broker entered.");

                // Create instance
                Account_db inst = new Account_db(
                    true,
                    broker,
                    DateTime.Now,
                    name,
                    desc,
                    2);

                // Attempt to add to database
                int rowsAffected = context.ExecuteNonQueryCommand(
                    commandText: "INSERT INTO ACCOUNTS (Active, Broker, DateCreated, Name, Description) VALUES (@active, @broker, @date, @name, @desc)",
                    parameters: new Dictionary<string, object> {
                        {"@active", true },
                        {"@broker", inst.Broker },
                        {"@date", inst.Date },
                        {"@name", inst.Name },
                        {"@desc", inst.Description }
                    },
                    message: out string message
                );
                if(rowsAffected == -1)
                    throw new Exception(message);

                // Return
                response = new StatusResponse("Account added successfully");
                return inst;
            } 
            catch (Exception ex)
            {
                // Error occured.
                response = new StatusResponse(ex);
                return null;
            }
        }


        public static List<Account_db> getCollection(DBContext context, out StatusResponse response)
        {
            try
            {
                // Attempt to get from the database
                DataTable table = context.ExecuteDataQueryCommand(
                    commandText: "SELECT * FROM ACCOUNTS",
                    parameters: new Dictionary<string, object>
                    {

                    },
                    message: out string message
                );

                if (table == null)
                    throw new Exception(message);

                // Parse
                List<Account_db> inst = new List<Account_db>();
                foreach (DataRow row in table.Rows)
                {
                    inst.Add(new Account_db(
                        id: int.Parse(row["Id"].ToString()),
                        active: Boolean.Parse(row["Active"].ToString()),
                        broker: row["Broker"].ToString(),
                        date: DateTime.Parse(row["date"].ToString()),
                        name: row["name"].ToString(),
                        desc: row["description"].ToString()
                        )
                    );
                }

                // Return
                response = new StatusResponse("Accounts successfully retreived.");
                return inst;
            } catch (Exception ex)
            {
                response = new StatusResponse(ex);
                return null;
            }
        }
    }
}
