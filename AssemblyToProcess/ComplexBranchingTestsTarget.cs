using Microsoft.VisualStudio.TestTools.UnitTesting;

public class ComplexBranchingTestsTarget
{
    int i = 0;

    void Method1(ref string a_toSplit, int a_lineLength)
    {
        switch (i++)
        {
            case 0:
                Assert.AreEqual("testLine1", a_toSplit);
                Assert.AreEqual(45, a_lineLength);
                break;
            case 1:
                Assert.AreEqual("testLine2", a_toSplit);
                Assert.AreEqual(45, a_lineLength);
                break;
            case 2:
                Assert.AreEqual("testLine3", a_toSplit);
                Assert.AreEqual(45, a_lineLength);
                break;
            case 3:
                Assert.AreEqual("testLine4", a_toSplit);
                Assert.AreEqual(45, a_lineLength);
                break;
            case 4:
                Assert.Fail();
                break;
        }
    }

    public void TestMethod1()
    {
        var data = "testLine1";
        Method1(ref data, 45);
    }

    List<string> Method2(string a_searchPattern)
    {
        if (a_searchPattern == "*")
        {
            return new List<string>();
        }
        else
            // No curly braces here. The compiler turns them into nops in debug mode, and this needs a branch instruction that jumps directly to a call.
            Assert.Fail();

        return null;
    }

    public void TestMethod2()
    {
        Method2("*");
    }
}