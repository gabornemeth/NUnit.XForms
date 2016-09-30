using System;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Diagnostics;

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

        [Test]
        public async Task Async()
        {
            var watch = new Stopwatch();
            watch.Start();
            await Task.Delay(1000);
            watch.Stop();
            Assert.True(watch.ElapsedMilliseconds > 900);
        }
    }
}
