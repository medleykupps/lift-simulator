namespace LiftSimulator.Core.Models
{
    public class LiftRequest
    {
        public LiftRequest() { }

        public LiftRequest(int peopleCount, int sourceFloorNumber, int targetFloorNumber)
        {
            Tick = 0;
            PeopleCount = peopleCount;
            SourceFloorNumber = sourceFloorNumber;
            TargetFloorNumber = targetFloorNumber;
        }

        public int Tick { get; set; }
        public int? TickComplete { get; set; }
        public int? TickAssigned { get; set; }
        public int? TickServiced { get; set; }
        public int PeopleCount { get; set; }
        public int SourceFloorNumber { get; set; }
        public int TargetFloorNumber { get; set; }

        public bool IsAssigned()
        {
            return TickAssigned.GetValueOrDefault(-1) > -1;
        }

        public bool IsServiced()
        {
            return TickServiced.GetValueOrDefault(-1) > -1;
        }

        public bool IsComplete()
        {
            return TickComplete.GetValueOrDefault(-1) > -1;
        }

        public static LiftRequest CreateUnassigned(LiftRequest other, int newPeopleCount)
        {
            return new LiftRequest(newPeopleCount, other.SourceFloorNumber, other.TargetFloorNumber)
                   {
                       Tick = other.Tick
                   };
        }
    }
}