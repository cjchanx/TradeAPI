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
    public class CommissionsHelper_db
    {
        public static Commission_db Add(string broker, int type, double rate, DBContext context, out StatusResponse response)
        {
            try
            {
                // Validate current data
                if (string.IsNullOrEmpty(broker?.Trim()))
                    throw new StatusException(System.Net.HttpStatusCode.BadRequest, "Invalid symbol entered.");;

                // Create instance
                Commission_db inst = new Commission_db(
                    broker,
                    type,
                    rate
                    );

                // Attempt to add to database
                int rowsAffected = context.ExecuteNonQueryCommand(
                    commandText: "INSERT INTO Commissions (BrokerName, OrderType, Rate) VALUES (@name, @type, @rate)",
                    parameters: new Dictionary<string, object> {
                        {"@name", inst.Broker},
                        {"@type", inst.Type },
                        {"@rate", inst.Rate }
                    },
                    message: out string message
                );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return
                response = new StatusResponse("Comission rate added successfully");
                return inst;
            }
            catch (Exception ex)
            {
                // Error occured.
                response = new StatusResponse(ex);
                return null;
            }
        }

        public static List<Commission_db> GetCollection(DBContext context, out StatusResponse response)
        {
            try
            {
                // Attempt to get from the database
                DataTable table = context.ExecuteDataQueryCommand(
                    commandText: "SELECT * FROM Comissions",
                    parameters: new Dictionary<string, object>
                    {

                    },
                    message: out string message
                );

                if (table == null)
                    throw new Exception(message);

                // Parse
                List<Commission_db> inst = new List<Commission_db>();
                foreach (DataRow row in table.Rows)
                {
                    inst.Add(new Commission_db(
                            broker: row["BrokerName"].ToString(),
                            type: Int32.Parse(row["OrderType"].ToString()),
                            rate: double.Parse(row["Rate"].ToString())
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

        public static double GetCommission(string broker, int type, DBContext context, out StatusResponse response)
        {
            try
            {
                // Attempt to get from the database
                DataTable table = context.ExecuteDataQueryCommand(
                    commandText: "SELECT * FROM Comissions WHERE BrokerName = @name, OrderType = @typ",
                    parameters: new Dictionary<string, object>
                    {
                        {"@name", broker},
                        {"@typ", type }
                    },
                    message: out string message
                );

                if (table == null)
                    throw new Exception(message);

                // Parse
                Commission_db inst = null;
                foreach (DataRow row in table.Rows)
                {
                    inst = new Commission_db(
                            broker: row["BrokerName"].ToString(),
                            type: Int32.Parse(row["OrderType"].ToString()),
                            rate: double.Parse(row["Rate"].ToString())
                        );
                }

                // Return
                response = new StatusResponse("Comission sucessfully retreived.");

                if (inst == null)
                    return 0;
                else
                    return inst.Rate;
            }
            catch (Exception ex)
            {
                response = new StatusResponse(ex);
            }

            return 0;
        }

        /// <summary>
        /// Remove removes the relevant Commission from the database by BrokerName and Type, returns number of rows updated.
        /// </summary>
        /// <returns></returns>
        public static int Remove(string broker, int type, DBContext context, out StatusResponse response)
        {
            Console.WriteLine("Running REMOVE.");
            try
            {
                // Attempt to remove from database
                int rowsAffected = context.ExecuteNonQueryCommand(
                    commandText: "DELETE FROM `Comissions` WHERE BrokerName=@name AND Type=@type",
                    parameters: new Dictionary<string, object> {
                        {"@brokername", broker},
                        {"@type", type }
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

        /// <summary>
        /// Remove removes the relevant Commission from the database by BrokerName, returns number of rows updated.
        /// </summary>
        /// <returns></returns>
        public static int RemoveByBroker(string broker, DBContext context, out StatusResponse response)
        {
            Console.WriteLine("Running REMOVE.");
            try
            {
                // Attempt to remove from database
                int rowsAffected = context.ExecuteNonQueryCommand(
                    commandText: "DELETE FROM `Comissions` WHERE BrokerName=@name",
                    parameters: new Dictionary<string, object> {
                        {"@brokername", broker},
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
