//
// TestHelper.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.hu)
//
//    Copyright (C) 2017, Gabor Nemeth
//

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;

namespace NUnit.XForms
{
    /// <summary>
    /// Helper class for discovering tests
    /// </summary>
    internal static class TestHelper
    {
        public static CustomAttributeData GetFirstTestFixtureAttribute(TypeInfo type)
        {
            var attributes = GetTestFixtureAttributes(type);
            if (attributes != null)
            {
                var attributesArray = attributes.ToArray();
                if (attributesArray.Length > 0)
                    return attributesArray[0];
            }

            return null;
        }

        public static IEnumerable<CustomAttributeData> GetTestFixtureAttributes(TypeInfo typeInfo)
        {
            foreach (var attribute in typeInfo.CustomAttributes)
            {
                if (attribute.AttributeType.FullName == TestAttributes.TestFixture)
                    yield return attribute; // TestFixture attribute found
            }
        }

        /// <summary>
        /// Checks whether a type contains any tests.
        /// </summary>
        /// <param name="typeInfo"></param>
        /// <returns></returns>
        public static bool HasTests(TypeInfo typeInfo)
        {
            if (GetTestFixtureAttributes(typeInfo).Count() > 0)
                return true;

            var methods = typeInfo.DeclaredMethods;
            var testMethods = GetMethods(typeInfo, attribute => attribute.AttributeType.FullName == TestAttributes.Test);
            if (testMethods.Count() > 0)
                return true;

            return false;
        }


        /// <summary>
        /// Finds the first method with a given attribute
        /// </summary>
        /// <param name="type">Methods of this type is where searching</param>
        /// <param name="match">Predicate to determine if the found attribute is the searched one</param>
        /// <returns></returns>
        public static MethodInfo GetFirstMethod(TypeInfo type, Func<CustomAttributeData, bool> match)
        {
            var methods = GetMethods(type, match);
            if (methods != null)
            {
                var methodsArray = methods.ToArray();
                if (methodsArray.Length > 0)
                    return methodsArray[0];
            }

            return null;
        }

        public static IEnumerable<MethodInfo> GetMethods(TypeInfo typeInfo, Func<CustomAttributeData, bool> match)
        {
            foreach (var method in typeInfo.DeclaredMethods)
            {
                foreach (var attribute in method.CustomAttributes)
                {
                    if (match(attribute))
                    {
                        yield return method;
                    }
                }
            }
        }

        public static TypeInfo GetType(Assembly asm, Func<CustomAttributeData, bool> match, out CustomAttributeData matchingAttributeData)
        {
            matchingAttributeData = null;
            foreach (var type in asm.DefinedTypes)
            {
                foreach (var attribute in type.CustomAttributes)
                {
                    if (match(attribute))
                    {
                        matchingAttributeData = attribute;
                        return type;
                    }
                }
            }

            return null;
        }

        public static object CreateTestFixture(Type testType, CustomAttributeData testFixtureAttribute)
        {
            var testTypeInfo = testType.GetTypeInfo();
            if (testTypeInfo.IsAbstract)
                return null; // cannot instantiate abstract class

            // check for constructor with parameters
            if (testFixtureAttribute != null &&
                testFixtureAttribute.ConstructorArguments != null &&
                testFixtureAttribute.ConstructorArguments.Count > 0)
            {
                var constructorArgs = (testFixtureAttribute.ConstructorArguments[0].Value as IEnumerable<CustomAttributeTypedArgument>).ToArray();
                var args = new object[constructorArgs.Length];
                for (var i = 0; i < constructorArgs.Length; i++)
                {
                    args[i] = constructorArgs[i].Value;
                }
                return Activator.CreateInstance(testType, args);
            }
                
            return Activator.CreateInstance(testType); // parameterless constructor
        }

        public static void Invoke(MethodInfo methodInfo, object obj)
        {
            if (methodInfo.ReturnParameter.ParameterType == typeof(void))
            {
                methodInfo.Invoke(obj, null);
            }
            else if (methodInfo.ReturnParameter.ParameterType == typeof(Task))
            {
                var task = methodInfo.Invoke(obj, null) as Task;
                if (task != null)
                    task.Wait();
            }
        }

        public static string GetExceptionDetails(Exception ex)
        {
            if (ex.InnerException != null)
                return ex.InnerException.Message;
            else
                return ex.Message;
        }
    }
}
