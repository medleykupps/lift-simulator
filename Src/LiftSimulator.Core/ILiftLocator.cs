using System.Collections.Generic;
using LiftSimulator.Core.Models;

namespace LiftSimulator.Core
{
    public interface ILiftLocator
    {
        Lift FindLiftForRequest(LiftRequest request, IEnumerable<Lift> lifts);
    }
}