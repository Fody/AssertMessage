using Xunit;

public class XunitTestsTarget
{
    public void True_should_have_message()
    {
        var actual = false;

        Assert.True(actual);
    }

    public void False_should_have_message()
    {
        var actual = true;

        Assert.False(actual);
    }
}