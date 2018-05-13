using System;
using System.Collections.Generic;
using System.IO;

public class SourceCodeProvider : ISourceCodeProvider
{
    Dictionary<string, string[]> filesCache = new Dictionary<string, string[]>();

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