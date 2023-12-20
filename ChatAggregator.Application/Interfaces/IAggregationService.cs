using ChatAggregator.Domain.ModelEntities;

namespace ChatAggregator.Application.Interfaces
{
    public interface IAggregationService
    {
        Dictionary<string, List<ChatEvent>> GetAggregatedResults(AggregationForm aggregationForm);
        
    }
}
