using LiftSimulator.Core.Models;
using Xunit;

namespace LiftSimulator.Core.Tests
{
    public class LiftTestFixture
    {
        [Fact]
        public void When_assigned_request_GetCapacity_is_0()
        {
            var lift = new Lift("A", 1);
            var request = new LiftRequest(10, 5, 10);

            lift.AssignRequest(request, 1);

            Assert.True(lift.GetCapacity() == 0);
        }

        [Fact]
        public void When_service_request_GetCapacity_is_changed()
        {
            var lift = new Lift("A", 1);
            var request = new LiftRequest(10, 1, 10);

            lift.AssignRequest(request, 1);
            request.TickServiced = 1;

            Assert.True(lift.GetCapacity() == 10);
        }
    }
}