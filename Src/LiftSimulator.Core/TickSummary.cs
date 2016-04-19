using System.Collections.Generic;

namespace LiftSimulator.Core
{
    public class TickSummary
    {
        public int Tick { get; set; }
        public SimulationContext Context { get; set; }
        public IList<string> Message { get; set; } = new List<string>();
    }
}