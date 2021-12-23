using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Models
{
    public class OwnedSecurity_db
    {
        #region Constructors
        /// <summary>
        /// Default constructor for serialization.
        /// </summary>
        public OwnedSecurity_db() { }

        /// <summary>
        /// orders
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="AccountRef"></param>
        /// <param name="Action"></param>
        /// <param name="DateCreated"></param>
        /// <param name="Quantity"></param>
        /// <param name="Status"></param>
        /// <param name="Symbol"></param>
        public OwnedSecurity_db(int AccountRef, string Symbol, int Quantity, double AveragePrice)
        {
            this.AccountRef = AccountRef;
            this.Symbol = Symbol;
            this.Quantity = Quantity;
            this.AveragePrice = AveragePrice;
        }

        #endregion

        #region Properties

        public int AccountRef { get; set; }

        public string Symbol { get; set; }

        public int Quantity { get; set; }

        public double AveragePrice { get; set; }

        #endregion
    }
}
