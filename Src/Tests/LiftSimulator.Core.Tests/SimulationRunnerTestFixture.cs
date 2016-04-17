using System;
using System.Diagnostics;
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
            Assert.NotNull(results.Summaries);
            Assert.True(results.Summaries.Count == 6);
        }

        [Fact]
        public void When_given_two_requests_RunSimulation_runs()
        {
            var context = new SimulationContext(
                new[]
                {
                    new LiftRequest(10, 1, 6),
                    new LiftRequest(10, 2, 6)
                },
                TestDataHelper.GetLifts());

            var runner = new SimulationRunner();
            var results = runner.RunSimulation(context);

            Assert.NotNull(results);
            Assert.NotNull(results.Summaries);
            Assert.True(results.Summaries.Count == 6);
        }

        [Fact]
        public void When_large_count_of_people_RunSimulation_runs()
        {
            var context = new SimulationContext(
                new[]
                {
                    new LiftRequest(60, 1, 6),
                },
                TestDataHelper.GetLifts());

            var runner = new SimulationRunner();
            var results = runner.RunSimulation(context);

            Assert.NotNull(results);
            Assert.NotNull(results.Summaries);
            Assert.True(results.Summaries.Count == 17);
        }
    }
}