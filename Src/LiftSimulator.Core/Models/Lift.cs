using System.Collections.Generic;

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

        public void AssignRequest(LiftRequest request)
        {
            _requests.Add(request);
        }

        public IEnumerable<LiftRequest> GetRequests()
        {
            return _requests;
        }
    }
}
