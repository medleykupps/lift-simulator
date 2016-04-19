using System;
using System.Collections.Generic;
using System.Linq;

namespace LiftSimulator.Core.Models
{
    public class Lift
    {
        public Lift(string id, int currentFloor, LiftDirection? direction = null)
        {
            Id = id;
            CurrentFloor = currentFloor;
            Direction = direction;
        }

        public string Id { get; private set; }
        public int CurrentFloor { get; private set; }
        public LiftDirection? Direction { get; private set; }

        protected readonly IList<LiftRequest> Requests = new List<LiftRequest>();

        public void AssignRequest(LiftRequest request, int tick)
        {
            request.TickAssigned = tick;

            Requests.Add(request);

            Direction = request.SourceFloorNumber < CurrentFloor
                ? LiftDirection.Down
                : LiftDirection.Up;
        }

        public IEnumerable<LiftRequest> GetRequests()
        {
            return Requests;
        }

        public int GetCapacity()
        {
            return Requests.Where(req => req.IsServiced() && !req.IsComplete()).Sum(req => req.PeopleCount);
        }

        public int Capacity => GetCapacity();

        public bool IsFull()
        {
            return GetCapacity() >= MaximumCapacity;
        }

        public ServiceResult ServiceAllRequests(int tick)
        {
            var result = new ServiceResult();

            var peopleOffCount = 0;
            var peopleOnCount = 0;

            foreach (var request in Requests.Where(req => !req.IsComplete()))
            {
                if (request.IsServiced() && CurrentFloor == request.TargetFloorNumber)
                {
                    // Drop people off at this floor
                    request.TickComplete = tick;
                    peopleOffCount += request.PeopleCount;
                }

                if (request.IsAssigned() && !request.IsServiced() && CurrentFloor == request.SourceFloorNumber)
                {
                    // Pick people up from this floor

                    // Check we have enough capacity in the lift, otherwise create a new request for the remaining people
                    if (GetCapacity() + request.PeopleCount <= MaximumCapacity)
                    {
                        ServiceRequest(request, tick);
                        peopleOnCount += request.PeopleCount;
                    }
                    else
                    {
                        // Create a new Request for the remaining people
                        var roomInLift = MaximumCapacity - GetCapacity();
                        var remaining = LiftRequest.CreateUnassigned(request, request.PeopleCount - roomInLift);
                        result.Remaining.Add(remaining);

                        request.PeopleCount = roomInLift;
                        ServiceRequest(request, tick);
                        peopleOnCount += request.PeopleCount;
                    }
                }
            }

            if (peopleOnCount > 0)
            {
                result.SummaryItems.Add(CreateSummaryItem(tick, LiftAction.GetOn, peopleOnCount));
            }
            if (peopleOffCount > 0)
            {
                result.SummaryItems.Add(CreateSummaryItem(tick, LiftAction.GetOff, peopleOffCount));
            }

            if (Requests.All(req => req.IsComplete()))
            {
                // Stop moving if no more requests;
                // We could move to the ground floor
                Direction = null;
            }

            return result;
        }

        private SummaryItem CreateSummaryItem(int tick, LiftAction action, int peopleCount)
        {
            var desc = peopleCount == 1 ? "person" : "people";
            var actionDesc = action == LiftAction.GetOff ? "got off" : "got on";

            return new SummaryItem
                   {
                       Tick = tick,
                       Action = action,
                       ActionDesc = actionDesc,
                       Level = CurrentFloor,
                       LiftId = Id,
                       PeopleCount = peopleCount,
                       Message = $"{peopleCount} {desc} {actionDesc} Lift {Id} on Level {CurrentFloor}"
                   };
        }

        public void Move()
        {
            if (Direction.HasValue)
            {
                CurrentFloor = Direction.Value == LiftDirection.Up
                    ? CurrentFloor + 1
                    : CurrentFloor - 1;
                if (CurrentFloor > MaximumFloor)
                {
                    CurrentFloor = MaximumFloor;
                    Direction = null;
                }
                else if (CurrentFloor < MinimumFloor)
                {
                    CurrentFloor = MinimumFloor;
                    Direction = null;
                }
            }
        }

        private void ServiceRequest(LiftRequest request, int tick)
        {
            request.TickServiced = tick;

            Direction = request.TargetFloorNumber > CurrentFloor
                ? LiftDirection.Up
                : LiftDirection.Down;
        }

        public string GetDirectionDescription()
        {
            return Direction == null
                ? "stopped"
                : Direction == LiftDirection.Up
                    ? "moving up"
                    : "moving down";
        }

        public const int MaximumCapacity = 20;
        public const int MaximumFloor = 10;
        public const int MinimumFloor = 1;

    }
}
