using System.Collections.Generic;
using LiftSimulator.Core;

namespace LiftSimulator.Models
{
    public class UpdateResult
    {
        public int Tick { get; set; }
        public SimulationContext Context { get; set; }
        public IEnumerable<SummaryItem> SummaryItems { get; set; }
    }
}