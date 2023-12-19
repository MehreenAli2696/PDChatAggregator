using ChatAggregator.Domain.Enums;
using ChatAggregator.Domain.ModelEntities;
using ChatAggregator.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAggregator.Infrastructure.Fetcher
{
    public class ChatFetchingService : IChatFetchingService
    {
        public IEnumerable<ChatEvent> FetchChatEvents()
        {
            var chatevents = new List<ChatEvent>();
            
            chatevents.Add(new ChatEvent() { Time = DateTime.Parse("2023-12-17 13:00:00"), SenderName = "Kate", Type = ChatEventType.EnterRoom});
            chatevents.Add(new ChatEvent() { Time = DateTime.Parse("2023-12-17 13:05:00"), SenderName = "Bob", Type = ChatEventType.EnterRoom });
            chatevents.Add(new ChatEvent() { Time = DateTime.Parse("2023-12-17 13:15:00"), SenderName = "Bob", Type = ChatEventType.Comment, Message = "Hey, Kate - high five?", RecieverName = "Kate" });
            chatevents.Add(new ChatEvent() { Time = DateTime.Parse("2023-12-17 13:17:00"), SenderName = "Kate", Type = ChatEventType.HighFive, RecieverName= "Bob"});
            chatevents.Add(new ChatEvent() { Time = DateTime.Parse("2023-12-17 13:18:00"), SenderName = "Bob", Type = ChatEventType.LeaveRoom });
            chatevents.Add(new ChatEvent() { Time = DateTime.Parse("2023-12-17 13:20:00"), SenderName = "Kate", Type = ChatEventType.Comment, Message = "Oh, typical" });
            chatevents.Add(new ChatEvent() { Time = DateTime.Parse("2023-12-17 13:25:00"), SenderName = "Kate", Type = ChatEventType.LeaveRoom });

            chatevents.Add(new ChatEvent() { Time = DateTime.Parse("2023-12-17 14:00:00"), SenderName = "Bill", Type = ChatEventType.EnterRoom });
            chatevents.Add(new ChatEvent() { Time = DateTime.Parse("2023-12-17 14:05:00"), SenderName = "Tom", Type = ChatEventType.EnterRoom });
            chatevents.Add(new ChatEvent() { Time = DateTime.Parse("2023-12-17 14:15:00"), SenderName = "Tom", Type = ChatEventType.Comment, Message = "Hey, Bill - high five?", RecieverName = "Bill" });
            chatevents.Add(new ChatEvent() { Time = DateTime.Parse("2023-12-17 14:17:00"), SenderName = "Bill", Type = ChatEventType.HighFive, RecieverName = "Tom" });
            chatevents.Add(new ChatEvent() { Time = DateTime.Parse("2023-12-17 14:20:00"), SenderName = "Tom", Type = ChatEventType.LeaveRoom });
            chatevents.Add(new ChatEvent() { Time = DateTime.Parse("2023-12-17 14:25:00"), SenderName = "Bill", Type = ChatEventType.Comment, Message = "Oh, typical" });
            chatevents.Add(new ChatEvent() { Time = DateTime.Parse("2023-12-17 14:30:00"), SenderName = "Bill", Type = ChatEventType.LeaveRoom });

            chatevents.Add(new ChatEvent() { Time = DateTime.Parse("2023-12-17 15:00:00"), SenderName = "Jill", Type = ChatEventType.EnterRoom });
            chatevents.Add(new ChatEvent() { Time = DateTime.Parse("2023-12-17 15:05:00"), SenderName = "Alex", Type = ChatEventType.EnterRoom });
            chatevents.Add(new ChatEvent() { Time = DateTime.Parse("2023-12-17 15:15:00"), SenderName = "Alex", Type = ChatEventType.Comment, Message = "Hey, Jill - high five?", RecieverName = "Jill" });
            chatevents.Add(new ChatEvent() { Time = DateTime.Parse("2023-12-17 15:17:00"), SenderName = "Jill", Type = ChatEventType.HighFive, RecieverName = "Alex" });
            chatevents.Add(new ChatEvent() { Time = DateTime.Parse("2023-12-17 15:20:00"), SenderName = "Alex", Type = ChatEventType.LeaveRoom });
            chatevents.Add(new ChatEvent() { Time = DateTime.Parse("2023-12-17 15:25:00"), SenderName = "Jill", Type = ChatEventType.Comment, Message = "Oh, typical" });
            chatevents.Add(new ChatEvent() { Time = DateTime.Parse("2023-12-17 15:30:00"), SenderName = "Jill", Type = ChatEventType.LeaveRoom });

            return chatevents;
        }
    }
}
