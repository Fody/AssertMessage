using System.Collections.Generic;
using System.Linq;
using AssertMessage.Fody.Extensions;
using Mono.Cecil;

namespace AssertMessage.Fody.Extensions
{
    public static class TypeDefinitionExtensions
    {
        public static MethodDefinition FindMethod(
            this TypeDefinition typeDefinition, 
            string method,
            params string[] paramTypes)
        {
            return typeDefinition.Methods.FirstOrDefault(x => x.Name == method && x.IsMatch(paramTypes));
        }
    }
}