using LiftSimulator.Core.Models;
using Xunit;

namespace LiftSimulator.Core.Tests
{
    public class LiftLocatorTestFixture
    {
        [Fact]
        public void When_simple_request_FindLift_returns_lift()
        {
            var locator = new LiftLocator();
            var request = new LiftRequest(5, 1, 10);
            var lift = new Lift("A", 1);

            var result = locator.FindLiftForRequest(request, new Lift[] {lift});

            Assert.NotNull(result);
            Assert.True(result.Id == lift.Id);
        }

        [Fact]
        public void When_lift_A_is_closer_FindLift_returns_lift_A()
        {
            var locator = new LiftLocator();
            var request = new LiftRequest(5, 5, 10);
            var lifts = new[]
                        {
                            new Lift("A", 3),
                            new Lift("B", 1)
                        };

            var result = locator.FindLiftForRequest(request, lifts);

            Assert.NotNull(result);
            Assert.True(result.Id == "A");
        }

        [Fact]
        public void When_lift_B_is_closer_FindLift_returns_lift_B()
        {
            var locator = new LiftLocator();
            var request = new LiftRequest(5, 5, 10);
            var lifts = new[]
                        {
                            new Lift("A", 1),
                            new Lift("B", 3)
                        };

            var result = locator.FindLiftForRequest(request, lifts);

            Assert.NotNull(result);
            Assert.True(result.Id == "B");
        }

        [Fact]
        public void When_lift_A_is_closer_above_request_FindLift_returns_lift_A()
        {
            var locator = new LiftLocator();
            var request = new LiftRequest(5, 5, 10);
            var lifts = new[]
                        {
                            new Lift("A", 6), 
                            new Lift("B", 1)
                        };

            var result = locator.FindLiftForRequest(request, lifts);

            Assert.NotNull(result);
            Assert.True(result.Id == "A");
        }

        [Fact]
        public void When_lift_A_is_closer_above_request_but_moving_up_FindLift_returns_lift_B()
        {
            var locator = new LiftLocator();
            var request = new LiftRequest(5, 5, 10);
            var lifts = new[]
                        {
                            new Lift("A", 6, LiftDirection.Up),
                            new Lift("B", 1)
                        };

            var result = locator.FindLiftForRequest(request, lifts);

            Assert.NotNull(result);
            Assert.True(result.Id == "B");
        }

        [Fact]
        public void When_lift_A_is_above_and_B_below_request_FindLift_returns_null()
        {
            var locator = new LiftLocator();
            var request = new LiftRequest(5, 5, 10);
            var lifts = new[]
                        {
                            new Lift("A", 6, LiftDirection.Up),
                            new Lift("B", 4, LiftDirection.Down)
                        };

            var result = locator.FindLiftForRequest(request, lifts);

            Assert.Null(result);
        }
    }
}