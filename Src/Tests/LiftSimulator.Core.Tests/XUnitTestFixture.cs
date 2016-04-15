using LiftSimulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LiftSimulator.Core.Tests
{    
    public class XUnitTestFixture
    {
        [Fact]
        public void TestXUnit()
        {
            Assert.True(true);
            Assert.False(false);
        }
    }

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
