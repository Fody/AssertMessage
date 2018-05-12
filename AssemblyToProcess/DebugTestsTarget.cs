using System;
using System.Diagnostics;

public class DebugTestsTarget
{
    static DebugTestsTarget()
    {
        Trace.Listeners.Remove("Default");
        Trace.Listeners.Add(new ErrorThrowingTraceListener());
    }

    public string False_should_have_message()
    {
        var actual = false;

        try
        {
            Debug.Assert(actual);
        }
        catch (Exception exception)
        {
            return exception.Message;
        }

        return null;
    }

    public string False_should_have_original_message()
    {
        var actual = false;

        try
        {
            Debug.Assert(actual, "original");
        }
        catch (Exception exception)
        {
            return exception.Message;
        }
        return null;
    }
}