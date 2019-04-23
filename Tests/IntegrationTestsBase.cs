using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Fody;
using Xunit;
using Xunit.Abstractions;

public abstract class IntegrationTestsBase :
    XunitLoggingBase
{
    static TestResult testResult;

    static IntegrationTestsBase()
    {
        var weavingTask = new ModuleWeaver();

        testResult = weavingTask.ExecuteTestRun("AssemblyToProcess.dll",
            assemblyName: "IntegrationTestsBase",
            ignoreCodes: new List<string> {"0x80131869"});
    }

    protected string CallTestMethod([CallerMemberName] string memberName = "")
    {
        var name = GetType().Name + "Target";
        var type = testResult.Assembly.GetType(name);
        var test = Activator.CreateInstance(type);
        var method = test.GetType().GetMethod(memberName);
        Assert.NotNull(method);

        return (string) method.Invoke(test, new object[0]);
    }

    protected IntegrationTestsBase(ITestOutputHelper output) : 
        base(output)
    {
    }
}