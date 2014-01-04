using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class XunitTests : IntegrationTestsBase<Xunit.Sdk.AssertException>
    {
        [Test]
        public void True_should_have_message()
        {
            CheckIfMessageIsValid("Assert.True(actual);");
        }

        [Test]
        public void False_should_have_message()
        {
            CheckIfMessageIsValid("Assert.False(actual);");
        }
    }
}
