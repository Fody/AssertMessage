[NotInParallel]
public class TUnitTests :
    IntegrationTestsBase
{
    [Test]
    public async Task IsEqualTo_should_have_message()
    {
        var assertionMessage = await CallTestMethodAsync();
        await Assert.That(assertionMessage).Contains("Assert.That(actual).IsEqualTo(2);");
    }

    [Test]
    public async Task Passing_assertion_should_pass()
    {
        var assertionMessage = await CallTestMethodAsync();
        await Assert.That(assertionMessage).IsNull();
    }

    [Test]
    public async Task And_chain_should_have_message()
    {
        var assertionMessage = await CallTestMethodAsync();
        await Assert.That(assertionMessage).Contains("Assert.That(actual).IsGreaterThan(0).And.IsEqualTo(2);");
    }

    [Test]
    public async Task Multiline_should_have_message()
    {
        var assertionMessage = await CallTestMethodAsync();
        await Assert.That(assertionMessage).Contains("Assert.That(actual) .IsNotNull() .And .IsEqualTo(\"b\");");
    }

    [Test]
    public async Task Existing_because_should_not_be_overwritten()
    {
        var assertionMessage = await CallTestMethodAsync();
        await Assert.That(assertionMessage).Contains("custom reason");
        await Assert.That(assertionMessage).DoesNotContain("Assert.That(actual).IsEqualTo(2);");
    }
}
