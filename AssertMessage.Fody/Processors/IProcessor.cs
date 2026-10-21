using Mono.Cecil;

public interface IProcessor
{
    bool IsValidForModule(ModuleDefinition module);

    bool IsValidForMethod(MethodReference methodReference);

    MethodReference GetAssertionMethodWithMessage(MethodReference methodReference);

    /// <summary>
    /// For frameworks (MSTest 4) whose asserts take an optional message, so the compiler always calls the overload
    /// with a message and passes the default "". Returns the index of that message parameter, or -1.
    /// </summary>
    int GetOptionalMessageIndex(MethodReference methodReference);
}