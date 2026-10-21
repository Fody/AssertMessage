using Fody;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;

public class ModuleWeaver :
    BaseModuleWeaver
{
    ISequencePointExtrator sequencePointExtrator;

    List<IProcessor> allProcessors;

    List<IProcessor> processors;

    List<IChainProcessor> allChainProcessors;

    List<IChainProcessor> chainProcessors;

    public ModuleWeaver()
    {
        var sourceCodeProvider = new SourceCodeProvider();
        sequencePointExtrator = new SequencePointExtrator(sourceCodeProvider);
        processors = new();
        chainProcessors = new();
        allProcessors = CreateInstances<IProcessor>();
        allChainProcessors = CreateInstances<IChainProcessor>();
    }

    List<T> CreateInstances<T>()
    {
        return GetType()
            .Assembly
            .GetTypes()
            .Where(_ => typeof(T).IsAssignableFrom(_) && !_.IsAbstract && !_.IsInterface)
            .Select(_ => (T) _.GetConstructor(Type.EmptyTypes).Invoke(Array.Empty<object>()))
            .ToList();
    }

    public override void Execute()
    {
        SelectActiveProcessors();

        AnalyzeTypes();
    }

    public override IEnumerable<string> GetAssembliesForScanning()
    {
        yield break;
    }

    void SelectActiveProcessors()
    {
        processors = allProcessors.Where(_ => _.IsValidForModule(ModuleDefinition)).ToList();
        chainProcessors = allChainProcessors.Where(_ => _.IsValidForModule(ModuleDefinition)).ToList();
    }

    void AnalyzeTypes()
    {
        if (processors.Count == 0 && chainProcessors.Count == 0)
        {
            return;
        }

        foreach (var method in ModuleDefinition.GetTypes().SelectMany(_ => _.GetMethods()))
        {
            try
            {
                AnalyzeMethod(method);
            }
            catch (WeavingException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new WeavingException($"Exception occurred processing type: {method.DeclaringType.FullName}. Report bug with code on https://github.com/Fody/AssertMessage Error: {ex.Message}{ex.StackTrace}");
            }
        }
    }

    void AnalyzeMethod(MethodDefinition method)
    {
        if (!method.HasBody)
        {
            return;
        }
        var instructions = method.Body.Instructions;

        var toInsert = GetInstructionsToInsert(instructions, method);

        if (!toInsert.Any())
        {
            return;
        }
        method.Body.SimplifyMacros();
        foreach (var instruction in toInsert)
        {
            instructions.Insert(instruction.Position, instruction.Instruction);
        }
        method.Body.OptimizeMacros();
    }

    List<InstructionToInsert> GetInstructionsToInsert(IList<Instruction> instructions, MethodDefinition method)
    {
        var index = 0;
        var toAdd = new List<InstructionToInsert>();
        SequencePoint lastSequencePoint = null;

        var branchTargetFixups = new Dictionary<Instruction, Instruction>();

        for (var originalIndex = 0; originalIndex < instructions.Count; originalIndex++)
        {
            var ins = instructions[originalIndex];
            lastSequencePoint = method.DebugInformation.GetSequencePoint(ins) ?? lastSequencePoint;

            var methodReference = ins.Operand as MethodReference;
            var chainProcessor = chainProcessors.FirstOrDefault(_ => _.IsChainEnd(method, originalIndex));
            if (chainProcessor != null)
            {
                var source = sequencePointExtrator.GetSourceCode(lastSequencePoint);
                var first = true;
                foreach (var newInstruction in chainProcessor.GetMessageInstructions(ModuleDefinition, ins, source))
                {
                    toAdd.Add(new(index, newInstruction));
                    if (first)
                    {
                        branchTargetFixups[ins] = newInstruction;
                        first = false;
                    }

                    index++;
                }
            }
            else if (IsValidInstruction(ins, methodReference))
            {
                var processor = processors.FirstOrDefault(_ => _.IsValidForMethod(methodReference));
                var newMethod = processor?.GetAssertionMethodWithMessage(methodReference);
                if (newMethod != null)
                {
                    var newInstruction = GenerateNewCode(index, lastSequencePoint, ins, newMethod);
                    toAdd.Add(newInstruction);
                    branchTargetFixups[ins] = newInstruction.Instruction;
                    index++;
                    var lastType = newMethod.Parameters.Last().ParameterType;
                    if (lastType.FullName == "System.Object[]")
                    {
                        toAdd.Add(new(index, Instruction.Create(OpCodes.Ldnull)));
                        index++;
                    }
                }
                else
                {
                    ReplaceDefaultMessage(instructions, originalIndex, methodReference, lastSequencePoint);
                }
            }

            index++;
        }

        foreach (var ins in instructions)
        {
            if (ins.Operand is Instruction oldTarget && branchTargetFixups.TryGetValue(oldTarget, out var newTarget))
            {
                ins.Operand = newTarget;
            }
            else if (ins.Operand is Instruction[] targets)
            {
                for (var i = 0; i < targets.Length; i++)
                {
                    if (branchTargetFixups.TryGetValue(targets[i], out newTarget))
                    {
                        targets[i] = newTarget;
                    }
                }
            }
        }

        foreach (var @try in method.Body.ExceptionHandlers)
        {
            if (branchTargetFixups.TryGetValue(@try.TryStart, out var newTryStart))
            {
                @try.TryStart = newTryStart;
            }
        }

        return toAdd;
    }

    /// <summary>
    /// The arguments after the message are [CallerArgumentExpression] strings, so the compiler loads each with ldstr
    /// right before the call. Only a message left at its default "" is replaced; one the author wrote is kept.
    /// </summary>
    void ReplaceDefaultMessage(IList<Instruction> instructions, int callIndex, MethodReference methodReference, SequencePoint lastSequencePoint)
    {
        var messageIndex = processors
            .Select(_ => _.GetOptionalMessageIndex(methodReference))
            .DefaultIfEmpty(-1)
            .Max();
        if (messageIndex < 0)
        {
            return;
        }

        var trailing = methodReference.Parameters.Count - 1 - messageIndex;
        var messageLoadIndex = callIndex - 1 - trailing;
        if (messageLoadIndex < 0)
        {
            return;
        }

        for (var i = messageLoadIndex; i < callIndex; i++)
        {
            if (instructions[i].OpCode != OpCodes.Ldstr)
            {
                return;
            }
        }

        var messageLoad = instructions[messageLoadIndex];
        if ((string) messageLoad.Operand != "")
        {
            return;
        }

        messageLoad.Operand = sequencePointExtrator.GetSourceCode(lastSequencePoint);
    }

    InstructionToInsert GenerateNewCode(int index, SequencePoint lastSequencePoint, Instruction ins, MethodReference newMethod)
    {
        var imported = ModuleDefinition.ImportReference(newMethod);
        ins.Operand = imported;
        var source = sequencePointExtrator.GetSourceCode(lastSequencePoint);
        var loadString = Instruction.Create(OpCodes.Ldstr, source);
        return new(index, loadString);
    }

    static bool IsValidInstruction(Instruction ins, MethodReference methodReference)
    {
        return (ins.OpCode == OpCodes.Callvirt || ins.OpCode == OpCodes.Call) && methodReference != null;
    }

    public override bool ShouldCleanReference => true;
}