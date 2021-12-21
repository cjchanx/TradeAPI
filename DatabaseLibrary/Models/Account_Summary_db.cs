using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Models
{
    public class Account_Summary_db
    {
        #region Constructors
        /// <summary>
        /// Default constructor for serialization.
        /// </summary>
        public Account_Summary_db() { }

        /// <summary>
        /// Account_summary
        /// </summary>
        /// <param name="AccountRef"></param>
        /// <param name="AvailableFunds"></param>
        /// <param name="GrossPositionValue"></param>
        /// <param name="NetLiquidation"></param>
        public Account_Summary_db(int AccountRef, double AvailableFunds, double GrossPositionValue, double NetLiquidation)
        {
            this.AccountRef = AccountRef;
            this.AvailableFunds = AvailableFunds;
            this.GrossPositionValue = GrossPositionValue;
            this.NetLiquidation = NetLiquidation;
        }

        #endregion

        #region Properties

        public int AccountRef { get; set; }

        public double AvailableFunds { get; set; }

        public double GrossPositionValue { get; set; }

        public double NetLiquidation { get; set; }



        #endregion
    }
}
