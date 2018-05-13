using Xunit;
using Xunit.Abstractions;

public class XunitTests : IntegrationTestsBase
{
    [Fact]
    public void True_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        Assert.Contains("Assert.True(actual);", assertionMessage);
    }

    [Fact]
    public void False_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        Assert.Contains("Assert.False(actual);", assertionMessage);
    }

    public XunitTests(ITestOutputHelper output) : base(output)
    {
    }
}