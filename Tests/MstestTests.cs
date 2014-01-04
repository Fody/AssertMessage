using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class MstestTests : IntegrationTestsBase<AssertFailedException>
    {
        [Test]
        public void StringContains_should_have_message()
        {
            CheckIfMessageIsValid("StringAssert.Contains(\"test\", actual);");
        }

        [Test]
        public void AreEqual_should_have_message_orginal_formated_message()
        {
            CheckIfMessageIsValid(message =>
            {
                NUnit.Framework.StringAssert.Contains("orginal_message", message);
                NUnit.Framework.StringAssert.DoesNotContain("Assert.AreEqual(", message);
            });       
        }

        [Test]
        public void AreEqual_should_have_message()
        {
            CheckIfMessageIsValid("Assert.AreEqual(expected, actual);");
        }

        [Test]
        public void AreEqualInt_should_have_message()
        {
            CheckIfMessageIsValid("Assert.AreEqual<int>(expected, actual);");
        }

        [Test]
        public void AreEqual_should_have_message_for_object()
        {
            CheckIfMessageIsValid("Assert.AreEqual(expected, actual);");
        }

        [Test]
        public void AreEqual_should_have_message_orginal_message()
        {
            CheckIfMessageIsValid(message =>
            {
                NUnit.Framework.StringAssert.Contains("orginal_message", message);
                NUnit.Framework.StringAssert.DoesNotContain("Assert.AreEqual(", message);
            });       
        }

        [Test]
        public void AreNotEqual_should_have_message()
        {
            CheckIfMessageIsValid("Assert.AreNotEqual(notExpected, actual);");
        }

        [Test]
        public void Contains_should_have_message()
        {
            CheckIfMessageIsValid("CollectionAssert.Contains(collection, expected);");
        }

        [Test]
        public void IsTrue_should_have_message()
        {
            CheckIfMessageIsValid("Assert.IsTrue(actual);");
        }

        [Test]
        public void IsFalse_should_have_message()
        {
            CheckIfMessageIsValid("Assert.IsFalse(actual);");
        }

        [Test]
        public void IsInstanceOf_should_have_message()
        {
            CheckIfMessageIsValid("Assert.IsInstanceOfType(actual, typeof(int));");
        }

        [Test]
        public void Fail_should_have_message()
        {
            CheckIfMessageIsValid("Assert.Fail();");
        }
    }
}
