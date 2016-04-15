using Xunit;

namespace LiftSimulator.Core.Tests
{
    public class SimulationRunnerTestFixture
    {
        [Fact]
        public void When_given_context_RunSimulation_returns_results()
        {
            var context = new SimulationContext();
            var runner = new SimulationRunner();
            var results = runner.RunSimulation(context);

            Assert.NotNull(results);
        }
    }
}