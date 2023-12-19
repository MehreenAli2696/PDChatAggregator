using ChatAggregator.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAggregator.Domain.ModelEntities
{
    public class AggregationForm
    {
        public Granularity Granularity {get;set ;}
        public DateTime StartTime { get;set ;} 
        public DateTime EndTime { get;set ;}   

    }
}
