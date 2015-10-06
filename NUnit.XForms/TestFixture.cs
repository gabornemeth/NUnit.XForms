//
// TestFixture.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.hu)
//
//    Copyright (C) 2015, Gabor Nemeth
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace NUnit.XForms
{
    /// <summary>
    /// Test fixture containing test cases
    /// </summary>
    class TestFixture
    {
        public TypeInfo Type { get; private set; }
        /// <summary>
        /// Tesztesetek
        /// </summary>
        public List<Test> Tests { get; private set; }
        /// <summary>
        /// Ezen a példányon futtatjuk a teszteket
        /// </summary>
        internal object Instance { get; private set; }
        /// <summary>
        /// TestFixture attribútum
        /// </summary>
        public CustomAttributeData TestFixtureAttribute { get; private set; }

        private object _setUpFixture, _tearDownFixture;
        private MethodInfo _setUpMethod, _tearDownMethod;

        public TestFixture(TypeInfo testType, CustomAttributeData testFixtureAttribute)
        {
            Type = testType;
            Tests = new List<Test>();

            // Tesztesetek meghatározása
            foreach (var method in TestHelper.GetMethods(Type, attribute => attribute.AttributeType.FullName == TestAttributes.Test))
            {
                Tests.Add(new Test(this, method));
            }

            TestFixtureAttribute = testFixtureAttribute;
            Instance = TestHelper.CreateTestFixture(Type.AsType(), testFixtureAttribute);

            FindSetUpFixture(out _setUpFixture, out _setUpMethod);
            FindTearDownFixture(out _tearDownFixture, out _tearDownMethod);
        }

        private void FindSetUpFixture(out object setUpFixtureInstance, out MethodInfo setUpMethod)
        {
            setUpFixtureInstance = null;
            setUpMethod = null;

            CustomAttributeData setUpFixtureAttributeData;
            var setUpFixtureType = TestHelper.GetType(Type.Assembly,
                attribute => attribute.AttributeType.FullName == TestAttributes.SetUpFixture, out setUpFixtureAttributeData);
            if (setUpFixtureType == null)
                return; // no SetUpFixture found

            setUpFixtureInstance = TestHelper.CreateTestFixture(setUpFixtureType.AsType(), setUpFixtureAttributeData);
            setUpMethod = TestHelper.GetFirstMethod(setUpFixtureType,
                attribute => attribute.AttributeType.FullName == TestAttributes.SetUp);
        }

        private void FindTearDownFixture(out object tearDownFixtureInstance, out MethodInfo tearDownMethod)
        {
            tearDownFixtureInstance = null;
            tearDownMethod = null;

            CustomAttributeData tearDownFixtureAttributeData;
            var tearDownFixtureType = TestHelper.GetType(Type.Assembly,
                attribute => attribute.AttributeType.FullName == TestAttributes.TearDownFixture, out tearDownFixtureAttributeData);
            if (tearDownFixtureType == null)
                return; // no TearDownFixture found

            tearDownFixtureInstance = TestHelper.CreateTestFixture(tearDownFixtureType.AsType(), tearDownFixtureAttributeData);
            tearDownMethod = TestHelper.GetFirstMethod(tearDownFixtureType,
                attribute => attribute.AttributeType.FullName == TestAttributes.TearDown);
        }

        public void SetUp()
        {
            if (_setUpFixture != null)
            {
                TestHelper.Invoke(_setUpMethod, _setUpFixture);
            }
        }

        public void TearDown()
        {
            if (_tearDownFixture != null)
            {
                TestHelper.Invoke(_tearDownMethod, _tearDownFixture);
            }
        }

        public void Run()
        {
            //SetUp();
            // Run tests
            foreach (var test in Tests)
            {
                test.Run();
            }
            //TearDown();
        }
    }
}
