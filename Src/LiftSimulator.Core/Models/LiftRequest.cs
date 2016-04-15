namespace LiftSimulator.Core.Models
{
    public class LiftRequest
    {
        public int PeopleCount { get; set; }
        public int SourceFloorNumber { get; set; }
        public int TargetFloorNumber { get; set; }
    }
}