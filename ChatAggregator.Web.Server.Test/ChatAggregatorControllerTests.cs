using ChatAggregator.Application.Interfaces;
using ChatAggregator.Domain.Enums;
using ChatAggregator.Domain.ModelEntities;
using ChatAggregator.Server.Controllers;
using Moq;

namespace ChatAggregator.Server.Test
{
    public class ChatAggregatorControllerTests
    {
        private Mock<IReportingService> _reportingServiceMock;
        private readonly ChatAggregatorController _chatAggregatorController;

        public ChatAggregatorControllerTests()
        {
            _reportingServiceMock = new Mock<IReportingService>();
            _chatAggregatorController = new ChatAggregatorController(_reportingServiceMock.Object);
        }
        [Fact]
        public void GetChatReport_Should_Return_Expected_Report()
        {
            var expectedReport = new List<ChatEventResult>() { new ChatEventResult() { Time = "Expected Key", EventReport = "Expected Value" } };
            var aggregation = new Domain.ModelEntities.AggregationForm() { StartTime = DateTime.Parse("2023-12-16 13:00:00"), EndTime = DateTime.Parse("2023-12-18 13:00:00"), Granularity = Granularity.None };
            _reportingServiceMock.Setup(r => r.GetReport(It.IsAny<AggregationForm>())).Returns(expectedReport);

            var result = _chatAggregatorController.GetChatReport(aggregation.Granularity, aggregation.StartTime, aggregation.EndTime);

            Assert.Equivalent(expectedReport, result);
        }
    }
}