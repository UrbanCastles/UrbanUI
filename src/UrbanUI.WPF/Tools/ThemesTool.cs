using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Resources;
using UrbanUI.WPF.Themes;

namespace UrbanUI.WPF.Tools
{
   public class ThemesTool
   {
      private List<Theme> availableThemes = new List<Theme>();

      public ThemesTool() { }

      public ThemesTool(Uri uriSourceJson)
      {
         LoadThemes(uriSourceJson);
      }

      public Theme GetTheme(string themeKey)
      {
         if (string.IsNullOrWhiteSpace(themeKey) || availableThemes == null)
            return null;

         return availableThemes.Where(x => x.Key.ToLower() == themeKey.ToLower()).FirstOrDefault();
      }

      public List<Theme> GetThemes()
      {
         return availableThemes;
      }


      #region Load Themes
      public void LoadThemes(Uri UriSource)
      {

         var fileContent = string.Empty;
         StreamResourceInfo streamInfo = Application.GetContentStream(UriSource);

         if (streamInfo != null && streamInfo.Stream != null)
         {
            using (StreamReader reader = new StreamReader(streamInfo.Stream))
            {
               fileContent = reader.ReadToEnd();
            }
         }

         if (!string.IsNullOrWhiteSpace(fileContent))
         {
            var result = JsonConvert.DeserializeObject<ThemeList>(fileContent);
            availableThemes = result.themes;
         }

      }
      #endregion Loade Themes
   }


}
