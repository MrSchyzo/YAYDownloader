﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace YTDownloaderWpf.Tasks.ValueConverters
{
    public class NotEqualComparer : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !Equals(value, parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
