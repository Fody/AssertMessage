using Mono.Cecil;

public interface IProcessor
{
    bool IsValidForModule(ModuleDefinition module);

    bool IsValidForMethod(MethodReference methodReference);

    MethodReference GetAssertionMethodWithMessage(MethodReference methodReference);
}