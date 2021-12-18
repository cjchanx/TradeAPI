using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingLibrary.Enums
{
    public enum OrderAction
    {
        Upper_limit_sell, // For sell orders where the limit is higher than the current
        Lower_limit_sell, // For sell orders where the limit is lower than the current
        Upper_limit_buy, // For buy orders where the limit is higher than the current
        Lower_limit_buy // For buy orders where the limit is lower than the current
    }
}
