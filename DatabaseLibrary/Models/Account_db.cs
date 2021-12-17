using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Models
{
    public class Account_db
    {
        #region Constructors
        /// <summary>
        /// Default constructor for serialization.
        /// </summary>
        public Account_db() { }

        /// <summary>
        /// Account constructor with parameters.
        /// </summary>
        /// <param name="active"></param>
        /// <param name="broker"></param>
        /// <param name="date"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public Account_db(bool active, string broker, DateTime date, string name, string desc)
        {
            Active = active;
            Broker = broker;
            Date = date;
            Name = name;
            Description = desc;
        }

        #endregion

        #region Properties

        public bool Active { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Broker { get; set; }

        public string Description { get; set; }

        #endregion
    }
}
