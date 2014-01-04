using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssertMessage.Fody.SourceCode
{
    public class SourceCodeProvider : ISourceCodeProvider
    {
        private IDictionary<string, string[]> filesCache = new Dictionary<string, string[]>();

        public string[] GetSourceCode(string path)
        {
            string[] result = null;
            if(filesCache.TryGetValue(path, out result))
            {
                return result;
            }

            try
            {
                result = File.ReadAllLines(path);
            }
            catch(Exception)
            {
                result = null;
            }

            filesCache[path] = result;
            return result;
        }
    }
}
