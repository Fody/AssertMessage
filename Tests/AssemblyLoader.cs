using AssertMessage.Fody;
using Mono.Cecil;
using System.IO;
using System.Reflection;

internal class AssemblyLoader
{
    static AssemblyLoader()
    {
        var basePath = Path.GetDirectoryName(typeof(AssemblyLoader).Assembly.Location);
        var beforeAssemblyPath = Path.GetFullPath(basePath + @"\..\..\..\AssemblyToProcess\bin\Debug\AssemblyToProcess.dll");
#if (!DEBUG)

        beforeAssemblyPath = beforeAssemblyPath.Replace("Debug", "Release");
#endif
        var afterAssemblyPath = beforeAssemblyPath.Replace(".dll", "2.dll");
        var pbdAfterAssemblyPath = beforeAssemblyPath.Replace(".dll", "2.pdb");
        var pbdBeforeAssemblyPath = beforeAssemblyPath.Replace(".dll", ".pdb");
        File.Copy(beforeAssemblyPath, afterAssemblyPath, true);
        File.Copy(pbdBeforeAssemblyPath, pbdAfterAssemblyPath, true);
        var weavedAssemblyPath = beforeAssemblyPath.Replace(".dll", "3.dll");

        var assemblyResolver = new MockAssemblyResolver
        {
            Directory = Path.GetDirectoryName(beforeAssemblyPath)
        };
        var moduleDefinition = ModuleDefinition.ReadModule(afterAssemblyPath, new ReaderParameters
        {
            ReadSymbols = true,
            AssemblyResolver = assemblyResolver
        });
        var weavingTask = new ModuleWeaver
        {
            ModuleDefinition = moduleDefinition,
        };

        weavingTask.Execute();
        moduleDefinition.Write(weavedAssemblyPath);

        Assembly = Assembly.LoadFile(weavedAssemblyPath);
    }

    public static Assembly Assembly { get; }
}