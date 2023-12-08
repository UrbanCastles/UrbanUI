using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace UrbanUI.WPF.Themes
{
   internal static class Defaults
   {
      internal static Theme getDefaultThemeSetups()
      {
         var brushConverter = new BrushConverter();
         return new Theme
         {
            Key = "defaultdark",
            Name = "Default Dark",
            Details = "Default Dark",
            IsDarkTheme = true,
            ThemeBackground = (Brush)brushConverter.ConvertFromString("#19212e"),
            ThemeForeground = (Brush)brushConverter.ConvertFromString("#ffffff"),
            MenuBackground = (Brush)brushConverter.ConvertFromString("#19212e"),
            MenuForeground = (Brush)brushConverter.ConvertFromString("#e5dfe8"),
            MenuFocusBackground = (Brush)brushConverter.ConvertFromString("#352738"),
            MenuFocusForeground = (Brush)brushConverter.ConvertFromString("#1aa84e"),
            MenuHighlightBackground = (Brush)brushConverter.ConvertFromString("#352738"),
            MenuHighlightForeground = (Brush)brushConverter.ConvertFromString("#ffffff"),
            MenuFocusMarkerColor = (Brush)brushConverter.ConvertFromString("#1aa84e"),
            MenuFocusIconColor = (Brush)brushConverter.ConvertFromString("#1aa84e"),
            ItemBackground = (Brush)brushConverter.ConvertFromString("#2a3139"),
            ItemForeground = (Brush)brushConverter.ConvertFromString("#e5dfe8"),
            ItemFocusBackground = (Brush)brushConverter.ConvertFromString("#62686D"),
            ItemFocusForeground = (Brush)brushConverter.ConvertFromString("#1aa84e"),
            ItemHighlightBackground = (Brush)brushConverter.ConvertFromString("#62686D"),
            ItemHighlightForeground = (Brush)brushConverter.ConvertFromString("#e5dfe8"),
            DisabledBackground = (Brush)brushConverter.ConvertFromString("#2a3139"),
            DisabledForeground = (Brush)brushConverter.ConvertFromString("#8e8e8e")
         };
      }
   }
}
