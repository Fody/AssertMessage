public class XunitTests :
    IntegrationTestsBase
{
    [Test]
    public async Task True_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        await Assert.That(assertionMessage).Contains("Assert.True(actual);");
    }

    [Test]
    public async Task False_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        await Assert.That(assertionMessage).Contains("Assert.False(actual);");
    }
}