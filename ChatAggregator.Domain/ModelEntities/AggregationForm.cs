using ChatAggregator.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ChatAggregator.Domain.ModelEntities
{
    public class AggregationForm
    {
        public Granularity Granularity {get;set ;}
        [Required]
        public DateTime StartTime { get;set ;}
        [Required]
        public DateTime EndTime { get;set ;}   

    }
}
