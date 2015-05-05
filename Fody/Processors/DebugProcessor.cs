using Mono.Cecil;

namespace AssertMessage.Fody.Processors
{
  public class DebugProcessor : ProcessorBase
  {
    public override bool IsValidForModule(ModuleDefinition module)
    {
      return IsReferenced(module, "System");
    }

    protected override bool IsThisFramework(MethodReference methodReference)
    {
      return IsTypeFrom(methodReference, "System.Diagnostics.Debug");
    }

    protected override bool IsAssertMethod(MethodReference methodReference)
    {
      var resolved = methodReference.Resolve();
      var name = resolved.DeclaringType.Name;
      var methodName = resolved.Name;

      return (name.Equals("Debug") && methodName.Equals("Assert"));
    }
  }
}
