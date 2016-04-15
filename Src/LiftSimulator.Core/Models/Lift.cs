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

        private readonly IList<LiftRequest> _requests = new List<LiftRequest>();

        public void AssignRequest(LiftRequest request, int tick)
        {
            request.TickAssigned = tick;
            _requests.Add(request);

            Direction = request.TargetFloorNumber < CurrentFloor
                ? LiftDirection.Down
                : LiftDirection.Up;
        }

        public IEnumerable<LiftRequest> GetRequests()
        {
            return _requests;
        }

        public int GetCapacity()
        {
            return _requests.Where(req => !req.IsComplete()).Sum(req => req.PeopleCount);
        }

        public bool IsFull()
        {
            return GetCapacity() >= MaximumCapacity;
        }

        public void Update(int tick)
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

            // Let people off
            foreach (var request in _requests.Where(req => !req.IsComplete()))
            {
                if (CurrentFloor == request.TargetFloorNumber)
                {
                    request.TickComplete = tick;
                }
            }

            if (_requests.All(req => req.IsComplete()))
            {
                // Stop moving if no more requests;
                // We could move to the ground floor
                Direction = null;
            }
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
