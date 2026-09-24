using Mono.Cecil;
using Mono.Cecil.Cil;

/// <summary>
/// For frameworks that attach a message to the end of a fluent call chain instead of using a message overload.
/// </summary>
public interface IChainProcessor
{
    bool IsValidForModule(ModuleDefinition module);

    /// <summary>
    /// Is the instruction at <paramref name="index"/> the end of a chain that has no message yet.
    /// </summary>
    bool IsChainEnd(MethodDefinition method, int index);

    /// <summary>
    /// The instructions to insert before the chain end. The chain result is on the stack, and must be left on the stack.
    /// </summary>
    IEnumerable<Instruction> GetMessageInstructions(ModuleDefinition module, Instruction chainEnd, string message);
}
