using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssertMessage.Fody.Processors
{
    public class NunitProcessor : ProcessorBase
    {
        public override bool IsValidForModule(ModuleDefinition module)
        {
            return IsReferenced(module, "nunit.framework");
        }

        protected override bool IsThisFramework(MethodReference methodReference)
        {
            return IsTypeFrom(methodReference, "NUnit.Framework.");
        }
    }
}
