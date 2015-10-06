//
// TestRunner.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.hu)
//
//    Copyright (C) 2015, Gabor Nemeth
//
// Test runner for mobile platforms

using System;
using System.Collections.Generic;
using System.Reflection;

namespace NUnit.XForms
{
    /// <summary>
    /// Test runner
    /// </summary>
    public class TestRunner
    {
        /// <summary>
        /// Test fixtures
        /// </summary>
        internal List<TestFixture> Fixtures { get; private set; }
        /// <summary>
        /// Results of test run
        /// </summary>
        public List<TestResult> Results { get; private set; }

        /// <summary>
        /// ctor.
        /// </summary>
        public TestRunner()
        {
            Fixtures = new List<TestFixture>();
            Results = new List<TestResult>(); // initialize results;
        }

        /// <summary>
        /// Add test
        /// </summary>
        /// <param name="type"><c>System.Type</c> which contains the test</param>
        public void Add(Type type)
        {
            if (!AddInternal(type))
                throw new ArgumentException(string.Format("{0} is not a test type.", type.Name));
        }

        private bool AddInternal(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            var attrs = TestHelper.GetTestFixtureAttributes(typeInfo);
            if (attrs == null)
                return false; // no TestFixture attribute
            foreach (var testFixtureAttribute in attrs)
            {
                Fixtures.Add(new TestFixture(typeInfo, testFixtureAttribute));
            }

            return true;
        }

        /// <summary>
        /// Add assembly containing tests. All tests discovered in the assembly will be added.
        /// </summary>
        /// <param name="asm"></param>
        public void Add(Assembly asm)
        {
            foreach (var type in asm.DefinedTypes)
            {
                AddInternal(type.AsType());
            }
        }

        /// <summary>
        /// Run tests
        /// </summary>
        public void Run()
        {
            Results.Clear(); // remove previous results
            foreach (var fixture in Fixtures)
            {
                fixture.Run();
            }
        }
    }
}
