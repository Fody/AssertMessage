using Xunit;

public class XunitTests : IntegrationTestsBase
{
    [Fact]
    public void True_should_have_message()
    {
        CheckIfMessageIsValid("Assert.True(actual);");
    }

    [Fact]
    public void False_should_have_message()
    {
        CheckIfMessageIsValid("Assert.False(actual);");
    }
}