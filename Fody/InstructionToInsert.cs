using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal struct InstructionToInsert
{
    public InstructionToInsert(int position, Instruction instruction)
        : this()
    {
        this.Position = position;
        this.Instruction = instruction;
    }

    public int Position { get; private set; }

    public Instruction Instruction { get; private set; }
}

