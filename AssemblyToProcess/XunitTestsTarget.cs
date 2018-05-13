using System;
using Xunit;

public class XunitTestsTarget
{
    public string True_should_have_message()
    {
        var actual = false;

        try
        {
            Assert.True(actual);
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
}