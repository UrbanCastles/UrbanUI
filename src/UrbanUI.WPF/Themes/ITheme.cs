

namespace UrbanUI.WPF.Themes
{
   public interface ITheme
   {
      void SetThemeUI(Theme theme, bool UpdateContentThemes = false);
      Theme GetTheme();
   }

   public static class ThemeChanger
   { 

      public static void ChangeThemeOfPages(Theme theme)
      {

      }
   }
}
