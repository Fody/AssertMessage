using Mono.Cecil;

static class TypeDefinitionExtensions
{
    public static MethodDefinition FindMethod(
        this TypeDefinition typeDefinition,
        string method,
        params string[] paramTypes)
    {
        return typeDefinition.Methods.FirstOrDefault(_ => _.Name == method &&
                                                          _.IsMatch(paramTypes));
    }
}