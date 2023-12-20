using ChatAggregator.Domain.Enums;
using ChatAggregator.Domain.ModelEntities;
using ChatAggregator.Application.Interfaces;

namespace ChatAggregator.Infrastructure.Fetcher
{
    public class ChatFetchingService : IChatFetchingService
    {
        public IEnumerable<ChatEvent> FetchChatEvents()
        {
            var chatevents = new List<ChatEvent>()
            {
                new (){ Time = DateTime.Parse("2023-12-17 13:00:00"), SenderName = "Kate", Type = ChatEventType.EnterRoom},
                new () { Time = DateTime.Parse("2023-12-17 13:00:00"), SenderName = "Kate", Type = ChatEventType.EnterRoom},
                new () { Time = DateTime.Parse("2023-12-17 13:05:00"), SenderName = "Bob", Type = ChatEventType.EnterRoom },
                new () { Time = DateTime.Parse("2023-12-17 13:15:00"), SenderName = "Bob", Type = ChatEventType.Comment, Message = "Hey, Kate - high five?", ReceiverName = "Kate" },
                new () { Time = DateTime.Parse("2023-12-17 13:17:00"), SenderName = "Kate", Type = ChatEventType.HighFive, ReceiverName = "Bob" },
                new () { Time = DateTime.Parse("2023-12-17 13:18:00"), SenderName = "Bob", Type = ChatEventType.LeaveRoom },
                new () { Time = DateTime.Parse("2023-12-17 13:20:00"), SenderName = "Kate", Type = ChatEventType.Comment, Message = "Oh, typical" },
                new () { Time = DateTime.Parse("2023-12-17 13:25:00"), SenderName = "Kate", Type = ChatEventType.LeaveRoom },

                new () { Time = DateTime.Parse("2023-12-17 14:00:00"), SenderName = "Bill", Type = ChatEventType.EnterRoom },
                new () { Time = DateTime.Parse("2023-12-17 14:05:00"), SenderName = "Tom", Type = ChatEventType.EnterRoom },
                new () { Time = DateTime.Parse("2023-12-17 14:15:00"), SenderName = "Tom", Type = ChatEventType.Comment, Message = "Hey, Bill - high five?", ReceiverName = "Bill" },
                new () { Time = DateTime.Parse("2023-12-17 14:17:00"), SenderName = "Bill", Type = ChatEventType.HighFive, ReceiverName = "Tom" },
                new () { Time = DateTime.Parse("2023-12-17 14:20:00"), SenderName = "Tom", Type = ChatEventType.LeaveRoom },
                new () { Time = DateTime.Parse("2023-12-17 14:25:00"), SenderName = "Bill", Type = ChatEventType.Comment, Message = "Oh, typical" },
                new () { Time = DateTime.Parse("2023-12-17 14:30:00"), SenderName = "Bill", Type = ChatEventType.LeaveRoom },

                new () { Time = DateTime.Parse("2023-12-17 15:00:00"), SenderName = "Jill", Type = ChatEventType.EnterRoom },
                new () { Time = DateTime.Parse("2023-12-17 15:05:00"), SenderName = "Alex", Type = ChatEventType.EnterRoom },
                new () { Time = DateTime.Parse("2023-12-17 15:15:00"), SenderName = "Alex", Type = ChatEventType.Comment, Message = "Hey, Jill - high five?", ReceiverName = "Jill" },
                new () { Time = DateTime.Parse("2023-12-17 15:17:00"), SenderName = "Jill", Type = ChatEventType.HighFive, ReceiverName = "Alex" },
                new () { Time = DateTime.Parse("2023-12-17 15:20:00"), SenderName = "Alex", Type = ChatEventType.LeaveRoom },
                new () { Time = DateTime.Parse("2023-12-17 15:25:00"), SenderName = "Jill", Type = ChatEventType.Comment, Message = "Oh, typical" },
                new () { Time = DateTime.Parse("2023-12-17 15:30:00"), SenderName = "Jill", Type = ChatEventType.LeaveRoom }
            };
            
            return chatevents;
        }
    }
}
