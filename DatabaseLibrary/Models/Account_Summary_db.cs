using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Models
{
    public class Account_Summarydb
    {
        #region Constructors
        /// <summary>
        /// Default constructor for serialization.
        /// </summary>
        public Account_Summarydb() { }

        /// <summary>
        /// Account_summary
        /// </summary>
        /// <param name="AccountRef"></param>
        /// <param name="AvailableFunds"></param>
        /// <param name="GrossPositionValue"></param>
        /// <param name="NetLiquidation"></param>
        public Account_Summarydb(int AccountRef, float AvailableFunds, float GrossPositionValue, float NetLiquidation)
        {
            this.AccountRef = AccountRef;
            this.AvailableFunds = AvailableFunds;
            this.GrossPositionValue = GrossPositionValue;
            this.NetLiquidation = NetLiquidation;
        }

        #endregion

        #region Properties

        public int AccountRef { get; set; }

        public float AvailableFunds { get; set; }

        public float GrossPositionValue { get; set; }

        public float NetLiquidation { get; set; }



        #endregion
    }
}
