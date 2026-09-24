using Mono.Cecil;
using Mono.Cecil.Cil;

public class TUnitProcessor : IChainProcessor
{
    const int hiddenLine = 0xFEEFEE;

    public bool IsValidForModule(ModuleDefinition module)
    {
        return module.AssemblyReferences.Any(_ => _.Name.Equals("TUnit.Assertions"));
    }

    public bool IsChainEnd(MethodDefinition method, int index)
    {
        var instructions = method.Body.Instructions;
        if (!IsAssertionGetAwaiter(instructions[index]))
        {
            return false;
        }

        // walk back to the start of the statement, and require that the chain starts at Assert.That and has no Because
        var startsWithThat = false;
        for (var i = index - 1; i >= 0; i--)
        {
            var instruction = instructions[i];
            if (instruction.Operand is MethodReference methodReference)
            {
                var declaringType = methodReference.DeclaringType.FullName;
                if (methodReference.Name == "Because" && declaringType.StartsWith("TUnit.Assertions."))
                {
                    return false;
                }

                if (methodReference.Name == "That" && declaringType == "TUnit.Assertions.Assert")
                {
                    startsWithThat = true;
                }
            }

            var sequencePoint = method.DebugInformation.GetSequencePoint(instruction);
            if (sequencePoint != null && sequencePoint.StartLine != hiddenLine)
            {
                break;
            }
        }

        return startsWithThat;
    }

    public IEnumerable<Instruction> GetMessageInstructions(ModuleDefinition module, Instruction chainEnd, string message)
    {
        var getAwaiter = (MethodReference) chainEnd.Operand;
        var declaringType = getAwaiter.DeclaringType;
        var because = declaringType.Resolve().Methods.First(_ => _.Name == "Because" && _.Parameters.Count == 1);
        // Because is declared as returning Assertion<TValue>, so the return type must use the open generic parameter
        var elementType = ((GenericInstanceType) declaringType).ElementType;
        var returnType = new GenericInstanceType(elementType);
        returnType.GenericArguments.Add(elementType.GenericParameters[0]);
        var reference = new MethodReference(because.Name, returnType, declaringType)
        {
            HasThis = true
        };
        reference.Parameters.Add(new(module.TypeSystem.String));
        yield return Instruction.Create(OpCodes.Ldstr, message);
        yield return Instruction.Create(OpCodes.Callvirt, reference);
    }

    static bool IsAssertionGetAwaiter(Instruction instruction)
    {
        return (instruction.OpCode == OpCodes.Callvirt || instruction.OpCode == OpCodes.Call)
               && instruction.Operand is MethodReference {Name: "GetAwaiter"} methodReference
               && methodReference.DeclaringType.Namespace == "TUnit.Assertions.Core"
               && methodReference.DeclaringType.Name == "Assertion`1";
    }
}
