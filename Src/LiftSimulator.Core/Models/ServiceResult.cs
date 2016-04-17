using System.Collections.Generic;

namespace LiftSimulator.Core.Models
{
    public class ServiceResult
    {
        public IList<LiftRequest> Remaining { get; set; } = new List<LiftRequest>();
    }
}