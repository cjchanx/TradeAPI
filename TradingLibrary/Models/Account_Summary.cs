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
        public Account_Summary(int AccountRef, float AvailableFunds, float GrossPositionValue, float NetLiquidation)
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
        [JsonProperty(PropertyName = "AccountRef")]
        public int AccountRef { get; set; }

        /// <summary>
        /// Available Funds
        /// </summary>
        [JsonProperty(PropertyName = "AvailableFunds")]
        public float AvailableFunds { get; set; }

        /// <summary>
        /// Gross Position
        /// </summary>
        [JsonProperty(PropertyName = "GrossPositionValue")]
        public float GrossPositionValue { get; set; }

        /// <summary>
        /// Net Liquidation
        /// </summary>
        [JsonProperty(PropertyName = "NetLiquidation")]
        public float NetLiquidation { get; set; }

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
