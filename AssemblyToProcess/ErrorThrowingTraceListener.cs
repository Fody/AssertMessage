using System.Diagnostics;

public class ErrorThrowingTraceListener : TraceListener
{
    public override void Write(string message)
    {
        throw new NotImplementedException();
    }

    public override void WriteLine(string message)
    {
        throw new NotImplementedException();
    }

    public override void Fail(string message)
    {
        throw new Exception(message);
    }
}