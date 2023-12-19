using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAggregator.Domain.Enums
{
    public enum ChatEventType
    {
        EnterRoom = 0,
        LeaveRoom = 1, 
        Comment = 2,
        HighFive = 3
    }
}
// one that gets all the events from DB in the form of chatevents'
// A service that gets the aggregation according to the specification 
// specification that can be defined in form of enum
// 