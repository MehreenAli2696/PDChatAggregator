using ChatAggregator.Domain.Enums;
using ChatAggregator.Domain.ModelEntities;
using ChatAggregator.Application.Interfaces;
using ChatAggregator.Application.Services;

namespace ChatAggregator.Domain.Test
{
    public class AggregationServiceTests
    {
        private readonly Mock<IChatFetchingService> _chatFetchingServiceMock;
        private readonly IAggregationService _aggregationService;
        public AggregationServiceTests()
        {
            _chatFetchingServiceMock = new Mock<IChatFetchingService>();
            _aggregationService = new AggregationService(_chatFetchingServiceMock.Object);
        }
        [Fact]
        public void GetAggregatedResults_Should_Return_Minute_Aggregation_When_ByMinute_Granularity()
        {
            var events = new List<ChatEvent>()
            {
                new ChatEvent() { Time = DateTime.Parse("2023-12-17 13:00:00"), SenderName = "Kate", Type = ChatEventType.EnterRoom },
                new ChatEvent() { Time = DateTime.Parse("2023-12-17 13:05:00"), SenderName = "Bob", Type = ChatEventType.EnterRoom },
                new ChatEvent() { Time = DateTime.Parse("2023-12-17 13:15:00"), SenderName = "Bob", Type = ChatEventType.Comment, Message = "Hey, Kate - high five?", RecieverName = "Kate" },
                new ChatEvent() { Time = DateTime.Parse("2023-12-17 13:17:00"), SenderName = "Kate", Type = ChatEventType.HighFive, RecieverName = "Bob" },
                new ChatEvent() { Time = DateTime.Parse("2023-12-17 13:20:00"), SenderName = "Kate", Type = ChatEventType.LeaveRoom },
                new ChatEvent() { Time = DateTime.Parse("2023-12-17 13:25:00"), SenderName = "Bob", Type = ChatEventType.LeaveRoom }
            };
            var firstEventDate = DateTime.Parse("2023-12-17 13:00:00");
            _chatFetchingServiceMock.Setup(fs => fs.FetchChatEvents()).Returns(events);

            var aggResults = _aggregationService.GetAggregatedResults(new AggregationForm() { StartTime = DateTime.Parse("2023-12-16 13:00:00"), EndTime = DateTime.Parse("2023-12-18 13:00:00"), Granularity = Granularity.ByMinute });
            

            Assert.Contains("Kate",aggResults.First().Value.First().SenderName);
            Assert.Equal($"{firstEventDate.ToString("dd/MM/yyyy")} - {firstEventDate.Hour}:{firstEventDate.Minute.ToString("00")}", aggResults.First().Key);
            Assert.Equivalent(new ChatEvent() { Time = DateTime.Parse("2023-12-17 13:25:00"), SenderName = "Bob", Type = ChatEventType.LeaveRoom }, aggResults.Last().Value.First());
        }

        [Fact]
        public void GetAggregatedResults_Should_Return_Hour_Aggregation_When_Hourly_Granularity()
        {
            var events = new List<ChatEvent>()
            {
                new ChatEvent() { Time = DateTime.Parse("2023-12-17 13:00:00"), SenderName = "Kate", Type = ChatEventType.EnterRoom },
                new ChatEvent() { Time = DateTime.Parse("2023-12-17 13:05:00"), SenderName = "Bob", Type = ChatEventType.EnterRoom },
                new ChatEvent() { Time = DateTime.Parse("2023-12-17 13:15:00"), SenderName = "Bob", Type = ChatEventType.Comment, Message = "Hey, Kate - high five?", RecieverName = "Kate" },
                new ChatEvent() { Time = DateTime.Parse("2023-12-17 13:17:00"), SenderName = "Kate", Type = ChatEventType.HighFive, RecieverName = "Bob" },
                new ChatEvent() { Time = DateTime.Parse("2023-12-17 13:20:00"), SenderName = "Kate", Type = ChatEventType.LeaveRoom },
                new ChatEvent() { Time = DateTime.Parse("2023-12-17 13:25:00"), SenderName = "Bob", Type = ChatEventType.LeaveRoom }
            };
            var firstEventDate = DateTime.Parse("2023-12-17 13:00:00");

            _chatFetchingServiceMock.Setup(fs => fs.FetchChatEvents()).Returns(events);

            var aggResults = _aggregationService.GetAggregatedResults(new AggregationForm() { StartTime = DateTime.Parse("2023-12-16 13:00:00"), EndTime = DateTime.Parse("2023-12-18 13:00:00"), Granularity = Granularity.Hourly });

            Assert.Contains("Kate", aggResults.First().Value.First().SenderName.ToString());
            Assert.Equal($"{firstEventDate.ToString("dd/MM/yyyy")} - {firstEventDate.Hour}:00", aggResults.First().Key);
            Assert.Single(aggResults);

        }

        [Fact]
        public void GetAggregatedResults_Should_Return_Error_When_NoGranularity()
        {

        }
    }
}