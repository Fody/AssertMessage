using NUnit.Framework;
using System;
using System.Runtime.CompilerServices;

public abstract class IntegrationTestsBase<TException>
    where TException : Exception
{
    Type type;

    public IntegrationTestsBase()
    {
        var testType = GetType().FullName.Replace("Tests.", "AssemblyToProcess.")+ "Target";
        type = AssemblyLoader.Assembly.GetType(testType);
    }

    protected void CheckIfMessageIsValid(string message, [CallerMemberName] string memberName = "")
    {
        CheckIfMessageIsValid(assertionMessage => StringAssert.Contains(message, assertionMessage), memberName);
    }

    protected void CheckIfMessageIsValid(Action<string> action, [CallerMemberName] string memberName = "")
    {
        var message = CallTestMethod(memberName);
        action(message);
    }

    string CallTestMethod(string memberName)
    {
        var test = Activator.CreateInstance(type);
        var method = test.GetType().GetMethod(memberName);
        Assert.NotNull(method, "Invalid test name: {0}", memberName);

        return (string )method.Invoke(test, new object[0]);
    }
}