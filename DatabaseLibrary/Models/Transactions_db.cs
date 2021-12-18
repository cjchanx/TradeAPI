using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Models
{
    public class Transactions_db
    {
        #region Constructors
        /// <summary>
        /// Default constructor for serialization.
        /// </summary>
        public Transactions_db() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="AccountRef"></param>
        /// <param name="Action"></param>
        /// <param name="AveragePrice"></param>
        /// <param name="Commission"></param>
        /// <param name="DateCreated"></param>
        /// <param name="Price"></param>
        /// <param name="Quantity"></param>
        /// <param name="RealizedPNL"></param>
        public Transactions_db(int Id, int AccountRef, int Action, float AveragePrice, float Commission, DateTime DateCreated, float Price, int Quantity, float RealizedPNL)
        {
            this.Id = Id;
            this.AccountRef = AccountRef;
            this.Action = Action;
            this.AveragePrice = AveragePrice;
            this.Commission = Commission;
            this.DateCreated = DateCreated;
            this.Price = Price;
            this.Quantity = Quantity;
            this.RealizedPNL = RealizedPNL;

        }

        #endregion

        #region Properties
        public int Id { get; set; }

        public int AccountRef { get; set; }

        public int Action { get; set; }

        public float AveragePrice { get; set; }

        public float Commission { get; set; }

        public DateTime DateCreated { get; set; }


        public float Price { get; set; }

        public int Quantity { get; set; }

        public float RealizedPNL { get; set; }


        #endregion
    }
}
