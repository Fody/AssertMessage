using System;
using System.Diagnostics;

namespace AssemblyToProcess
{
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

  public class DebugTests
  {
    static DebugTests()
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
}
