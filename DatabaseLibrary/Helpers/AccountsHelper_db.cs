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
    public class AccountsHelper_db
    {
        /// <summary>
        /// Force deletes an account by its id, by deleting ALL linked assets.
        /// </summary>
        /// <returns>Account_db object</returns>
        public static int ForceDelete(DBContext context, out StatusResponse response, int id = -1)
        {
            if(id == -1)
            {
                throw new Exception("Invalid ID supplied.");
            }

            try
            {
                // Attempt to add to database
                int rowsAffected = context.ExecuteNonQueryCommand(
                    commandText: "DELETE FROM `Accounts` WHERE `Id`=@Id",
                    parameters: new Dictionary<string, object> {
                        {"@Id", id },
                    },
                    message: out string message
                );
                if (rowsAffected == -1)
                    throw new Exception(message);
                if(rowsAffected == 0)
                {
                    response = new StatusResponse("No account with given id.");
                }
                else
                    response = new StatusResponse("Sucessfully deleted.");
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
        /// Update updates an account's parameters based on it's Id 
        /// </summary>
        /// <returns>Account_db object</returns>
        public static Accounts_db Update(int id, string broker, bool active, string name, string desc, string pass, DBContext context, out StatusResponse response)
        {
            try
            {
                // Validate current data
                if (string.IsNullOrEmpty(name?.Trim()))
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid name entered.");
                if (string.IsNullOrEmpty(broker?.Trim()))
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid broker entered.");
                if(string.IsNullOrEmpty(desc?.Trim()))
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid description entered.");
                if (string.IsNullOrEmpty(pass?.Trim()))
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid password entered.");

                // Create instance
                Accounts_db inst = new Accounts_db(
                    id,
                    active,
                    broker,
                    DateTime.Now,
                    name,
                    desc,
                    pass
                    );

                // Attempt to add to database
                int rowsAffected = context.ExecuteNonQueryCommand(
                    commandText: "UPDATE `Accounts` SET `Broker`=@broker,`Name`=@name,`Description`=@desc,`Password`=@pass, `Active`=@active WHERE `Id`=@id",
                    parameters: new Dictionary<string, object> {
                        {"@id", inst.Id },
                        {"@broker", inst.Broker },
                        {"@name", inst.Name },
                        {"@desc", inst.Description },
                        {"@pass", inst.Password },
                        {"@active", inst.Active }
                    },
                    message: out string message
                );
                if (rowsAffected == -1 || rowsAffected == 0)
                    throw new Exception(message);

                // Return

                response = new StatusResponse("Account updated successfully");
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
        public static Accounts_db Add(int id, string broker, string name, string desc, string pass, DBContext context, out StatusResponse response)
        {
            try
            {
                // Validate current data
                if (string.IsNullOrEmpty(name?.Trim()))
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid name entered.");
                if (string.IsNullOrEmpty(broker?.Trim()))
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid broker entered.");
                if (string.IsNullOrEmpty(desc?.Trim()))
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid description entered.");
                if (string.IsNullOrEmpty(pass?.Trim()))
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid password entered.");

                // Create instance
                Accounts_db inst = new Accounts_db(
                    id,
                    true,
                    broker,
                    DateTime.Now,
                    name,
                    desc,
                    pass
                    );
                
                // Attempt to add to database
                int rowsAffected = context.ExecuteNonQueryCommand(
                    commandText: "INSERT INTO Accounts (Id, Active, Broker, DateCreated, Name, Description, Password) VALUES (@id, @active, @broker, @date, @name, @desc, @pass)",
                    parameters: new Dictionary<string, object> {
                        {"@id", inst.Id },
                        {"@active", true },
                        {"@broker", inst.Broker },
                        {"@date", inst.Date },
                        {"@name", inst.Name },
                        {"@desc", inst.Description },
                        {"@pass", inst.Password }
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


        public static List<Accounts_db> getCollection(DBContext context, out StatusResponse response)
        {
            try
            {
                // Attempt to get from the database
                DataTable table = context.ExecuteDataQueryCommand(
                    commandText: "SELECT * FROM Accounts",
                    parameters: new Dictionary<string, object>
                    {

                    },
                    message: out string message
                );

                if (table == null)
                    throw new Exception(message);

                // Parse
                List<Accounts_db> inst = new List<Accounts_db>();
                foreach (DataRow row in table.Rows)
                {
                    inst.Add(new Accounts_db(
                        id: int.Parse(row["Id"].ToString()),
                        active: row["Active"].ToString() == "1",
                        broker: row["Broker"].ToString(),
                        date: DateTime.Parse(row["datecreated"].ToString()),
                        name: row["name"].ToString(),
                        desc: row["description"].ToString(),
                        pass: row["password"].ToString()
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

        public static List<Accounts_db> getAccountById(int id, DBContext context, out StatusResponse response)
        {
            try
            {
                // Attempt to get from the database
                DataTable table = context.ExecuteDataQueryCommand(
                    commandText: "SELECT * FROM Accounts WHERE `Id`=@id",
                    parameters: new Dictionary<string, object>
                    {
                        {"@id", id}
                    },
                    message: out string message
                );

                if (table == null)
                    throw new Exception(message);

                // Parse
                List<Accounts_db> inst = new List<Accounts_db>();
                foreach (DataRow row in table.Rows)
                {
                    inst.Add(new Accounts_db(
                        id: int.Parse(row["Id"].ToString()),
                        active: row["Active"].ToString() == "1",
                        broker: row["Broker"].ToString(),
                        date: DateTime.Parse(row["datecreated"].ToString()),
                        name: row["name"].ToString(),
                        desc: row["description"].ToString(),
                        pass: row["password"].ToString()
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

        public static List<Accounts_db> getCollection(DBContext context)
        {
            try
            {
                // Attempt to get from the database
                DataTable table = context.ExecuteDataQueryCommand(
                    commandText: "SELECT * FROM Accounts",
                    parameters: new Dictionary<string, object>
                    {

                    },
                    message: out string message
                );

                if (table == null)
                    throw new Exception(message);

                // Parse
                List<Accounts_db> inst = new List<Accounts_db>();
                foreach (DataRow row in table.Rows)
                {
                    inst.Add(new Accounts_db(
                        id: int.Parse(row["Id"].ToString()),
                        active: row["Active"].ToString() == "1",
                        broker: row["Broker"].ToString(),
                        date: DateTime.Parse(row["datecreated"].ToString()),
                        name: row["name"].ToString(),
                        desc: row["description"].ToString(),
                        pass: row["password"].ToString()
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
