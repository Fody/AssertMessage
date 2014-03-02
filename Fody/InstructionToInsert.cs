using Mono.Cecil.Cil;

internal struct InstructionToInsert
{
    public InstructionToInsert(int position, Instruction instruction)
        : this()
    {
        Position = position;
        Instruction = instruction;
    }

    public int Position { get; private set; }

    public Instruction Instruction { get; private set; }
}

