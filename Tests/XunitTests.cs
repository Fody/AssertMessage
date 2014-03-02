using NUnit.Framework;

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
