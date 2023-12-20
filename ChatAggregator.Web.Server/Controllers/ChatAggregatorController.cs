using ChatAggregator.Application.Interfaces;
using ChatAggregator.Domain.Enums;
using ChatAggregator.Domain.ModelEntities;
using Microsoft.AspNetCore.Mvc;

namespace ChatAggregator.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatAggregatorController : ControllerBase
    {
        private readonly IReportingService _reportingService;
        public ChatAggregatorController(IReportingService reportingService)
        {
            _reportingService = reportingService;
        }

        [HttpGet(Name = "GetAggregatedChatReport")]
        public ICollection<ChatEventResult> GetChatReport(Granularity granularity, DateTime startTime, DateTime endTime)
        {

            var aggregationForm = new AggregationForm
            {
                Granularity = granularity,
                StartTime = startTime,
                EndTime = endTime
            };
            return _reportingService.GetReport(aggregationForm);
        }
    }
}
