using NUnit.Framework;
using System;
using System.Reflection;
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
        try
        {
            CallTestMethod(memberName);
            Assert.Fail("Exception was expected");
        }
        catch (TargetInvocationException ex)
        {
            var assertion = ex.InnerException as TException;
            Assert.IsNotNull(assertion, "Invalid inner exception: {0}", ex.Message);
            action(assertion.Message);
        }
    }

    void CallTestMethod(string memberName)
    {
        var test = Activator.CreateInstance(type);
        var method = test.GetType().GetMethod(memberName);
        Assert.NotNull(method, "Invalid test name: {0}", memberName);

        method.Invoke(test, new object[0]);
    }
}