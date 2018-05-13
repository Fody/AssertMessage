using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Fody;
using Xunit;

#pragma warning disable 618

public abstract class IntegrationTestsBase
{
    static TestResult testResult;

    static IntegrationTestsBase()
    {
        var weavingTask = new ModuleWeaver();

        testResult = weavingTask.ExecuteTestRun("AssemblyToProcess.dll",
            assemblyName: "IntegrationTestsBase",
            ignoreCodes: new List<string> { "0x80131869" });
    }

    protected void CheckIfMessageIsValid(string message, [CallerMemberName] string memberName = "")
    {
        CheckIfMessageIsValid(assertionMessage => Assert.Contains(message, assertionMessage), memberName);
    }

    protected void CheckIfMessageIsValid(Action<string> action, [CallerMemberName] string memberName = "")
    {
        var message = CallTestMethod(memberName);
        action(message);
    }

    string CallTestMethod(string memberName)
    {
        var name = GetType().Name + "Target";
        var type = testResult.Assembly.GetType(name);
        var test = Activator.CreateInstance(type);
        var method = test.GetType().GetMethod(memberName);
        Assert.NotNull(method);

        return (string )method.Invoke(test, new object[0]);
    }
}