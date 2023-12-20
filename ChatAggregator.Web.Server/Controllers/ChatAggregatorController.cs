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
        // TO-DO
        // May be lets make this a DTO and apply fluent validations here on DTO ?
        // Something like AggregationForm. So that we can get rid of multiple arguments here?
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
