using System.Collections.Generic;
using LiftSimulator.Core.Models;

namespace LiftSimulator.Core
{
    public class TickSummary
    {
        public int Tick { get; set; }
        public SimulationContext Context { get; set; }
        public IList<SummaryItem> Items { get; set; } = new List<SummaryItem>();
    }

    public class SummaryItem
    {
        public int Tick { get; set; }
        public string LiftId { get; set; }
        public int PeopleCount { get; set; }
        public int Level { get; set; }
        public LiftAction Action { get; set; }
        public string ActionDesc { get; set; }
        public string Message { get; set; }
    }

    public enum LiftAction
    {
        GetOn,
        GetOff
    }
}