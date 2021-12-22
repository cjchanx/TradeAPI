using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Models
{
    public class Orders_db
    {
        #region Constructors
        /// <summary>
        /// Default constructor for serialization.
        /// </summary>
        public Orders_db() { }

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
        public Orders_db(int Id, int AccountRef, int Action, double TargetPrice, DateTime DateCreated, int Quantity, int Status, string Symbol, string Broker)
        {
            this.Id = Id;
            this.AccountRef = AccountRef;
            this.Action = Action;
            this.TargetPrice = TargetPrice;
            this.DateCreated = DateCreated;
            this.Quantity = Quantity;
            this.Status = Status;
            this.Symbol = Symbol;
            this.Broker = Broker;
        }

        #endregion

        #region Properties

        public int Id { get; set; }

        public int AccountRef { get; set; }
        public int Action { get; set; }

        public DateTime DateCreated { get; set; }

        public int Quantity { get; set; }

        public int Status { get; set; }

        public string Symbol { get; set; }

        public string Broker { get; set; }

        public double TargetPrice { get; set; }

        #endregion
    }
}
