using System.Collections.Generic;

namespace LiftSimulator.Core
{
    public class SimulationResults
    {


        public IList<TickSummary> Summaries { get; set; } = new List<TickSummary>();
    }
}