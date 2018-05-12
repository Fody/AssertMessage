using Microsoft.VisualStudio.TestTools.UnitTesting;

public class MstestTests
{
    public void AreEqualInt_should_have_message()
    {
        var expected = 1;
        var actual = 2;

        // ReSharper disable once RedundantTypeArgumentsOfMethod
        Assert.AreEqual<int>(expected, actual);
    }

    public void AreEqual_should_have_message()
    {
        var expected = "1";
        var actual = "2";

        Assert.AreEqual(expected, actual);
    }

    public void StringContains_should_have_message()
    {
        var actual = "badstring";

        StringAssert.Contains("test", actual);
    }

    public void AreEqual_should_have_message_original_formated_message()
    {
        var expected = 1;
        var actual = 2;

        Assert.AreEqual(expected, actual, "{0}_{1}", "original", "message");
    }

    public void AreEqual_should_have_message_for_object()
    {
        var expected = new object();
        var actual = new object();

        Assert.AreEqual(expected, actual);
    }

    public void AreEqual_should_have_message_original_message()
    {
        var expected = 1;
        var actual = 2;

        Assert.AreEqual(expected, actual, "original_message");
    }

    public void AreNotEqual_should_have_message()
    {
        var notExpected = 1.5;
        var actual = 1.5;

        Assert.AreNotEqual(notExpected, actual);
    }

    public void Contains_should_have_message()
    {
        var expected = new object();
        var collection = new[] {new object(), new object()};

        CollectionAssert.Contains(collection, expected);
    }

    public void IsTrue_should_have_message()
    {
        var actual = false;

        Assert.IsTrue(actual);
    }

    public void IsFalse_should_have_message()
    {
        var actual = true;

        Assert.IsFalse(actual);
    }

    public void IsInstanceOf_should_have_message()
    {
        var actual = new object();

        Assert.IsInstanceOfType(actual, typeof(int));
    }

    public void Fail_should_have_message()
    {
        Assert.Fail();
    }
}