using Mono.Cecil;
using System.Collections.Generic;
using System.Linq;

public abstract class ProcessorBase : IProcessor
{
    public abstract bool IsValidForModule(ModuleDefinition module);

    protected abstract bool IsThisFramework(MethodReference methodReference);

    public virtual bool IsValidForMethod(MethodReference methodReference)
    {
        return IsThisFramework(methodReference)
               && IsAssertMethod(methodReference)
               && !HasMessage(methodReference);
    }

    public virtual MethodReference GetAssertionMethodWithMessage(MethodReference methodReference)
    {
        var parameters = methodReference.Parameters;
        var newParameters = new List<string>(parameters.Select(x => x.ParameterType.IsGenericParameter ? "T" : x.ParameterType.Name)) {"String"};

        var newMethod = methodReference.DeclaringType.Resolve().FindMethod(methodReference.Name, newParameters.ToArray());
        if (newMethod == null)
        {
            newParameters = new List<string>(parameters.Select(x => x.ParameterType.IsGenericParameter ? "T" : x.ParameterType.Name))
            {
                "String",
                "Object[]"
            };
            newMethod = methodReference.DeclaringType.Resolve().FindMethod(methodReference.Name, newParameters.ToArray());
        }

        if (newMethod == null)
        {
            return null;
        }

        if (methodReference is GenericInstanceMethod genericMethod)
        {
            return GetGenericMethod(newMethod, genericMethod);
        }

        return newMethod;
    }

    protected virtual bool IsAssertMethod(MethodReference methodReference)
    {
        var resolved = methodReference.Resolve();
        var name = resolved.DeclaringType.Name;
        if (!resolved.IsStatic || !name.EndsWith("Assert"))
        {
            return false;
        }

        return true;
    }

    protected virtual bool HasMessage(MethodReference methodReference)
    {
        var parameters = methodReference.Parameters;
        if (parameters.Count >= 2)
        {
            var lastParameterName = parameters[parameters.Count - 1].ParameterType.Name;
            if ("Object[]".Equals(lastParameterName))
            {
                return true;
            }

            if ("String".Equals(lastParameterName) && !"String".Equals(parameters[parameters.Count - 2].ParameterType.Name))
            {
                return true;
            }
        }

        var found = parameters.Any(x => x.Name == "message" || x.Name == "userMessage");
        return found;
    }

    private static MethodReference GetGenericMethod(MethodDefinition newMethod, GenericInstanceMethod genericMethod)
    {
        var newGenericMethod = new GenericInstanceMethod(newMethod);

        foreach (var arg in genericMethod.GenericArguments)
        {
            newGenericMethod.GenericArguments.Add(arg);
        }

        return newGenericMethod;
    }

    protected static bool IsReferenced(ModuleDefinition module, string assembly)
    {
        return module.AssemblyReferences.Any(x => x.Name.Equals(assembly));
    }

    protected static bool IsTypeFrom(MethodReference methodReference, string name)
    {
        return methodReference.DeclaringType.FullName.StartsWith(name);
    }
}