using ChatAggregator.Domain.Enums;

namespace ChatAggregator.Domain.ModelEntities
{
    public class ChatEvent
    {
        public ChatEventType Type { get; set; } 
        public DateTime Time { get; set; }
        public string SenderName { get; set; } = string.Empty;
        public string? Message { get; set; }
        public string? ReceiverName { get; set; }    
    }
}
