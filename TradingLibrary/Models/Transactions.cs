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
        public Transactions(int Id, int AccountRef, int Action, float AveragePrice, float commission, DateTime date,  float price, int orderef, float realizedPNL)
        {
            this.Id = Id;
            this.AccountRef = AccountRef;
            this.Action = Action;
            this.AveragePrice = AveragePrice;
            this.Commission = Commission;
            this.DateCreated = date;
            this.Price = price;
            this.OrderRef = orderef;
            this.RealizedPNL = realizedPNL;

        }

        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "Id")]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "AccountRef")]
        public int AccountRef { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "Action")]
        public int Action { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "AveragePrice")]
        public float AveragePrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "Commission")]
        public float Commission { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "DateCreated")]
        public DateTime DateCreated { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "OrderRef")]
        public int OrderRef { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "Price")]
        public float Price { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "Quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "RealizedPNL")]
        public float RealizedPNL { get; set; }


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
