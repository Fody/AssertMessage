using System.Runtime.CompilerServices;
using Fody;

public abstract class IntegrationTestsBase
{
    static Fody.TestResult testResult;

    static IntegrationTestsBase()
    {
        var weavingTask = new ModuleWeaver();

        testResult = weavingTask.ExecuteTestRun("AssemblyToProcess.dll",
            assemblyName: "IntegrationTestsBase",
            ignoreCodes: new List<string> {"0x80131869"});
    }

    protected async Task<string> CallTestMethodAsync([CallerMemberName] string memberName = "")
    {
        return await (Task<string>) Invoke(memberName);
    }

    protected string CallTestMethod([CallerMemberName] string memberName = "")
    {
        return (string) Invoke(memberName);
    }

    object Invoke(string memberName)
    {
        var name = GetType().Name + "Target";
        var type = testResult.Assembly.GetType(name);
        var test = Activator.CreateInstance(type);
        var method = test.GetType().GetMethod(memberName);
        if (method is null)
        {
            throw new($"Method {memberName} not found on {name}");
        }

        return method.Invoke(test, Array.Empty<object>());
    }
}