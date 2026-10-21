using Mono.Cecil;

public class XunitProcessor : ProcessorBase
{
    public override bool IsValidForModule(ModuleDefinition module)
    {
        return IsReferenced(module, "xunit.assert") ||
               IsReferenced(module, "xunit.v3.assert");
    }

    protected override bool IsThisFramework(MethodReference methodReference)
    {
        return IsTypeFrom(methodReference, "Xunit.");
    }
}