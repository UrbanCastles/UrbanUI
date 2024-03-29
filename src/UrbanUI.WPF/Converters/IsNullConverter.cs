﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace UrbanUI.WPF.Converters
{
   public class IsNullConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         return value is null;
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         throw new NotImplementedException();
      }
   }
}
