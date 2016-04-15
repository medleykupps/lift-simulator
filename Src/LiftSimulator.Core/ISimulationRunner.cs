namespace LiftSimulator.Core
{
    public interface ISimulationRunner
    {
        SimulationResults RunSimulation(SimulationContext context);
    }
}