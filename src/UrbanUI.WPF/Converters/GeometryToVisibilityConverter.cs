using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace UrbanUI.WPF.Converters
{

   [ValueConversion(typeof(Geometry), typeof(Visibility))]
   public class GeometryToVisibilityConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         if (value == null)
         {
            return Visibility.Collapsed;
         }
         else
         {
            return Visibility.Visible;
         }
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         throw new NotImplementedException();
      }
   }
}
