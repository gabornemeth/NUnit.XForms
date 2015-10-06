using System;
using NUnit.Framework;

namespace Sample
{
    [TestFixture]
    public class MyTest
    {
        [Test]
        public void Success()
        {
            Assert.True(true);
        }

        [Test]
        public void Fail()
        {
            Assert.True(false);
        }

        [Test]
        public void ThrowException()
        {
            throw new Exception("Test throwing exception.");
        }
    }
}
