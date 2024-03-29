﻿using Mono.Cecil;

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
        var newParameters = new List<string>(parameters.Select(_ => _.ParameterType.IsGenericParameter ? "T" : _.ParameterType.Name)) {"String"};

        var typeDefinition = methodReference.DeclaringType.Resolve();
        var newMethod = typeDefinition.FindMethod(methodReference.Name, newParameters.ToArray());
        if (newMethod == null)
        {
            newParameters = new(parameters.Select(_ => _.ParameterType.IsGenericParameter ? "T" : _.ParameterType.Name))
            {
                "String",
                "Object[]"
            };
            newMethod = typeDefinition.FindMethod(methodReference.Name, newParameters.ToArray());
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
        return resolved.IsStatic && name.EndsWith("Assert");
    }

    protected bool HasMessage(MethodReference methodReference)
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

        return parameters.Any(_ => _.Name is "message" or "userMessage");
    }

    static MethodReference GetGenericMethod(MethodDefinition newMethod, GenericInstanceMethod genericMethod)
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
        return module.AssemblyReferences.Any(_ => _.Name.Equals(assembly));
    }

    protected static bool IsTypeFrom(MethodReference methodReference, string name)
    {
        return methodReference.DeclaringType.FullName.StartsWith(name);
    }
}