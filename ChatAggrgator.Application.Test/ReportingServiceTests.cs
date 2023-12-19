using ChatAggregator.Domain.Enums;
using ChatAggregator.Domain.ModelEntities;
using ChatAggregator.Application.Interfaces;
using ChatAggregator.Application.Services;

namespace ChatAggregator.Domain.Test
{
    public class ReportingServiceTests
    {
        private readonly IReportingService _reportingService;
        private readonly Mock<IChatFetchingService> _chatFetchingServiceMock;
        private readonly Mock<IAggregationService> _aggregateServiceMock;
        public ReportingServiceTests() {
            _aggregateServiceMock = new Mock<IAggregationService>();
            _chatFetchingServiceMock = new Mock<IChatFetchingService>();
            _reportingService = new ReportingService(_aggregateServiceMock.Object, _chatFetchingServiceMock.Object);
        }

        [Fact]
        public void GetReport_Should_Return_Minute_Report_When_ByMinute_Granularity()
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
            var aggEvents = new Dictionary<string, List<ChatEvent>>() {

                { "17/12/2023 - 13:00", new List<ChatEvent>() { new ChatEvent() { Time = DateTime.Parse("2023-12-17 13:00:00"), SenderName = "Kate", Type = ChatEventType.EnterRoom } } },
                { "17/12/2023 - 13:05", new List<ChatEvent>() { new ChatEvent() { Time = DateTime.Parse("2023-12-17 13:05:00"), SenderName = "Bob", Type = ChatEventType.EnterRoom } } },
                { "17/12/2023 - 13:15", new List<ChatEvent>() { new ChatEvent() { Time = DateTime.Parse("2023-12-17 13:15:00"), SenderName = "Bob", Type = ChatEventType.Comment, Message = "Hey, Kate - high five?", RecieverName = "Kate" } } },
                { "17/12/2023 - 13:17", new List<ChatEvent>() { new ChatEvent() { Time = DateTime.Parse("2023-12-17 13:17:00"), SenderName = "Kate", Type = ChatEventType.HighFive, RecieverName = "Bob" } } },
                { "17/12/2023 - 13:20", new List<ChatEvent>() { new ChatEvent() { Time = DateTime.Parse("2023-12-17 13:20:00"), SenderName = "Kate", Type = ChatEventType.LeaveRoom } } },
                { "17/12/2023 - 13:25", new List<ChatEvent>() { new ChatEvent() { Time = DateTime.Parse("2023-12-17 13:25:00"), SenderName = "Bob", Type = ChatEventType.LeaveRoom } } }
            };

            var aggregation = new AggregationForm() { StartTime = DateTime.Parse("2023-12-16 13:00:00"), EndTime = DateTime.Parse("2023-12-18 13:00:00"), Granularity = Granularity.Minute };
            _chatFetchingServiceMock.Setup(fs => fs.FetchChatEvents()).Returns(events);
            _aggregateServiceMock.Setup(ags => ags.GetAggregatedResults(aggregation)).Returns(aggEvents);

            var report = _reportingService.GetReport(aggregation);
            Assert.NotNull(report);
            Assert.Contains("Kate enters the room", report.First().EventReport);
            Assert.Contains("Bob leaves", report.Last().EventReport);
        }

        [Fact]
        public void GetReport_Should_Return_Hour_Report_When_Hourly_Granularity()
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
            var aggEvents = new Dictionary<string, List<ChatEvent>>() {


                { "17/12/2023 - 13:00", new List<ChatEvent>() { new ChatEvent() { Time = DateTime.Parse("2023-12-17 13:00:00"), SenderName = "Kate", Type = ChatEventType.EnterRoom },
                 new ChatEvent() { Time = DateTime.Parse("2023-12-17 13:05:00"), SenderName = "Bob", Type = ChatEventType.EnterRoom } ,
                 new ChatEvent() { Time = DateTime.Parse("2023-12-17 13:15:00"), SenderName = "Bob", Type = ChatEventType.Comment, Message = "Hey, Kate - high five?", RecieverName = "Kate" },
                 new ChatEvent() { Time = DateTime.Parse("2023-12-17 13:17:00"), SenderName = "Kate", Type = ChatEventType.HighFive, RecieverName = "Bob" },
                 new ChatEvent() { Time = DateTime.Parse("2023-12-17 13:20:00"), SenderName = "Kate", Type = ChatEventType.LeaveRoom },
                 new ChatEvent() { Time = DateTime.Parse("2023-12-17 13:25:00"), SenderName = "Bob", Type = ChatEventType.LeaveRoom } } }
            };

            var aggregation = new AggregationForm() { StartTime = DateTime.Parse("2023-12-16 13:00:00"), EndTime = DateTime.Parse("2023-12-18 13:00:00"), Granularity = Granularity.Hour };
            _chatFetchingServiceMock.Setup(fs => fs.FetchChatEvents()).Returns(events);
            _aggregateServiceMock.Setup(ags => ags.GetAggregatedResults(aggregation)).Returns(aggEvents);

            var report = _reportingService.GetReport(aggregation);
            Assert.NotNull(report);
            Assert.Single(report);
            Assert.Contains("2 person(s) entered", report.First().EventReport);
            Assert.Contains("2 person(s) left", report.First().EventReport);
            Assert.Contains("1 person(s) high-fived 1 other person(s)", report.First().EventReport);

        }

        [Fact]
        public void GetReport_Should_Return_As_Report_When_NoGranularity()
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

            var aggregation = new AggregationForm() { StartTime = DateTime.Parse("2023-12-16 13:00:00"), EndTime = DateTime.Parse("2023-12-18 13:00:00"), Granularity = Granularity.None };
            _chatFetchingServiceMock.Setup(fs => fs.FetchChatEvents()).Returns(events);
            //_aggregateServiceMock.Setup(ags => ags.GetAggregatedResults(aggregation)).Returns(aggEvents);

            var report = _reportingService.GetReport(aggregation);

            Assert.NotNull(report);
            Assert.Contains("Kate enters the room", report.First().EventReport);
            Assert.Contains("Bob leaves", report.Last().EventReport);
        }

    }
}
