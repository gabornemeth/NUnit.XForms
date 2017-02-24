using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.XForms;

namespace Sample
{
    class AndroidTestApp : MyTestApp
    {
        protected override void AddTests(TestRunner runner)
        {
            base.AddTests(runner);
            // Add assembly containing FsUnit tests
            //runner.Add(typeof(FsUnitTests.Test).Assembly);
        }
    }
}
