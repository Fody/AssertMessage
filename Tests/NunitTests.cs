using Xunit;
using Xunit.Abstractions;

public class NunitTests : IntegrationTestsBase
{
    [Fact]
    public void StringContains_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        Assert.Contains("StringAssert.Contains(\"test\", actual);", assertionMessage);
    }

    [Fact]
    public void AreEqual_should_have_message_for_object()
    {
        var assertionMessage = CallTestMethod();
        Assert.Contains("Assert.AreEqual(expected, actual);", assertionMessage);
    }

    [Fact]
    public void AreEqual_should_have_message_for_int()
    {
        var assertionMessage = CallTestMethod();
        Assert.Contains("Assert.AreEqual(expected, actual);", assertionMessage);
    }

    [Fact]
    public void AreNotEqual_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        Assert.Contains("Assert.AreNotEqual(expected, actual);", assertionMessage);
    }

    [Fact]
    public void AreEqual_should_have_message_original_message()
    {
        var message = CallTestMethod();
        Assert.Contains("original_message", message);
        Assert.DoesNotContain("Assert.AreEqual(", message);
    }

    [Fact]
    public void AreEqual_should_have_message_original_formatted_message()
    {
        var message = CallTestMethod();
        Assert.Contains("original_message", message);
        Assert.DoesNotContain("Assert.AreEqual(", message);
    }

    [Fact]
    public void Contains_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        Assert.Contains("Assert.Contains(expected, collection);", assertionMessage);
    }

    [Fact]
    public void IsTrue_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        Assert.Contains("Assert.IsTrue(actual);", assertionMessage);
    }

    [Fact]
    public void False_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        Assert.Contains("Assert.False(actual);", assertionMessage);
    }

    [Fact]
    public void IsEmpty_should_have_message_for_collection()
    {
        var assertionMessage = CallTestMethod();
        Assert.Contains("Assert.IsEmpty(actual);", assertionMessage);
    }

    [Fact]
    public void IsInstanceOf_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        Assert.Contains("Assert.IsInstanceOf<int>(actual);", assertionMessage);
    }

    [Fact]
    public void Throws_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        Assert.Contains("Assert.Throws<Exception>(action);", assertionMessage);
    }

    [Fact]
    public void Fail_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        Assert.Contains("Assert.Fail();", assertionMessage);
    }

    public NunitTests(ITestOutputHelper output) : base(output)
    {
    }
}