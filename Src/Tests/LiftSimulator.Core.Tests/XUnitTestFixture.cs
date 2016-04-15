using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LiftSimulator.Core.Tests
{    
    public class XUnitTestFixture
    {
        [Fact]
        public void TestXUnit()
        {
            Assert.True(true);
            Assert.False(false);
        }
    }
}
