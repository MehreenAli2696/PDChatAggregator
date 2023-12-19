using ChatAggregator.Domain.Enums;
using ChatAggregator.Domain;
using ChatAggregator.Application.Interfaces;
using ChatAggregator.Domain.ModelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ChatAggregator.Application.Services
{
    public class ReportingService : IReportingService
    {
        private IAggregationService _aggregationService;
        private IChatFetchingService _chatFetchingService;
        public ReportingService(IAggregationService aggregationService, IChatFetchingService chatFetchingService) {
            this._aggregationService = aggregationService;
            this._chatFetchingService = chatFetchingService;
        }   
        public ICollection<Tuple<string, string>> GetReport(AggregationForm aggregation)
        {
            if(aggregation.Granularity == Granularity.NoGranularity)
            {
                var chatevents = _chatFetchingService.FetchChatEvents().Where(e => e.Time >= aggregation.StartTime && e.Time <= aggregation.EndTime) ;

                return RenderNonAggregatedReport(chatevents);
            }

            var result = this._aggregationService.GetAggregatedResults(aggregation);

            return RenderAggregatedReport(result, aggregation.Granularity);
        }

        private ICollection<Tuple<string, string>> RenderNonAggregatedReport(IEnumerable<ChatEvent> chatevents)
        {
            var report = new List<Tuple<string, string>>();
            var value = new StringBuilder();

            foreach(ChatEvent chatEvent in chatevents)
            {
                report.Add(new Tuple<string, string>($"{chatEvent.Time.ToString("dd/MM/yyyy")} - {chatEvent.Time.Hour}:{chatEvent.Time.Minute.ToString("00")}:{chatEvent.Time.Millisecond.ToString("00")}", ChatEventToString(chatEvent)));
            }
            
            return report;
        }

        private ICollection<Tuple<string, string>> RenderAggregatedReport (Dictionary<string, List<ChatEvent>> aggregatedEvents, Granularity granularity)
        {
            //StringBuilder result = new StringBuilder();
            var report = new List<Tuple<string, string>>();

            foreach (var aggregation in aggregatedEvents)
            {
                var value = new StringBuilder();
                switch(granularity)
                {
                    case Granularity.ByMinute:
                        foreach (var eventItem in aggregation.Value)
                        {
                            value.AppendLine($"{eventItem.Time.ToString("dd/MM/yyyy")} - {eventItem.Time.Hour}:{eventItem.Time.Minute.ToString("00")}:{eventItem.Time.Millisecond.ToString("00")} : {ChatEventToString(eventItem)}");
                        }
                        break;
                    case Granularity.Hourly:
                        var events = aggregation.Value;

                        var enters = events.Count(e => e.Type == ChatEventType.EnterRoom);
                        var leaves = events.Count(e => e.Type == ChatEventType.EnterRoom);
                        var comments = events.Count(e => e.Type == ChatEventType.Comment);
                        var highFivesSender = events.Where(e => e.Type == ChatEventType.HighFive).Select(e => e.SenderName)
                            .Distinct().Count();
                        var highFivesOtherUser = events.Where(e => e.Type == ChatEventType.HighFive).Select(e => e.RecieverName)
                            .Distinct().Count();

                        if (enters > 0)
                            value.AppendLine($"\t{enters} person(s) entered");
                        if (leaves > 0)
                            value.AppendLine($"\t{leaves} person(s) left");
                        if (highFivesSender > 0)
                            value.AppendLine($"\t{highFivesSender} person(s) high-fived {highFivesOtherUser} other person(s)");
                        if (comments > 0)
                            value.AppendLine($"\t{comments} comment(s)");
                        break;
                }

                report.Add(new Tuple<string,string>(aggregation.Key, value.ToString()));

            }

            return report;
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
                    return $"{singleEvent.SenderName} high-fives {singleEvent.RecieverName}";

                default:
                    throw new ArgumentException("Chat Event not recognized");
            }

        }
    }
}
