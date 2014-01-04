
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssemblyToProcess
{
    public class NunitTests
    {
        public void StringContains_should_have_message()
        {
            string actual = "badstring";

            StringAssert.Contains("test", actual);
        }

        public void AreEqual_should_have_message_orginal_formated_message()
        {
            var expected = 1;
            var actual = 2;

            Assert.AreEqual(expected, actual, "{0}_{1}", "orginal", "message");
        }

        public void AreEqual_should_have_message_for_int()
        {
            var expected = 1;
            var actual = 2;

            Assert.AreEqual(expected, actual);
        }

        public void AreEqual_should_have_message_for_object()
        {
            var expected = new object();
            var actual = new object();
            
            Assert.AreEqual(expected, actual);
        }

        public void AreEqual_should_have_message_orginal_message()
        {
            var expected = 1;
            var actual = 2;

            Assert.AreEqual(expected, actual, "orginal_message");
        }
        
        public void AreNotEqual_should_have_message()
        {
            var expected = 1;
            var actual = 1;

            Assert.AreNotEqual(expected, actual);
        }

        public void Contains_should_have_message()
        {
            var expected = new object();
            var collection = new object[] { new Object(), new Object() };

            Assert.Contains(expected, collection);
        }

        public void IsTrue_should_have_message()
        {
            var actual = false;

            Assert.IsTrue(actual);
        }
        
        public void False_should_have_message()
        {
            var actual = true;

            Assert.False(actual);
        }

        public void IsEmpty_should_have_message_for_collection()
        {
            var actual = new object[]{new object()};

            Assert.IsEmpty(actual);
        }

        public void IsInstanceOf_should_have_message()
        {
            var actual = new Object();

            Assert.IsInstanceOf<int>(actual);
        }

        public void Throws_should_have_message()
        {
            var action = new TestDelegate(() => { });

            var ex = Assert.Throws<Exception>(action);
            Assert.IsNotNull(ex);
        }

        public void Fail_should_have_message()
        {
            Assert.Fail();
        }
    }
}
