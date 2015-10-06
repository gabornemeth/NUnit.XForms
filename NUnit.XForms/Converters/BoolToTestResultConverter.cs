﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NUnit.XForms.Converters
{
    /// <summary>
    /// <c>Boolean</c> értéket alakít szöveggé, amely egy teszt sikerességét vagy sikertelenségét jelzi
    /// </summary>
    public class BoolToTestResultConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return System.Convert.ToBoolean(value) ? "Success" : "Failed";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
