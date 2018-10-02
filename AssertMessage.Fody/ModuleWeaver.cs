using System;
using System.Collections.Generic;
using System.Linq;
using Fody;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;

public class ModuleWeaver : BaseModuleWeaver
{
    const string AssemblyName = "AssertionMessage";

    ISequencePointExtrator sequencePointExtrator;

    List<IProcessor> allProcessors;

    List<IProcessor> processors;

    public ModuleWeaver()
    {
        var sourceCodeProvider = new SourceCodeProvider();
        sequencePointExtrator = new SequencePointExtrator(sourceCodeProvider);
        processors = new List<IProcessor>();
        allProcessors = GetType()
            .Assembly
            .GetTypes()
            .Where(x => typeof(IProcessor).IsAssignableFrom(x) && !x.IsAbstract)
            .Select(x => (IProcessor)x.GetConstructor(new Type[0]).Invoke(new object[0]))
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
        processors = allProcessors.Where(x => x.IsValidForModule(ModuleDefinition)).ToList();
    }

    void AnalyzeTypes()
    {
        if (processors.Count == 0)
        {
            return;
        }

        foreach (var method in ModuleDefinition.GetTypes().SelectMany(x => x.GetMethods()))
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
        if (method.HasBody)
        {
            var instructions = method.Body.Instructions;

            var toInsert = GetInstructionsToInsert(instructions, method);

            if (toInsert.Any())
            {
                method.Body.SimplifyMacros();
                foreach (var instruction in toInsert)
                {
                    instructions.Insert(instruction.Position, instruction.Instruction);
                }
                method.Body.OptimizeMacros();

            }
        }
    }

    List<InstructionToInsert> GetInstructionsToInsert(ICollection<Instruction> instructions, MethodDefinition method)
    {
        var index = 0;
        var toAdd = new List<InstructionToInsert>();
        SequencePoint lastSequencePoint = null;

        var branchTargetFixups = new Dictionary<Instruction, Instruction>();

        foreach (var ins in instructions)
        {
            lastSequencePoint = method.DebugInformation.GetSequencePoint(ins) ?? lastSequencePoint;

            var methodReference = ins.Operand as MethodReference;
            if (IsValidInstruction(ins, methodReference))
            {
                var processor = processors.FirstOrDefault(x => x.IsValidForMethod(methodReference));
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
                        toAdd.Add(new InstructionToInsert(index, Instruction.Create(OpCodes.Ldnull)));
                        index++;
                    }
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

    InstructionToInsert GenerateNewCode(int index, SequencePoint lastSequencePoint, Instruction ins, MethodReference newMethod)
    {
        var imported = ModuleDefinition.ImportReference(newMethod);
        ins.Operand = imported;
        var source = sequencePointExtrator.GetSourceCode(lastSequencePoint);
        var loadString = Instruction.Create(OpCodes.Ldstr, source);
        return new InstructionToInsert(index, loadString);
    }

    static bool IsValidInstruction(Instruction ins, MethodReference methodReference)
    {
        return (ins.OpCode == OpCodes.Callvirt || ins.OpCode == OpCodes.Call) && methodReference != null;
    }

    public override bool ShouldCleanReference => true;
}