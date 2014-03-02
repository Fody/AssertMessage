using Mono.Cecil;

namespace AssertMessage.Fody.Processors
{
    public class MstestProcessor : ProcessorBase
    {
        public override bool IsValidForModule(ModuleDefinition module)
        {
            return IsReferenced(module, "Microsoft.VisualStudio.QualityTools.UnitTestFramework");
        }

        protected override bool IsThisFramework(MethodReference methodReference)
        {
            return IsTypeFrom(methodReference, "Microsoft.VisualStudio.TestTools.UnitTesting.");
        }
    }
}
