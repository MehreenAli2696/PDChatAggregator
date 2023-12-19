﻿using ChatAggregator.Domain.ModelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAggregator.Application.Interfaces
{
    public interface IAggregationService
    {
        Dictionary<string, List<ChatEvent>> GetAggregatedResults(AggregationForm aggregationForm);
        
    }
}