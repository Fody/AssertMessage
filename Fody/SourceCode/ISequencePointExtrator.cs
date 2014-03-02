using Mono.Cecil.Cil;

namespace AssertMessage.Fody.SourceCode
{
    public interface ISequencePointExtrator
    {
        string GetSourceCode(SequencePoint sequencePoint);
    }
}
