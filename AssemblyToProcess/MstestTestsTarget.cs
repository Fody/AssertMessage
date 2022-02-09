using Microsoft.VisualStudio.TestTools.UnitTesting;

public class MstestTestsTarget
{
    public string AreEqualInt_should_have_message()
    {
        var expected = 1;
        var actual = 2;

        try
        {
            // ReSharper disable once RedundantTypeArgumentsOfMethod
            Assert.AreEqual<int>(expected, actual);
        }
        catch (Exception exception)
        {
            return exception.Message;
        }
        return null;
    }

    public string AreEqual_should_have_message()
    {
        var expected = "1";
        var actual = "2";

        try
        {
            Assert.AreEqual(expected, actual);
        }
        catch (Exception exception)
        {
            return exception.Message;
        }
        return null;
    }

    public string StringContains_should_have_message()
    {
        var actual = "badstring";

        try
        {
            StringAssert.Contains("test", actual);
        }
        catch (Exception exception)
        {
            return exception.Message;
        }
        return null;
    }

    public string AreEqual_should_have_message_original_formatted_message()
    {
        var expected = 1;
        var actual = 2;

        try
        {
            Assert.AreEqual(expected, actual, "{0}_{1}", "original", "message");
        }
        catch (Exception exception)
        {
            return exception.Message;
        }
        return null;
    }

    public string AreEqual_should_have_message_for_object()
    {
        var expected = new object();
        var actual = new object();

        try
        {
            Assert.AreEqual(expected, actual);
        }
        catch (Exception exception)
        {
            return exception.Message;
        }
        return null;
    }

    public string AreEqual_should_have_message_original_message()
    {
        var expected = 1;
        var actual = 2;

        try
        {
            Assert.AreEqual(expected, actual, "original_message");
        }
        catch (Exception exception)
        {
            return exception.Message;
        }
        return null;
    }

    public string AreNotEqual_should_have_message()
    {
        var notExpected = 1.5;
        var actual = 1.5;

        try
        {
            Assert.AreNotEqual(notExpected, actual);
        }
        catch (Exception exception)
        {
            return exception.Message;
        }
        return null;
    }

    public string Contains_should_have_message()
    {
        var expected = new object();
        var collection = new[] {new object(), new object()};

        try
        {
            CollectionAssert.Contains(collection, expected);
        }
        catch (Exception exception)
        {
            return exception.Message;
        }
        return null;
    }

    public string IsTrue_should_have_message()
    {
        var actual = false;

        try
        {
            Assert.IsTrue(actual);
        }
        catch (Exception exception)
        {
            return exception.Message;
        }
        return null;
    }

    public string IsFalse_should_have_message()
    {
        var actual = true;

        try
        {
            Assert.IsFalse(actual);
        }
        catch (Exception exception)
        {
            return exception.Message;
        }
        return null;
    }

    public string IsInstanceOf_should_have_message()
    {
        var actual = new object();

        try
        {
            Assert.IsInstanceOfType(actual, typeof(int));
        }
        catch (Exception exception)
        {
            return exception.Message;
        }
        return null;
    }

    public string Fail_should_have_message()
    {
        try
        {
            Assert.Fail();
        }
        catch (Exception exception)
        {
            return exception.Message;
        }

        return null;
    }
}