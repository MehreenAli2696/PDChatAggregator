using ChatAggregator.Domain.ModelEntities;

namespace ChatAggregator.Application.Interfaces
{
    public interface IChatFetchingService
    {
        IEnumerable<ChatEvent> FetchChatEvents();
    }
}
