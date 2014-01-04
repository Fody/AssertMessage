using AssertMessage.Fody;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    internal class AssemblyLoader
    {
        static AssemblyLoader()
        {
            var beforeAssemblyPath = Path.GetFullPath(@"..\..\..\AssemblyToProcess\bin\Debug\AssemblyToProcess.dll");
#if (!DEBUG)

        beforeAssemblyPath = beforeAssemblyPath.Replace("Debug", "Release");
#endif
            var afterAssemblyPath = beforeAssemblyPath.Replace(".dll", "2.dll");
            var pbdAfterAssemblyPath = beforeAssemblyPath.Replace(".dll", "2.pdb");
            var pbdBeforeAssemblyPath = beforeAssemblyPath.Replace(".dll", ".pdb");
            File.Copy(beforeAssemblyPath, afterAssemblyPath, true);
            File.Copy(pbdBeforeAssemblyPath, pbdAfterAssemblyPath, true);

            var moduleDefinition = ModuleDefinition.ReadModule(afterAssemblyPath, new ReaderParameters
            {
                ReadSymbols = true,
            });
            var weavingTask = new ModuleWeaver
            {
                ModuleDefinition = moduleDefinition,
            };

            weavingTask.Execute();
            moduleDefinition.Write(afterAssemblyPath);

            Assembly = Assembly.LoadFile(afterAssemblyPath);
        }

        public static Assembly Assembly { get; private set; }
    }
}
