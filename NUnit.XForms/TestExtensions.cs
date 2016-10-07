using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NUnit.XForms
{
    internal static class TestExtensions
    {
        public static bool HasAttribute(this MethodInfo method, string attributeName)
        {
            return method.CustomAttributes.FirstOrDefault(a => a.AttributeType.FullName == attributeName) != null;
        }

        public static CustomAttributeData GetAttribute(this MethodInfo method, string attributeName)
        {
            return method.CustomAttributes.FirstOrDefault(a => a.AttributeType.FullName == attributeName);
        }
    }
}
