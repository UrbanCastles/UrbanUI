

namespace UrbanUI.WPF.Themes
{
   public interface ITheme
   {
      public void SetThemeUI(Theme theme, bool UpdateContentThemes = false);
      public Theme GetTheme();
   }

   public static class ThemeChanger
   { 

      public static void ChangeThemeOfPages(Theme theme)
      {

      }
   }
}
