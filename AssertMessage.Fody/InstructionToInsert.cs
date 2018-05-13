using Mono.Cecil.Cil;

struct InstructionToInsert
{
    public InstructionToInsert(int position, Instruction instruction)
        : this()
    {
        Position = position;
        Instruction = instruction;
    }

    public int Position { get; }

    public Instruction Instruction { get; }
}