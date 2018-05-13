#if DEBUG
using Xunit;

public class DebugTests : IntegrationTestsBase
{
    [Fact]
    public void False_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        Assert.Contains("Debug.Assert(actual);", assertionMessage);
    }

    [Fact]
    public void False_should_have_original_message()
    {
        var assertionMessage = CallTestMethod();
        Assert.Contains("original", assertionMessage);
    }
}
#endif