using System;
using System.Collections.Generic;
using System.Linq;
using LiftSimulator.Core.Models;

namespace LiftSimulator.Core
{
    internal class LiftLocator : ILiftLocator
    {
        public Lift FindLiftForRequest(LiftRequest request, IEnumerable<Lift> lifts)
        {
            var distances = lifts.Select(lift =>
                new
                {
                    Lift = lift,
                    Distance = GetDistanceFromRequest(lift, request)
                });

            return distances
                .Where(dist => dist.Distance != null)
                .OrderBy(dist => dist.Distance)
                .FirstOrDefault()
                ?.Lift;
        }

        private int? GetDistanceFromRequest(Lift lift, LiftRequest request)
        {
            // Travelling in the opposite direction...?
            if (lift.Direction.HasValue)
            {
                if ((lift.Direction.Value == LiftDirection.Up && request.SourceFloorNumber < lift.CurrentFloor) || 
                    (lift.Direction.Value == LiftDirection.Down && request.SourceFloorNumber > lift.CurrentFloor))
                {
                    return null;
                }
            }

            if (lift.IsFull())
            {
                return null;
            }

            // Calculate the number of stops between the lift and the request because a lift with many stops will be slow

            var otherStops = lift.GetRequests().Count(
                req =>
                    !req.IsComplete()
                    &&
                    ((lift.Direction == LiftDirection.Up && req.TargetFloorNumber < request.SourceFloorNumber) ||
                     (lift.Direction == LiftDirection.Down && req.TargetFloorNumber > request.SourceFloorNumber)));

            return Math.Abs(lift.CurrentFloor - request.SourceFloorNumber) + otherStops;
        }
    }
}