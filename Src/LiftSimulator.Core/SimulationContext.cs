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

        public IList<LiftRequest> Requests { get; set; }
        public IEnumerable<Lift> Lifts { get; set; }

        public IEnumerable<Level> Levels
        {
            get
            {
                return
                    from level in Enumerable.Range(Lift.MinimumFloor, Lift.MaximumFloor)
                    let waiting = Requests.Where(req => req.SourceFloorNumber == level && !req.IsServiced()).Sum(req => req.PeopleCount)
                    select new Level()
                           {
                               Number = level,
                               Waiting = waiting
                           };
            }
        }
    }

    public class Level
    {
        public int Number { get; set; }
        public int Waiting { get; set; }
    }
}