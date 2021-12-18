using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TradingLibrary.Models
{
    public class Brokers
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="website"></param>
        public Brokers(String name, String website)
        {
            Name = name;
            Website = website;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "Name")]
        public String Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "Website")]
        public String Website { get; set; }


        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name + Website;
        }

        #endregion
    }
}
