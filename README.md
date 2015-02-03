## This is an add-in for [Fody](https://github.com/Fody/Fody/) 

![Icon](https://raw.githubusercontent.com/Fody/AssertMessage/master/Icons/package_icon.png)

Adds 'message' parameter to Assertions. It is generated from source code.

Supported frameworks:
- Nunit
- Mstest
- Xunit

[Introduction to Fody](http://github.com/Fody/Fody/wiki/SampleUsage).

## The nuget package  [![NuGet Status](http://img.shields.io/nuget/v/AssertMessage.Fody.svg?style=flat)](https://www.nuget.org/packages/AssertMessage.Fody/)

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

## Icon

Message by Prerak Patel from [The Noun Project](http://thenounproject.com)
