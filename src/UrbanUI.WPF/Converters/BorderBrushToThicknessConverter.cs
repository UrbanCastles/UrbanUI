using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace UrbanUI.WPF.Converters
{
   public class BorderBrushToThicknessConverter : IMultiValueConverter
   {
      public enum Mode
      {
         Margin,
         BorderThickness
      }

      public Mode ConvertMode { get; set; }

      public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
      {
         var borderBrush = values[0] as Brush;
         var userBorderThickness = values.Length > 1 && values[1] is Thickness
             ? (Thickness)values[1]
             : new Thickness(1);

         bool hasVisibleBorder = !(borderBrush == null || borderBrush == Brushes.Transparent);

         switch (ConvertMode)
         {
            case Mode.Margin:
               return hasVisibleBorder ? new Thickness(1) : new Thickness(0);

            case Mode.BorderThickness:
               return hasVisibleBorder ? userBorderThickness : new Thickness(0);

            default:
               return new Thickness(0);
         }
      }

      public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
      {
         throw new NotImplementedException();
      }
   }
}
