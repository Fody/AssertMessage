using Mono.Cecil;

namespace AssertMessage.Fody.Processors
{
    public interface IProcessor
    {
        bool IsValidForModule(ModuleDefinition module);

        bool IsValidForMethod(MethodReference methodReference);

        MethodReference GetAssertionMethodWithMessage(MethodReference methodReference);
    }
}
