using System;
using NUnit.Framework;
using Tests;

[TestFixture]
public class DebugTests : IntegrationTestsBase<Exception>
{
    [Test]
    public void False_should_have_message()
    {
        CheckIfMessageIsValid("Debug.Assert(actual);");
    }

    [Test]
    public void False_should_have_original_message()
    {
        CheckIfMessageIsValid("original");
    }
}