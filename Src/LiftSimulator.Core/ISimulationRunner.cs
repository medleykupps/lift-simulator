using LiftSimulator.Core.Models;

namespace LiftSimulator.Core
{
    public interface ISimulationRunner
    {
        SimulationResults RunSimulation(SimulationContext context);

        TickSummary UpdateSimulationTick(int tick);

        LiftRequest RequestLift(int tick, int peopleCount, int sourceFloorNumber, int targetFloorNumber);
        SimulationContext Reset();

        SimulationContext Context { get; }
    }
}