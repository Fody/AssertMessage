public class MstestTests : IntegrationTestsBase
{
    [Test]
    public async Task StringContains_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        await Assert.That(assertionMessage).Contains("StringAssert.Contains(\"test\", actual);");
    }

    [Test]
    public async Task AreEqual_should_have_message_original_formatted_message()
    {
        var message = CallTestMethod();
        await Assert.That(message).Contains("original_message");
        await Assert.That(message).DoesNotContain("Assert.AreEqual(");
    }

    [Test]
    public async Task AreEqual_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        await Assert.That(assertionMessage).Contains("Assert.AreEqual(expected, actual);");
    }

    [Test]
    public async Task AreEqualInt_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        await Assert.That(assertionMessage).Contains("Assert.AreEqual<int>(expected, actual);");
    }

    [Test]
    public async Task AreEqual_should_have_message_for_object()
    {
        var assertionMessage = CallTestMethod();
        await Assert.That(assertionMessage).Contains("Assert.AreEqual(expected, actual);");
    }

    [Test]
    public async Task AreEqual_should_have_message_original_message()
    {
        var message = CallTestMethod();
        await Assert.That(message).Contains("original_message");
        await Assert.That(message).DoesNotContain("Assert.AreEqual(");
    }

    [Test]
    public async Task AreNotEqual_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        await Assert.That(assertionMessage).Contains("Assert.AreNotEqual(notExpected, actual);");
    }

    [Test]
    public async Task Contains_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        await Assert.That(assertionMessage).Contains("CollectionAssert.Contains(collection, expected);");
    }

    [Test]
    public async Task IsTrue_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        await Assert.That(assertionMessage).Contains("Assert.IsTrue(actual);");
    }

    [Test]
    public async Task IsFalse_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        await Assert.That(assertionMessage).Contains("Assert.IsFalse(actual);");
    }

    [Test]
    public async Task IsInstanceOf_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        await Assert.That(assertionMessage).Contains("Assert.IsInstanceOfType(actual, typeof(int));");
    }

    [Test]
    public async Task Fail_should_have_message()
    {
        var assertionMessage = CallTestMethod();
        await Assert.That(assertionMessage).Contains("Assert.Fail();");
    }
}