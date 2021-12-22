using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TradingLibrary.Models
{
    public class OwnedSecurity
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AccountRef"></param>
        /// <param name="Symbol"></param>
        /// <param name="Quantity"></param>
        /// <param name="AveragePrice"></param>
        public OwnedSecurity(int AccountRef, string Symbol, int Quantity, double AveragePrice)
        {
            this.AccountRef = AccountRef;
            this.Symbol = Symbol;
            this.Quantity = Quantity;
            this.AveragePrice = AveragePrice;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "accountref")]
        public int AccountRef { get; set; }

        [JsonProperty(PropertyName = "symbol")]
        public string Symbol { get; set; }

        [JsonProperty(PropertyName = "quantity")]
        public int Quantity { get; set; }

        [JsonProperty(PropertyName = "averageprice")]
        public double AveragePrice { get; set; }

        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return AccountRef.ToString() + Symbol.ToString();
        }

        #endregion
    }
}
