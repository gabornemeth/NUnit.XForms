//
// Test.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.hu)
//
//    Copyright (C) 2015, Gabor Nemeth
//

using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace NUnit.XForms
{
    /// <summary>
    /// Test case
    /// </summary>
    class Test
    {
        public MethodInfo Method { get; private set; }
        private readonly TestFixture _fixture;

        public TestResult Result { get; private set; }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="fixture"></param>
        /// <param name="testMethod"></param>
        public Test(TestFixture fixture, MethodInfo testMethod)
        {
            _fixture = fixture;
            Method = testMethod;
        }

        /// <summary>
        /// Teszteset végrehajtása
        /// </summary>
        public void Run()
        {
            var setUp = TestHelper.GetFirstMethod(_fixture.Type, attribute => attribute.AttributeType.FullName == TestAttributes.SetUp);
            var tearDown = TestHelper.GetFirstMethod(_fixture.Type, attribute => attribute.AttributeType.FullName == TestAttributes.TearDown);
            var obj = _fixture.Instance;

            Result = new TestResult { Name = Method.Name };
            // teszteset végrehajtása
            try
            {
                Debug.WriteLine(Method.Name + " started.");
                if (setUp != null)
                    setUp.Invoke(obj, null);
                Invoke(Method, obj);
                if (tearDown != null)
                    tearDown.Invoke(obj, null);

                Result.Success = true;
                Debug.WriteLine(Method.Name + " passed.");
            }
            catch (Exception ex)
            {
                Result.Success = false;
                Result.Details = TestHelper.GetExceptionDetails(ex);
                Debug.WriteLine(Method.Name + " failed.");
            }
        }

        private void Invoke(MethodInfo methodInfo, object obj)
        {
            if (methodInfo.ReturnParameter.ParameterType == typeof(void))
            {
                // sync test
                methodInfo.Invoke(obj, null);
            }
            else if (methodInfo.ReturnParameter.ParameterType == typeof(Task))
            {
                // async test - wait for the task to finish
                var task = methodInfo.Invoke(obj, null) as Task;
                if (task != null)
                    task.Wait();
            }
        }
    }
}
