using System.Collections.Generic;
using LiftSimulator.Core.Models;

namespace LiftSimulator.Core.Tests
{
    public static class TestDataHelper
    {
        public static IEnumerable<Lift> GetLifts()
        {
            return new[]
                   {
                       new Lift("A", 1),
                       new Lift("B", 1)
                   };
        }
    }
}