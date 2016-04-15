using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiftSimulator.Core.Models;

namespace LiftSimulator.Core
{
    internal class SimulationRunner : ISimulationRunner
    {
        public ILiftLocator LiftLocator => new LiftLocator();

        public SimulationResults RunSimulation(SimulationContext context)
        {
            var results = new SimulationResults();

            // loop for each tick
            foreach (var tick in GetFinalTick(context))
            {
                var summary = new TickSummary { Tick = tick };

                // Move all lifts for the new tick
                foreach (var lift in context.Lifts)
                {
                    lift.Update(tick);
                }

                // VERY IMPORTANT Let people off the lift before letting people on!!!

                foreach (var unassigned in GetUnassignedRequests(context))
                {
                    var lift = LiftLocator.FindLiftForRequest(unassigned, context.Lifts);
                    if (lift != null)
                    {
                        // How much Capacty...?
                        if (lift.GetCapacity() + unassigned.PeopleCount <= Lift.MaximumCapacity)
                        {
                            lift.AssignRequest(unassigned, tick);
                        }
                        else
                        {
                            // Create a new Request for the remaining people
                            var roomInLift = Lift.MaximumCapacity - lift.GetCapacity();
                            var remaining = new LiftRequest(unassigned.PeopleCount - roomInLift, unassigned.SourceFloorNumber, unassigned.TargetFloorNumber);
                            context.Requests.Add(remaining);

                            unassigned.PeopleCount -= roomInLift;
                            lift.AssignRequest(unassigned, tick);
                        }
                    }
                }                

                foreach (var lift in context.Lifts)
                {
                    summary.Message.Add($"Lift '{lift.Id}' is at {lift.CurrentFloor}, {lift.GetDirectionDescription()}, with {lift.GetCapacity()} people");
                }

                results.Summaries.Add(summary);
            }



            // any unassigned requests

            // find elevator to service request
            // assign request to elevator
            // check people count and assign partial request if not enough room
            // create a new request for remaining people

            // all requests assigned or elevators full

            // add a summary for the current tick count to the results object

            // update the tick count
            // update all lifts for the new tick count

            return results;
        }

        private IEnumerable<LiftRequest> GetUnassignedRequests(SimulationContext context)
        {
            return context.Requests.Where(req => !req.IsAssigned());
        }

        private IEnumerable<int> GetFinalTick(SimulationContext context)
        {
            var tick = 0;
            while (context.Requests.Any(request => !request.IsComplete()))
            {
                if (tick > 10)
                {
                    yield break;
                }
                yield return tick++;
            }
        }
    }
}
