using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TradingLibrary.Models
{
    public class Account_Summary
    {
        #region Constructors

        /// <summary>
        /// Account summary constructor
        /// </summary>
        /// <param name="AccountRef"></param>
        /// <param name="AvailableFunds"></param>
        /// <param name="GrossPositionValue"></param>
        /// <param name="NetLiquidation"></param>
        public Account_Summary(int AccountRef, double AvailableFunds, double GrossPositionValue, double NetLiquidation)
        {
            this.AccountRef = AccountRef;
            this.AvailableFunds = AvailableFunds;
            this.GrossPositionValue = GrossPositionValue;
            this.NetLiquidation = NetLiquidation;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Account Ref
        /// </summary>
        [JsonProperty(PropertyName = "accountref")]
        public int AccountRef { get; set; }

        /// <summary>
        /// Available Funds
        /// </summary>
        [JsonProperty(PropertyName = "availablefunds")]
        public double AvailableFunds { get; set; }

        /// <summary>
        /// Gross Position
        /// </summary>
        [JsonProperty(PropertyName = "grosspositionvalue")]
        public double GrossPositionValue { get; set; }

        /// <summary>
        /// Net Liquidation
        /// </summary>
        [JsonProperty(PropertyName = "netliquidation")]
        public double NetLiquidation { get; set; }

        #endregion

        #region Methods
        /// <summary>
        /// returns a string containing the information of the class
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return AccountRef.ToString();
        }

        #endregion
    }
}
