using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssertMessage.Fody.Processors
{
    public interface IProcessor
    {
        bool IsValidForModule(ModuleDefinition module);

        bool IsValidForMethod(MethodReference methodReference);

        MethodReference GetAssertionMethodWithMessssage(MethodReference methodReference);
    }
}
