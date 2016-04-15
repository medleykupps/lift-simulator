using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LiftSimulator.Core.Models;

namespace LiftSimulator.Core
{
    public class SimulationContext
    {
        public SimulationContext()
        {            
            Requests = new List<LiftRequest>();
            Lifts = new List<Lift>();
        }

        public SimulationContext(IEnumerable<LiftRequest> requests, IEnumerable<Lift> lifts)
        {
            Requests = requests.ToList();
            Lifts = lifts.ToList();
        }

        public IEnumerable<LiftRequest> Requests { get; set; }
        public IEnumerable<Lift> Lifts { get; set; }
    }
}