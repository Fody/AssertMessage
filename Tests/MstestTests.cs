using Xunit;

public class MstestTests : IntegrationTestsBase
{
    [Fact]
    public void StringContains_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        Assert.Contains("StringAssert.Contains(\"test\", actual);", assertionMessage);
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
    public void AreEqual_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        Assert.Contains("Assert.AreEqual(expected, actual);", assertionMessage);
    }

    [Fact]
    public void AreEqualInt_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        Assert.Contains("Assert.AreEqual<int>(expected, actual);", assertionMessage);
    }

    [Fact]
    public void AreEqual_should_have_message_for_object()
    {
        var assertionMessage = CallTestMethod();
        Assert.Contains("Assert.AreEqual(expected, actual);", assertionMessage);
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
    public void AreNotEqual_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        Assert.Contains("Assert.AreNotEqual(notExpected, actual);", assertionMessage);
    }

    [Fact]
    public void Contains_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        Assert.Contains("CollectionAssert.Contains(collection, expected);", assertionMessage);
    }

    [Fact]
    public void IsTrue_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        Assert.Contains("Assert.IsTrue(actual);", assertionMessage);
    }

    [Fact]
    public void IsFalse_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        Assert.Contains("Assert.IsFalse(actual);", assertionMessage);
    }

    [Fact]
    public void IsInstanceOf_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        Assert.Contains("Assert.IsInstanceOfType(actual, typeof(int));", assertionMessage);
    }

    [Fact]
    public void Fail_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        Assert.Contains("Assert.Fail();", assertionMessage);
    }
}