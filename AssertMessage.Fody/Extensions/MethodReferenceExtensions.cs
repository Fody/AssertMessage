﻿using Mono.Cecil;

static class MethodReferenceExtensions
{
    public static bool IsMatch(this MethodReference methodReference, params string[] paramTypes)
    {
        if (methodReference.Parameters.Count != paramTypes.Length)
        {
            return false;
        }

        for (var index = 0; index < methodReference.Parameters.Count; index++)
        {
            var parameterDefinition = methodReference.Parameters[index];
            var paramType = paramTypes[index];
            if (parameterDefinition.ParameterType.Name != paramType
                && !("T".Equals(paramType) && parameterDefinition.ParameterType.IsGenericParameter))
            {
                return false;
            }
        }

        return true;
    }
}