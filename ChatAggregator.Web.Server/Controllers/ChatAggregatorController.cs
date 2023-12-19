using ChatAggregator.Domain.Enums;
using ChatAggregator.Domain.ModelEntities;
using ChatAggregator.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChatAggregator.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatAggregatorController : ControllerBase
    {
        private IReportingService _reportingService;
        public ChatAggregatorController(IReportingService reportingService) {
            this._reportingService = reportingService;
        }
        [HttpGet(Name = "GetAggregatedChatReport")]
        public ICollection<Tuple<string, string>> GetChatReport(Granularity granularity, DateTime startTime, DateTime endTime) {
            var aggregationForm = new AggregationForm() { Granularity = granularity, StartTime = startTime, EndTime = endTime };
            return _reportingService.GetReport(aggregationForm);
        }
    }
}
