﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace PrekrasnySklep.Converters
{
    [ValueConversion(typeof(double), typeof(string))]
    internal class NumToPriceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Format("{0:N2} zł", value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
