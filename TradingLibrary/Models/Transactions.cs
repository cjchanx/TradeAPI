using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TradingLibrary.Models
{
    public class Transactions
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AccountRef"></param>
        /// <param name="Action"></param>
        /// <param name="date"></param>
        /// <param name="quantity"></param>
        /// <param name="status"></param>
        /// <param name="symbol"></param>
        public Transactions(int Id, int AccountRef, int Action, double AveragePrice, double Commission, DateTime DateCreated, double Price, int Quantity, double RealizedPNL)
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
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "accountref")]
        public int AccountRef { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "action")]
        public int Action { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "averageprice")]
        public double AveragePrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "commission")]
        public double Commission { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "datecreated")]
        public DateTime DateCreated { get; set; }



        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "price")]
        public double Price { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "realizedpnl")]
        public double RealizedPNL { get; set; }


        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return AccountRef.ToString() + Id.ToString();
        }

        #endregion
    }
}
