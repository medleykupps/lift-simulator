using LiftSimulator.Core.Models;
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

        [Fact]
        public void When_given_a_simple_context_RunSimulation_runs()
        {
            var context = new SimulationContext(
                new[]
                {
                    new LiftRequest(10, 1, 6),
                },
                TestDataHelper.GetLifts());

            var runner = new SimulationRunner();
            var results = runner.RunSimulation(context);

            Assert.NotNull(results);
        }
    }
}