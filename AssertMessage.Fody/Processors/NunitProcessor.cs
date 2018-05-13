using Mono.Cecil;

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