using System.Collections.Generic;
using LiftSimulator.Core.Models;

namespace LiftSimulator.Models
{
    public class UpdateCommand
    {
        public int Tick { get; set; }
        public IEnumerable<LiftRequest> Requests { get; set; }
    }
}