using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingLibrary.Enums
{
    public enum OrderAction
    {
        Upper_limit_sell = 0, // For sell orders where the limit is higher than the current
        Lower_limit_sell = 1, // For sell orders where the limit is lower than the current
        Upper_limit_buy = 2, // For buy orders where the limit is higher than the current
        Lower_limit_buy = 3 // For buy orders where the limit is lower than the current
    }
}
