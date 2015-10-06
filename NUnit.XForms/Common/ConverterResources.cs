using NUnit.XForms.Converters;

namespace NUnit.XForms.Common
{
    /// <summary>
    /// Típuskonverterek
    /// </summary>
    public static class ConverterResources
    {
        public static BoolToTestResultConverter BoolToTestResultConverter = new BoolToTestResultConverter();
        public static BoolToTestResultColorConverter BoolToTestResultColorConverter = new BoolToTestResultColorConverter();
        public static BoolInverseConverter BoolInverseConverter = new BoolInverseConverter();
    }
}
