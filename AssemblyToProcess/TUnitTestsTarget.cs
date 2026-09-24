using TUnit.Assertions;
using TUnit.Assertions.Extensions;

public class TUnitTestsTarget
{
    public async Task<string> IsEqualTo_should_have_message()
    {
        var actual = 1;

        try
        {
            await Assert.That(actual).IsEqualTo(2);
        }
        catch (Exception exception)
        {
            return exception.Message;
        }
        return null;
    }

    public async Task<string> Passing_assertion_should_pass()
    {
        var actual = 2;

        try
        {
            await Assert.That(actual).IsEqualTo(2);
        }
        catch (Exception exception)
        {
            return exception.Message;
        }
        return null;
    }

    public async Task<string> And_chain_should_have_message()
    {
        var actual = 1;

        try
        {
            await Assert.That(actual).IsGreaterThan(0).And.IsEqualTo(2);
        }
        catch (Exception exception)
        {
            return exception.Message;
        }
        return null;
    }

    public async Task<string> Multiline_should_have_message()
    {
        var actual = "a";

        try
        {
            await Assert.That(actual)
                .IsNotNull()
                .And
                .IsEqualTo("b");
        }
        catch (Exception exception)
        {
            return exception.Message;
        }
        return null;
    }

    public async Task<string> Existing_because_should_not_be_overwritten()
    {
        var actual = 1;

        try
        {
            await Assert.That(actual).IsEqualTo(2).Because("custom reason");
        }
        catch (Exception exception)
        {
            return exception.Message;
        }
        return null;
    }
}
