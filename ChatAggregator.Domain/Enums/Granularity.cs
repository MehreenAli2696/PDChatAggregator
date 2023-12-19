using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAggregator.Domain.Enums
{
    public enum Granularity
    {
        NoGranularity = 0,
        ByMinute = 1,
        Hourly = 2
    }
}
