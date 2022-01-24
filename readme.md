# <img src="/package_icon.png" height="30px"> AssertMessage.Fody

[![Chat on Gitter](https://img.shields.io/gitter/room/fody/fody.svg)](https://gitter.im/Fody/Fody)
[![NuGet Status](https://img.shields.io/nuget/v/AssertMessage.Fody.svg)](https://www.nuget.org/packages/AssertMessage.Fody/)

Adds 'message' parameter to Assertions. It is generated from source code.


## This is an add-in for [Fody](https://github.com/Fody/Fody/)

**It is expected that all developers using Fody [become a Patron on OpenCollective](https://opencollective.com/fody/contribute/patron-3059). [See Licensing/Patron FAQ](https://github.com/Fody/Home/blob/master/pages/licensing-patron-faq.md) for more information.**


## Usage

See also [Fody usage](https://github.com/Fody/Home/blob/master/pages/usage.md).

Supported frameworks:

 * Nunit
 * Mstest
 * Xunit


### NuGet installation

Install the [AssertMessage.Fody NuGet package](https://nuget.org/packages/AssertMessage.Fody/) and update the [Fody NuGet package](https://nuget.org/packages/Fody/):

```powershell
PM> Install-Package Fody
PM> Install-Package AssertMessage.Fody
```

The `Install-Package Fody` is required since NuGet always defaults to the oldest, and most buggy, version of any dependency.


### Add to FodyWeavers.xml

Add `<AssertMessage/>` to [FodyWeavers.xml](https://github.com/Fody/Fody#add-fodyweaversxml)

```xml
<?xml version="1.0" encoding="utf-8" ?>
<Weavers>
  <AssertMessage/>
</Weavers>
```

### Your Code

```csharp
public void CustomerTest()
{
    var expectedCustomer = new Customer();
    var actualCustomer = new Customer();
    ...
    Assert.AreEqual(expectedCustomer.Money, actualCustomer.Money);
}
```


### What gets compiled

```csharp
public void CustomerTest()
{
    var expectedCustomer = new Customer();
    var actualCustomer = new Customer();
    ...
    Assert.AreEqual(expectedCustomer.Money, actualCustomer.Money, "Assert.AreEqual(expectedCustomer.Money, actualCustomer.Money);");
}
```


### Pdb files

The Pbd files are required for this plugin. To make it work in Release, enable Debug Info(pdbonly) in Advanced Build Settings Dialog Box. [More info](https://msdn.microsoft.com/en-us/library/s4wcexbc.aspx)


## Icon

Message by [Prerak Patel(https://thenounproject.com/prerakpatel/) from [The Noun Project](https://thenounproject.com)