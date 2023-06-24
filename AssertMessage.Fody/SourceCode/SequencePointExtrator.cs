using Mono.Cecil.Cil;
using Fody;

public class SequencePointExtrator : ISequencePointExtrator
{
    ISourceCodeProvider sourceCodeProvider;

    public SequencePointExtrator(ISourceCodeProvider sourceCodeProvider)
    {
        this.sourceCodeProvider = sourceCodeProvider;
    }

    public string GetSourceCode(SequencePoint sequencePoint)
    {
        string[] lines;
        try
        {
            lines = sourceCodeProvider.GetSourceCode(sequencePoint.Document.Url);
        }
        catch (Exception)
        {
            throw new WeavingException("Can not find sourcecode. Ensure that generating PDB is enabled.");
        }

        if (lines == null)
        {
            return null;
        }

        if (sequencePoint.StartLine == sequencePoint.EndLine)
        {
            return lines[sequencePoint.StartLine - 1].Substring(sequencePoint.StartColumn - 1, sequencePoint.EndColumn - sequencePoint.StartColumn);
        }

        var list = new List<string>();

        var startLine = sequencePoint.StartLine - 1;
        var endLine = sequencePoint.EndLine - 1;

        list.Add(lines[startLine].Substring(sequencePoint.StartColumn - 1));
        for (var i = startLine + 1; i < endLine; i++)
        {
            list.Add(lines[i]);
        }

        var endColumn = sequencePoint.EndColumn - 1;
        var lastLine = lines[endLine];
        if (endColumn < lastLine.Length)
        {
            list.Add(lastLine.Remove(endColumn));
        }
        else
        {
            list.Add(lastLine);
        }

        return string.Join(" ", list.Select(_ => _.Trim()));
    }
}