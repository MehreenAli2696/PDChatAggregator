using ChatAggregator.Domain.Enums;
using ChatAggregator.Application.Interfaces;
using ChatAggregator.Domain.ModelEntities;
using System.Text;

namespace ChatAggregator.Application.Services
{
    public class ReportingService : IReportingService
    {
        private readonly IAggregationService _aggregationService;
        private readonly IChatFetchingService _chatFetchingService;

        public ReportingService(IAggregationService aggregationService, IChatFetchingService chatFetchingService)
        {
            _aggregationService = aggregationService;
            _chatFetchingService = chatFetchingService;
        }

        public ICollection<ChatEventResult> GetReport(AggregationForm aggregation)
        {
            if (aggregation.Granularity == Granularity.None)
            {
                var chatEvents = _chatFetchingService.FetchChatEvents()
                    .Where(e => e.Time >= aggregation.StartTime && e.Time <= aggregation.EndTime);

                return RenderNonAggregatedReport(chatEvents);
            }

            var result = _aggregationService.GetAggregatedResults(aggregation);

            return RenderAggregatedReport(result, aggregation.Granularity);
        }

        private List<ChatEventResult> RenderNonAggregatedReport(IEnumerable<ChatEvent> chatEvents)
        {
            return chatEvents.Select(chatEvent => new ChatEventResult { Time = $"{chatEvent.Time:dd/MM/yyyy} - {chatEvent.Time.Hour}:{chatEvent.Time.Minute:00}:{chatEvent.Time.Millisecond:00}", EventReport = ChatEventToString(chatEvent) }).ToList();
        }

        private ICollection<ChatEventResult> RenderAggregatedReport(Dictionary<string, List<ChatEvent>> aggregatedEvents, Granularity granularity)
        {
            ArgumentNullException.ThrowIfNull(aggregatedEvents);

            var report = new List<ChatEventResult>();
            foreach (var aggregation in aggregatedEvents)
            {
                string value = granularity switch
                {
                    Granularity.Minute => RenderMinuteAggregation(aggregation.Value),
                    Granularity.Hour => RenderHourAggregation(aggregation.Value),
                    _ => throw new ArgumentOutOfRangeException(nameof(granularity))
                };
                report.Add(new ChatEventResult() { Time = aggregation.Key, EventReport = value });
            }
            return report;
        }

        private string RenderMinuteAggregation(List<ChatEvent> events)
        {
            var sb = new StringBuilder();
            foreach (var eventItem in events)
            {
                sb.AppendLine(ChatEventToString(eventItem));
            }
            return sb.ToString();
        }

        private string RenderHourAggregation(List<ChatEvent> events)
        {
            var sb = new StringBuilder();
            var enters = events.Count(e => e.Type == ChatEventType.EnterRoom);
            var leaves = events.Count(e => e.Type == ChatEventType.LeaveRoom);
            var comments = events.Count(e => e.Type == ChatEventType.Comment);
            var highFivesSender = events.Where(e => e.Type == ChatEventType.HighFive).Select(e => e.SenderName)
                             .Distinct().Count();
            var highFivesOtherUser = events.Where(e => e.Type == ChatEventType.HighFive).Select(e => e.ReceiverName)
                             .Distinct().Count();

            sb.AppendLineIf(enters > 0, $"{enters} person(s) entered");
            sb.AppendLineIf(leaves > 0, $"{leaves} person(s) left");
            sb.AppendLineIf(highFivesSender > 0, $"{highFivesSender} person(s) high-fived {highFivesOtherUser} other person(s)");
            sb.AppendLineIf(comments > 0, $"{comments} comment(s)");

            return sb.ToString();
        }

        private string ChatEventToString(ChatEvent singleEvent)
        {
            switch (singleEvent.Type)
            {
                case ChatEventType.EnterRoom:
                    return $"{singleEvent.SenderName} enters the room";

                case ChatEventType.LeaveRoom:
                    return $"{singleEvent.SenderName} leaves";

                case ChatEventType.Comment:
                    return $"{singleEvent.SenderName} comments: \"{singleEvent.Message}\"";

                case ChatEventType.HighFive:
                    return $"{singleEvent.SenderName} high-fives {singleEvent.ReceiverName}";

                default:
                    throw new ArgumentException("Chat Event not recognized");
            }

        }
    }
}
