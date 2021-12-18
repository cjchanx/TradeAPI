using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Models
{
    internal class Commission_db
    {
        #region Constructors
        public Commission_db() { }

        public Commission_db(string broker, int type, double rate)
        {
            this.Broker = broker;
            this.Type = type;   
            this.Rate = rate;
        }
        #endregion

        #region Properties
        public string Broker { get; set; }
        public int Type { get; set; }
        public double Rate { get; set; }
        #endregion
    }
}
