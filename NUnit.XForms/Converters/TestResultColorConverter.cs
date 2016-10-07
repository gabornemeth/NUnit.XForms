//
// TestResultColorConverter.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2016, Gabor Nemeth
//

using System;
using Xamarin.Forms;

namespace NUnit.XForms.Converters
{
    /// <summary>
    /// Represents <see cref="TestResult"/> as <see cref="Color"/>
    /// </summary>
    public class TestResultColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var result = (TestResult)value;
            switch (result)
            {
                case TestResult.Success:
                    return Color.FromHex("60e525");
                case TestResult.Fail:
                    return Color.Red;
                case TestResult.Ignored:
                    return Color.Yellow;
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
