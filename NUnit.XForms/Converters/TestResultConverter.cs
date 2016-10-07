using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NUnit.XForms.Converters
{
    /// <summary>
    /// Represents <see cref="TestResult"/> as <see cref="string"/>
    /// </summary>
    public class TestResultConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var result = (TestResult)value;
            switch (result)
            {
                case TestResult.Success:
                    return "Success";
                case TestResult.Fail:
                    return "Failed";
                case TestResult.Ignored:
                    return "Ignored";
                default:
                    throw new NotSupportedException(value.ToString());
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
