using System;
using Xunit;

public class DebugTests : IntegrationTestsBase<Exception>
{
    [Fact]
    public void False_should_have_message()
    {
        CheckIfMessageIsValid("Debug.Assert(actual);");
    }

    [Fact]
    public void False_should_have_original_message()
    {
        CheckIfMessageIsValid("original");
    }
}