using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Models
{
    public class Security_db
    {
        #region Constructors
        /// <summary>
        /// Default constructor for serialization.
        /// </summary>
        public Security_db() { }

        /// <summary>
        /// security
        /// </summary>
        /// <param name="Symbol"></param>
        /// <param name="Description"></param>
        /// <param name="Price"></param>
        public Security_db(string Symbol, string Description, double Price)
        {
            this.Symbol = Symbol;
            this.Description = Description;
            this.Price = Price;
        }

        #endregion

        #region Properties

        public string Symbol { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        #endregion
    }
}
