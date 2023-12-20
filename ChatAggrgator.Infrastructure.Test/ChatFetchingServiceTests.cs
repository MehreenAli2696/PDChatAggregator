using ChatAggregator.Application.Interfaces;
using ChatAggregator.Infrastructure.Fetcher;

namespace ChatAggregator.Infrstructure.Test
{
    public class ChatFetchingServiceTests
    {
        private readonly IChatFetchingService _chatFetchingService;

        public ChatFetchingServiceTests() {
            _chatFetchingService = new ChatFetchingService();
        }
        [Fact]
        public void GetEvents_Should_Return_Events_In_Descending_Order_Of_Time()
        {
            // Fill in the test case after Datafetching logic changes from in memory
        }
    }
}