using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssertMessage.Fody.Processors
{
    public class XunitProcessor : ProcessorBase
    {
        public override bool IsValidForModule(ModuleDefinition module)
        {
            return IsReferenced(module, "xunit");
        }

        protected override bool IsThisFramework(MethodReference methodReference)
        {
            return IsTypeFrom(methodReference, "Xunit.");
        }
    }
}
