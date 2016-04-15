namespace LiftSimulator.Core.Models
{
    public class LiftRequest
    {
        public int Tick { get; set; }
        public int? TickComplete { get; set; }
        public int PeopleCount { get; set; }
        public int SourceFloorNumber { get; set; }
        public int TargetFloorNumber { get; set; }

        public bool IsComplete()
        {
            return TickComplete.GetValueOrDefault(-1) > -1;
        }
    }
}