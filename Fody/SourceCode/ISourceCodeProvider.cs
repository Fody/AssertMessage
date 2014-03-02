namespace AssertMessage.Fody.SourceCode
{
    public interface ISourceCodeProvider
    {
        string[] GetSourceCode(string path);
    }
}
