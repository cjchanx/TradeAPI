using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TradingLibrary.Models
{
    public class Commission
    {
        #region Constructors

        public Commission(string broker, int type, double rate)
        {
            this.Broker = broker;
            this.Type = type;
            this.Rate = rate;
        }

        #endregion

        #region Properties
        [JsonProperty(PropertyName = "broker")]
        public string Broker { get; set; }

        [JsonProperty(PropertyName = "type")]
        public int Type { get; set; }

        [JsonProperty(PropertyName = "rate")]
        public double Rate{ get; set; }

        #endregion

        #region Methods
        public override string ToString()
        {
            return Broker + Type + Rate;
        }

        #endregion
    }
}
