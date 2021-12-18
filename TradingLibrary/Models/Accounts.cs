using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TradingLibrary.Models
{
    public class Accounts
    {
        #region Constructors

        /// <summary>
        /// Account constructor with parameters.
        /// </summary>
        /// <param name="active"></param>
        /// <param name="broker"></param>
        /// <param name="date"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public Accounts(int id, bool active, string broker, DateTime date, string name, string desc)
        {
            Id = id;
            Active = active;
            Broker = broker; 
            Date = date;
            Name = name;
            Description = desc;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Account status, active or not.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Account status, active or not.
        /// </summary>
        [JsonProperty(PropertyName = "active")]
        public bool Active { get; set; }

        /// <summary>
        /// Account name
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// DateTime object of the Date which the Account was created.
        /// </summary>
        [JsonProperty(PropertyName = "datecreated")]
        public DateTime Date { get; set; }

        /// <summary>
        /// string indicating the name of the related broker.
        /// </summary>
        [JsonProperty(PropertyName = "broker")]
        public string Broker { get; set; }

        /// <summary>
        /// string for a description of the account
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// returns active bool status as a string
        /// </summary>
        /// <returns></returns>
        public static string activeString(bool Active)
        {
            return Active ? "Active" : "Inactive";
        }
        /// <summary>
        /// returns a string containing the information of the class
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name + Broker + Date.ToString() + activeString(Active);
        }

        #endregion
    }
}
