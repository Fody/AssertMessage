using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

public class MstestTests : IntegrationTestsBase<AssertFailedException>
{
    [Fact]
    public void StringContains_should_have_message()
    {
        CheckIfMessageIsValid("StringAssert.Contains(\"test\", actual);");
    }

    [Fact]
    public void AreEqual_should_have_message_original_formatted_message()
    {
        CheckIfMessageIsValid(message =>
        {
            NUnit.Framework.StringAssert.Contains("original_message", message);
            NUnit.Framework.StringAssert.DoesNotContain("Assert.AreEqual(", message);
        });
    }

    [Fact]
    public void AreEqual_should_have_message()
    {
        CheckIfMessageIsValid("Assert.AreEqual(expected, actual);");
    }

    [Fact]
    public void AreEqualInt_should_have_message()
    {
        CheckIfMessageIsValid("Assert.AreEqual<int>(expected, actual);");
    }

    [Fact]
    public void AreEqual_should_have_message_for_object()
    {
        CheckIfMessageIsValid("Assert.AreEqual(expected, actual);");
    }

    [Fact]
    public void AreEqual_should_have_message_original_message()
    {
        CheckIfMessageIsValid(message =>
        {
            NUnit.Framework.StringAssert.Contains("original_message", message);
            NUnit.Framework.StringAssert.DoesNotContain("Assert.AreEqual(", message);
        });
    }

    [Fact]
    public void AreNotEqual_should_have_message()
    {
        CheckIfMessageIsValid("Assert.AreNotEqual(notExpected, actual);");
    }

    [Fact]
    public void Contains_should_have_message()
    {
        CheckIfMessageIsValid("CollectionAssert.Contains(collection, expected);");
    }

    [Fact]
    public void IsTrue_should_have_message()
    {
        CheckIfMessageIsValid("Assert.IsTrue(actual);");
    }

    [Fact]
    public void IsFalse_should_have_message()
    {
        CheckIfMessageIsValid("Assert.IsFalse(actual);");
    }

    [Fact]
    public void IsInstanceOf_should_have_message()
    {
        CheckIfMessageIsValid("Assert.IsInstanceOfType(actual, typeof(int));");
    }

    [Fact]
    public void Fail_should_have_message()
    {
        CheckIfMessageIsValid("Assert.Fail();");
    }
}