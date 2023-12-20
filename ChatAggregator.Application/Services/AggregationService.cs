using ChatAggregator.Application.Interfaces;
using ChatAggregator.Domain.ModelEntities;
using ChatAggregator.Domain.Enums;

namespace ChatAggregator.Application.Services
{
    public class AggregationService : IAggregationService
    {
        private readonly IChatFetchingService _chatFetchingService;

        public AggregationService(IChatFetchingService chatFetchingService)
        {
            _chatFetchingService = chatFetchingService;
        }

        public Dictionary<string, List<ChatEvent>> GetAggregatedResults(AggregationForm aggregationForm)
        {
            ArgumentNullException.ThrowIfNull(aggregationForm);

            var events = _chatFetchingService.FetchChatEvents();

            return aggregationForm.Granularity switch
            {
                Granularity.Minute => events
                    .Where(e => e.Time >= aggregationForm.StartTime && e.Time <= aggregationForm.EndTime)
                    .GroupBy(e => new { e.Time.Date, e.Time.Hour, e.Time.Minute })
                    .ToDictionary(g => $"{g.Key.Date:dd/MM/yyyy} - {g.Key.Hour}:{g.Key.Minute:00}", g => g.ToList()),
                Granularity.Hour => events
                    .Where(e => e.Time >= aggregationForm.StartTime && e.Time <= aggregationForm.EndTime)
                    .GroupBy(e => new { e.Time.Date, e.Time.Hour })
                    .ToDictionary(g => $"{g.Key.Date:dd/MM/yyyy} - {g.Key.Hour}:00", g => g.ToList()),
                Granularity.None =>
                    throw new ArgumentOutOfRangeException(),
                _ => throw new ArgumentException($"Invalid argument value - {aggregationForm}")
            };
        }
    }
}
