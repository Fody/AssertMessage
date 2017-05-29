[![Chat on Gitter](https://img.shields.io/gitter/room/fody/fody.svg?style=flat)](https://gitter.im/Fody/Fody)
[![NuGet Status](http://img.shields.io/nuget/v/AssertMessage.Fody.svg?style=flat)](https://www.nuget.org/packages/AssertMessage.Fody/)

## This is an add-in for [Fody](https://github.com/Fody/Fody/) 

![Icon](https://raw.githubusercontent.com/Fody/AssertMessage/master/Icons/package_icon.png)

Adds 'message' parameter to Assertions. It is generated from source code.

Supported frameworks:
- Nunit
- Mstest
- Xunit

[Introduction to Fody](http://github.com/Fody/Fody/wiki/SampleUsage).


## The nuget package

https://nuget.org/packages/AssertMessage.Fody/

    PM> Install-Package AssertMessage.Fody


## Your Code

    public void CustomerTest()
    {
        var expectedCustomer = new Customer();
        var actualCustomer = new Customer();
        ...
        Assert.AreEqual(expectedCustomer.Money, actualCustomer.Money);
    }


## What gets compiled

    public void CustomerTest()
    {
        var expectedCustomer = new Customer();
        var actualCustomer = new Customer();
        ...
        Assert.AreEqual(expectedCustomer.Money, actualCustomer.Money, "Assert.AreEqual(expectedCustomer.Money, actualCustomer.Money);");
    }


## Pdb files

The Pbd files are required for this plugin. To make it work in Release, please enable Debug Info(pdbonly) in Advanced Build Settings Dialog Box. [More info](https://msdn.microsoft.com/en-us/library/s4wcexbc.aspx)


## Icon

Message by Prerak Patel from [The Noun Project](http://thenounproject.com)
