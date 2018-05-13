using NUnit.Framework;
using System;

public class NunitTestsTarget
{
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

    public string AreEqual_should_have_message_for_int()
    {
        var expected = 1;
        var actual = 2;

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
        var expected = 1;
        var actual = 1;

        try
        {
            Assert.AreNotEqual(expected, actual);
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
            Assert.Contains(expected, collection);
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

    public string False_should_have_message()
    {
        var actual = true;

        try
        {
            Assert.False(actual);
        }
        catch (Exception exception)
        {
            return exception.Message;
        }
        return null;
    }

    public string IsEmpty_should_have_message_for_collection()
    {
        var actual = new[] {new object()};

        try
        {
            Assert.IsEmpty(actual);
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
            Assert.IsInstanceOf<int>(actual);
        }
        catch (Exception exception)
        {
            return exception.Message;
        }
        return null;
    }

    public string Throws_should_have_message()
    {
        var action = new TestDelegate(() => { });

        try
        {
            var ex = Assert.Throws<Exception>(action);
            Assert.IsNotNull(ex);
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