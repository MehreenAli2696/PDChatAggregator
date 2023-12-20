using ChatAggregator.Domain.ModelEntities;

namespace ChatAggregator.Application.Interfaces
{
    public interface IReportingService
    {
        ICollection<ChatEventResult> GetReport(AggregationForm aggregation);
    }
}
