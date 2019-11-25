using Xunit;
using Xunit.Abstractions;

public class ComplexBranchingTests :
    IntegrationTestsBase
{
    [Fact]
    public void TestMethod1()
    {
        CallTestMethod();
    }

    [Fact]
    public void TestMethod2()
    {
        CallTestMethod();
    }

    public ComplexBranchingTests(ITestOutputHelper output) :
        base(output)
    {
    }
}