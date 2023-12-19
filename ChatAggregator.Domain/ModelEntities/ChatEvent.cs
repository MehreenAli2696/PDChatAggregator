using ChatAggregator.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAggregator.Domain.ModelEntities
{
    public class ChatEvent
    {
        public ChatEventType Type { get; set; } 
        public DateTime Time { get; set; }
        public string SenderName { get; set; } = string.Empty;
        public string? Message { get; set; }
        public string? RecieverName { get; set; }    
    }
}
