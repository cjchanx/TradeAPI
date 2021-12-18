using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TradingLibrary.Models
{
    public class Security
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="website"></param>
        public Security(String symbol, float price)
        {
            this.Symbol = symbol;
            this.Price = price;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "symbol")]
        public String Symbol { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "price")]
        public float Price { get; set; }


        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Symbol;
        }

        #endregion
    }
}
