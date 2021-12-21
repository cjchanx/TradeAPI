using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TradingLibrary.Models
{
    public class Orders
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
        public Orders(int Id,int AccountRef, int Action, double TargetPrice, DateTime date,int quantity, int status, String symbol)
        {
            this.Id = Id;
            this.AccountRef = AccountRef;
            this.Action = Action;
            this.TargetPrice = TargetPrice;
            this.DateCreated = date;
            this.Quantity = quantity;
            this.Status = status;
            this.Symbol = symbol;
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

        [JsonProperty(PropertyName = "targetprice")]
        public double TargetPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "datecreated")]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "quantity")]
        public int Quantity { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public int Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "symbol")]
        public String Symbol { get; set; }

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
