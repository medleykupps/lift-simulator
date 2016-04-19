using System.Collections.Generic;
using System.Linq;
using LiftSimulator.Core.Models;

namespace LiftSimulator.Core
{
    public class SimulationRunner : ISimulationRunner
    {
        public SimulationContext Context { get; private set; }

        public SimulationRunner()
        {            
        }

        public SimulationRunner(SimulationContext context)
        {
            Context = context;
        }

        private static ISimulationRunner _instance;
        public static ISimulationRunner Instance => _instance ?? (_instance = InitialiseInstance());
        private static ISimulationRunner InitialiseInstance()
        {
            // TODO Load the context from the database

            var context = new SimulationContext(
                new List<LiftRequest>(),
                new List<Lift>
                {
                    new Lift("A", 1),
                    new Lift("B", 1),
                    new Lift("C", 1),
                    new Lift("D", 1)
                });

            return new SimulationRunner(context);
        }

        public SimulationContext Reset()
        {
            _instance = InitialiseInstance();

            return _instance.Context;
        }

        public LiftRequest RequestLift(int tick, int peopleCount, int sourceFloorNumber, int targetFloorNumber)
        {
            var request = new LiftRequest(tick, peopleCount, sourceFloorNumber, targetFloorNumber);

            Context.Requests.Add(request);

            return request;
        }

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

        public TickSummary UpdateSimulationTick(int tick)
        {
            var summary = new TickSummary { Tick = tick };

            var allRequestsProcessed = false;
            while (!allRequestsProcessed)
            {
                var assignedCount = 0;
                foreach (var unassigned in GetUnassignedRequests(Context))
                {
                    var lift = LiftLocator.FindLiftForRequest(unassigned, Context.Lifts);
                    if (lift != null)
                    {
                        assignedCount++;
                        lift.AssignRequest(unassigned, tick);
                    }
                }

                allRequestsProcessed = assignedCount == 0;
                foreach (var lift in Context.Lifts)
                {
                    var result = lift.ServiceAllRequests(tick);
                    foreach (var remaining in result.Remaining)
                    {
                        Context.Requests.Add(remaining);
                    }
                }
            }

            foreach (var lift in Context.Lifts)
            {
                lift.Move();
            }

            foreach (var lift in Context.Lifts)
            {
                summary.Message.Add($"Lift '{lift.Id}' is at {lift.CurrentFloor}, {lift.GetDirectionDescription()}, with {lift.GetCapacity()} people");
            }

            summary.Context = Context;

            return summary;
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
