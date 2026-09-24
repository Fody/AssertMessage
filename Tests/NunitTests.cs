public class NunitTests : IntegrationTestsBase
{
    [Test]
    public async Task StringContains_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        await Assert.That(assertionMessage).Contains("StringAssert.Contains(\"test\", actual);");
    }

    [Test]
    public async Task AreEqual_should_have_message_for_object()
    {
        var assertionMessage = CallTestMethod();
        await Assert.That(assertionMessage).Contains("Assert.AreEqual(expected, actual);");
    }

    [Test]
    public async Task AreEqual_should_have_message_for_int()
    {
        var assertionMessage = CallTestMethod();
        await Assert.That(assertionMessage).Contains("Assert.AreEqual(expected, actual);");
    }

    [Test]
    public async Task AreNotEqual_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        await Assert.That(assertionMessage).Contains("Assert.AreNotEqual(expected, actual);");
    }

    [Test]
    public async Task AreEqual_should_have_message_original_message()
    {
        var message = CallTestMethod();
        await Assert.That(message).Contains("original_message");
        await Assert.That(message).DoesNotContain("Assert.AreEqual(");
    }

    [Test]
    public async Task AreEqual_should_have_message_original_formatted_message()
    {
        var message = CallTestMethod();
        await Assert.That(message).Contains("original_message");
        await Assert.That(message).DoesNotContain("Assert.AreEqual(");
    }

    [Test]
    public async Task Contains_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        await Assert.That(assertionMessage).Contains("Assert.Contains(expected, collection);");
    }

    [Test]
    public async Task IsTrue_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        await Assert.That(assertionMessage).Contains("Assert.IsTrue(actual);");
    }

    [Test]
    public async Task False_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        await Assert.That(assertionMessage).Contains("Assert.False(actual);");
    }

    [Test]
    public async Task IsEmpty_should_have_message_for_collection()
    {
        var assertionMessage = CallTestMethod();
        await Assert.That(assertionMessage).Contains("Assert.IsEmpty(actual);");
    }

    [Test]
    public async Task IsInstanceOf_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        await Assert.That(assertionMessage).Contains("Assert.IsInstanceOf<int>(actual);");
    }

    [Test]
    public async Task Throws_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        await Assert.That(assertionMessage).Contains("Assert.Throws<Exception>(action);");
    }

    [Test]
    public async Task Fail_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        await Assert.That(assertionMessage).Contains("Assert.Fail();");
    }
}