public class SourceCodeProvider : ISourceCodeProvider
{
    Dictionary<string, string[]> filesCache = new();

    public string[] GetSourceCode(string path)
    {
        if (filesCache.TryGetValue(path, out var result))
        {
            return result;
        }

        try
        {
            result = File.ReadAllLines(path);
        }
        catch (Exception)
        {
            result = null;
        }

        filesCache[path] = result;
        return result;
    }
}