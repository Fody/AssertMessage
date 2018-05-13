using Mono.Cecil;

public class MstestProcessor : ProcessorBase
{
    public override bool IsValidForModule(ModuleDefinition module)
    {
        return IsReferenced(module, "Microsoft.VisualStudio.TestPlatform.TestFramework");
    }

    protected override bool IsThisFramework(MethodReference methodReference)
    {
        return IsTypeFrom(methodReference, "Microsoft.VisualStudio.TestTools.UnitTesting.");
    }
}