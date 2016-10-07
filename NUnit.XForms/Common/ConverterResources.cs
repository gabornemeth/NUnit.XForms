using NUnit.XForms.Converters;

namespace NUnit.XForms.Common
{
    /// <summary>
    /// Type converters that can be used in XAML
    /// </summary>
    public static class ConverterResources
    {
        public static TestResultConverter TestResultConverter = new TestResultConverter();
        public static TestResultColorConverter TestResultColorConverter = new TestResultColorConverter();
        public static BoolInverseConverter BoolInverseConverter = new BoolInverseConverter();
    }
}
