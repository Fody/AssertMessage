using AssertMessage.Fody.Processors;
using AssertMessage.Fody.SourceCode;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AssertMessage.Fody
{
    public class ModuleWeaver
    {
        private const string AssemblyName = "AssertionMessage";

        public ModuleDefinition ModuleDefinition { get; set; }

        private ISequencePointExtrator sequencePointExtrator;

        private IList<IProcessor> allProcessors;

        private IList<IProcessor> processors;

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

        public void Execute()
        {
            SelectActiveProcessors();

            AnalyzeTypes();

            RemoveReference();
        }

        private void SelectActiveProcessors()
        {
            processors = allProcessors.Where(x => x.IsValidForModule(ModuleDefinition)).ToList();
        }

        private void AnalyzeTypes()
        {
            if (processors.Count == 0)
            {
                return;
            }

            foreach (var method in ModuleDefinition.GetTypes().SelectMany(x => x.GetMethods()))
            {
                try
                {
                    AnalyzeMethod( method );
                }
                catch( WeavingException )
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new WeavingException("Excpetion occurs occurred  procesing type: " + method.DeclaringType.FullName + ". Report bug with code on https://github.com/Fody/AssertMessage Error: " + ex.Message + ex.StackTrace);
                }
            }
        }

        private void AnalyzeMethod(MethodDefinition method)
        {
            if (method.HasBody)
            {
                var instructions = method.Body.Instructions;

                var toInsert = GetInstructionsToInsert(instructions, method);

                foreach (var instruction in toInsert)
                {
                    instructions.Insert(instruction.Position, instruction.Instruction);
                }
            }
        }

        private IList<InstructionToInsert> GetInstructionsToInsert(ICollection<Instruction> instructions, MethodDefinition method)
        {
            var index = 0;
            var toAdd = new List<InstructionToInsert>();
            SequencePoint lastSequencePoint = null;

            foreach (var ins in instructions)
            {
                lastSequencePoint = method.DebugInformation.GetSequencePoint(ins) ?? lastSequencePoint;

                var methodReference = ins.Operand as MethodReference;
                if (IsValidInstruction(ins, methodReference))
                {
                    var processor = processors.FirstOrDefault(x => x.IsValidForMethod(methodReference));
                    if (processor != null)
                    {
                        var newMethod = processor.GetAssertionMethodWithMessage(methodReference);
                        if (newMethod != null)
                        {
                            var newInstruction = GenerateNewCode(index, lastSequencePoint, ins, newMethod);
                            toAdd.Add(newInstruction);
                            index++;
                            var lastType = newMethod.Parameters.Last().ParameterType;
                            if (lastType.FullName == "System.Object[]")
                            {
                                toAdd.Add(new InstructionToInsert(index, Instruction.Create(OpCodes.Ldnull)));
                                index++;
                            }
                        }
                    }
                }

                index++;
            }
            return toAdd;
        }

        private InstructionToInsert GenerateNewCode(int index, SequencePoint lastSequencePoint, Instruction ins, MethodReference newMethod)
        {
            var imported = ModuleDefinition.ImportReference(newMethod);
            ins.Operand = imported;
            var source = sequencePointExtrator.GetSourceCode(lastSequencePoint);
            var loadString = Instruction.Create(OpCodes.Ldstr, source);
            var newInstruction = new InstructionToInsert(index, loadString);
            return newInstruction;
        }

        private static bool IsValidInstruction(Instruction ins, MethodReference methodReference)
        {
            return (ins.OpCode == OpCodes.Callvirt || ins.OpCode == OpCodes.Call) && methodReference != null;
        }

        private void RemoveReference()
        {
            var referenceToRemove = ModuleDefinition.AssemblyReferences.FirstOrDefault(x => x.Name == AssemblyName);
            if (referenceToRemove != null)
            {
                ModuleDefinition.AssemblyReferences.Remove(referenceToRemove);
            }
        }
    }
}