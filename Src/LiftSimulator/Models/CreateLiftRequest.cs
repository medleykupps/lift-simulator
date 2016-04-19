namespace LiftSimulator.Models
{
    public class CreateLiftRequest
    {
        public int Tick { get; set; }
        public int PeopleCount { get; set; }
        public int SourceFloorNumber { get; set; }
        public int TargetFloorNumber { get; set; }
    }
}