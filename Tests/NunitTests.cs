using Xunit;

public class NunitTests : IntegrationTestsBase
{
    [Fact]
    public void StringContains_should_have_message()
    {
        CheckIfMessageIsValid("StringAssert.Contains(\"test\", actual);");
    }

    [Fact]
    public void AreEqual_should_have_message_for_object()
    {
        CheckIfMessageIsValid("Assert.AreEqual(expected, actual);");
    }

    [Fact]
    public void AreEqual_should_have_message_for_int()
    {
        CheckIfMessageIsValid("Assert.AreEqual(expected, actual);");
    }

    [Fact]
    public void AreNotEqual_should_have_message()
    {
        CheckIfMessageIsValid("Assert.AreNotEqual(expected, actual);");
    }

    [Fact]
    public void AreEqual_should_have_message_original_message()
    {
        CheckIfMessageIsValid(message =>
        {
            Assert.Contains("original_message", message);
            Assert.DoesNotContain("Assert.AreEqual(", message);
        });
    }

    [Fact]
    public void AreEqual_should_have_message_original_formatted_message()
    {
        CheckIfMessageIsValid(message =>
        {
            Assert.Contains("original_message", message);
            Assert.DoesNotContain("Assert.AreEqual(", message);
        });
    }

    [Fact]
    public void Contains_should_have_message()
    {
        CheckIfMessageIsValid("Assert.Contains(expected, collection);");
    }

    [Fact]
    public void IsTrue_should_have_message()
    {
        CheckIfMessageIsValid("Assert.IsTrue(actual);");
    }

    [Fact]
    public void False_should_have_message()
    {
        CheckIfMessageIsValid("Assert.False(actual);");
    }

    [Fact]
    public void IsEmpty_should_have_message_for_collection()
    {
        CheckIfMessageIsValid("Assert.IsEmpty(actual);");
    }

    [Fact]
    public void IsInstanceOf_should_have_message()
    {
        CheckIfMessageIsValid("Assert.IsInstanceOf<int>(actual);");
    }

    [Fact]
    public void Throws_should_have_message()
    {
        CheckIfMessageIsValid("Assert.Throws<Exception>(action);");
    }

    [Fact]
    public void Fail_should_have_message()
    {
        CheckIfMessageIsValid("Assert.Fail();");
    }
}