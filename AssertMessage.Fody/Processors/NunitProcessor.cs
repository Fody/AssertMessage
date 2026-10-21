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

    // NUnit 5 declares Assert.AreEqual and the other classic asserts as static extensions on Assert,
    // which compile to static methods on ClassicAssertExtensions
    protected override bool IsAssertMethod(MethodReference methodReference)
    {
        if (base.IsAssertMethod(methodReference))
        {
            return true;
        }

        var resolved = methodReference.Resolve();
        return resolved.IsStatic && resolved.DeclaringType.Name == "ClassicAssertExtensions";
    }
}