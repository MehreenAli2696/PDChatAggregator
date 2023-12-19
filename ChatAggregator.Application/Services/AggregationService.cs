using ChatAggregator.Application.Interfaces;
using ChatAggregator.Domain.ModelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatAggregator.Domain.Enums;

namespace ChatAggregator.Application.Services
{
    public class AggregationService : IAggregationService
    {
        private IChatFetchingService _chatFetchingService;
        public AggregationService(IChatFetchingService chatFetchingService) {
            this._chatFetchingService = chatFetchingService;
        }
        public Dictionary<string, List<ChatEvent>> GetAggregatedResults(AggregationForm aggregationForm)
        {
            if(aggregationForm == null)
            {
                throw new ArgumentNullException("aggregationForm");
            }

            var events = this._chatFetchingService.FetchChatEvents();
            switch (aggregationForm.Granularity)
            {
                case Granularity.ByMinute:
                    return events
                        .Where(e => e.Time >= aggregationForm.StartTime && e.Time <= aggregationForm.EndTime)
                        .GroupBy(e => new { e.Time.Date, e.Time.Hour, e.Time.Minute })
                        .ToDictionary(g => $"{g.Key.Date.ToString("dd/MM/yyyy")} - {g.Key.Hour}:{g.Key.Minute.ToString("00")}", g => g.ToList()) ;   
                    
                case Granularity.Hourly:
                    return events
                        .Where(e => e.Time >= aggregationForm.StartTime && e.Time <= aggregationForm.EndTime)
                        .GroupBy(e => new { e.Time.Date, e.Time.Hour })
                        .ToDictionary(g => $"{g.Key.Date.ToString("dd/MM/yyyy")} - {g.Key.Hour}:00", g => g.ToList());

                case Granularity.NoGranularity:
                    //TO-DO
                    throw new ArgumentOutOfRangeException();
            }

            throw new ArgumentException($"Invalid argument value - {aggregationForm.ToString()}");
        }
    }
}
