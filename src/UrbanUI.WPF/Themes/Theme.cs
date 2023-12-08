using System.Collections.Generic;
using System.Windows.Media;

namespace UrbanUI.WPF.Themes
{
   public class Theme
   {
      public string Key { get; set; }
      public string Name { get; set; }
      public string Details { get; set; }
      public bool IsDarkTheme { get; set; }
      public Brush ThemeBackground { get; set; }
      public Brush ThemeForeground { get; set; }
      public Brush MenuBackground { get; set; }
      public Brush MenuForeground { get; set; }
      public Brush MenuFocusBackground { get; set; }
      public Brush MenuFocusForeground { get; set; }
      public Brush MenuHighlightBackground { get; set; }
      public Brush MenuHighlightForeground { get; set; }
      public Brush MenuFocusMarkerColor { get; set; }
      public Brush MenuFocusIconColor { get; set; }
      public Brush ItemBackground { get; set; }
      public Brush ItemForeground { get; set; }
      public Brush ItemFocusBackground { get; set; }
      public Brush ItemFocusForeground { get; set; }
      public Brush ItemHighlightBackground { get; set; }
      public Brush ItemHighlightForeground { get; set; }
      public Brush DisabledBackground { get; set; }
      public Brush DisabledForeground { get; set; }
   }

   class ThemeList
   {
      public List<Theme> themes = new List<Theme>();
   }
}
