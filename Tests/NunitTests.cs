using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class NunitTests : IntegrationTestsBase<AssertionException>
    {
        [Test]
        public void StringContains_should_have_message()
        {
            CheckIfMessageIsValid("StringAssert.Contains(\"test\", actual);");
        }

        [Test]
        public void AreEqual_should_have_message_for_object()
        {
            CheckIfMessageIsValid("Assert.AreEqual(expected, actual);");
        }

        [Test]
        public void AreEqual_should_have_message_for_int()
        {
            CheckIfMessageIsValid("Assert.AreEqual(expected, actual);");
        }

        [Test]
        public void AreNotEqual_should_have_message()
        {
            CheckIfMessageIsValid("Assert.AreNotEqual(expected, actual);");
        }

        [Test]
        public void AreEqual_should_have_message_original_message()
        {
            CheckIfMessageIsValid(message =>
            {
                StringAssert.Contains("original_message", message);
                StringAssert.DoesNotContain("Assert.AreEqual(", message);
            });            
        }

        [Test]
        public void AreEqual_should_have_message_original_formated_message()
        {
            CheckIfMessageIsValid(message =>
            {
                StringAssert.Contains("original_message", message);
                StringAssert.DoesNotContain("Assert.AreEqual(", message);
            });
        }

        [Test]
        public void Contains_should_have_message()
        {
            CheckIfMessageIsValid("Assert.Contains(expected, collection);");
        }

        [Test]
        public void IsTrue_should_have_message()
        {
            CheckIfMessageIsValid("Assert.IsTrue(actual);");
        }

        [Test]
        public void False_should_have_message()
        {
            CheckIfMessageIsValid("Assert.False(actual);");
        }

        [Test]
        public void IsEmpty_should_have_message_for_collection()
        {
            CheckIfMessageIsValid("Assert.IsEmpty(actual);");
        }

        [Test]
        public void IsInstanceOf_should_have_message()
        {
            CheckIfMessageIsValid("Assert.IsInstanceOf<int>(actual);");
        }

        [Test]
        public void Throws_should_have_message()
        {
            CheckIfMessageIsValid("Assert.Throws<Exception>(action);");
        }

        [Test]
        public void Fail_should_have_message()
        {
            CheckIfMessageIsValid("Assert.Fail();");
        }
    }
}
