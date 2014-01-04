using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace AssemblyToProcess
{
    public class XunitTests
    {
        public void True_should_have_message()
        {
            var actual = false;

            Assert.True(actual);
        }

        public void False_should_have_message()
        {
            var actual = true;

            Assert.False(actual);
        }
    }
}
