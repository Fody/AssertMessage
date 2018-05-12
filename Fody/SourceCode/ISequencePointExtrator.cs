using Mono.Cecil.Cil;

public interface ISequencePointExtrator
{
    string GetSourceCode(SequencePoint sequencePoint);
}