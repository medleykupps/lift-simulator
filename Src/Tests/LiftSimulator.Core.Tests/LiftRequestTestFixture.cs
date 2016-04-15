using LiftSimulator.Core.Models;
using Xunit;

namespace LiftSimulator.Core.Tests
{
    public class LiftRequestTestFixture
    {
        [Fact]
        public void When_request_has_no_complete_tick_IsComplete_is_false()
        {
            var request = new LiftRequest
                          {
                              Tick = 0,
                              TickComplete = null
                          };

            Assert.False(request.IsComplete());
        }

        [Fact]
        public void When_request_has_complete_tick_IsComplete_is_true()
        {
            var request = new LiftRequest
                          {
                              Tick = 0,
                              TickComplete = 99
                          };

            Assert.True(request.IsComplete());
        }
    }
}