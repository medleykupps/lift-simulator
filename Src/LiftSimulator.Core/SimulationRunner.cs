using System.Collections.Generic;
using System.Linq;
using LiftSimulator.Core.Models;

namespace LiftSimulator.Core
{
    internal class SimulationRunner : ISimulationRunner
    {
        public ILiftLocator LiftLocator => new LiftLocator();

        public SimulationResults RunSimulation(SimulationContext context)
        {
            var results = new SimulationResults();

            foreach (var tick in GetFinalTick(context))
            {
                var summary = new TickSummary { Tick = tick };

                var allRequestsProcessed = false;
                while (!allRequestsProcessed)
                {
                    var assignedCount = 0;
                    foreach (var unassigned in GetUnassignedRequests(context))
                    {
                        var lift = LiftLocator.FindLiftForRequest(unassigned, context.Lifts);
                        if (lift != null)
                        {
                            assignedCount++;
                            lift.AssignRequest(unassigned, tick);
                        }
                    }

                    allRequestsProcessed = assignedCount == 0;
                    foreach (var lift in context.Lifts)
                    {
                        var result = lift.ServiceAllRequests(tick);
                        foreach (var remaining in result.Remaining)
                        {
                            context.Requests.Add(remaining);
                        }
                    }
                }

                foreach (var lift in context.Lifts)
                {
                    lift.Move();
                }

                foreach (var lift in context.Lifts)
                {
                    summary.Message.Add($"Lift '{lift.Id}' is at {lift.CurrentFloor}, {lift.GetDirectionDescription()}, with {lift.GetCapacity()} people");
                }

                results.Summaries.Add(summary);
            }

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
                if (tick > 100) // Remove
                {
                    yield break;
                }
                yield return tick++;
            }
        }
    }
}
