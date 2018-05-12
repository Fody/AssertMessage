using System.Diagnostics;

public class DebugTestsTarget
{
    static DebugTestsTarget()
    {
        Trace.Listeners.Remove("Default");
        Trace.Listeners.Add(new ErrorThrowingTraceListener());
    }

    public void False_should_have_message()
    {
        var actual = false;

        Debug.Assert(actual);
    }

    public void False_should_have_original_message()
    {
        var actual = false;

        Debug.Assert(actual, "original");
    }
}