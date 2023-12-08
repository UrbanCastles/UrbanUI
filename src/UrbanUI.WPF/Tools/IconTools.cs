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
   public class IconTools
   {
      List<Icon> availableIcons = new List<Icon>();

      public IconTools() { }

      public IconTools(Uri urijson)
      {
         this.LoadIcons(urijson);
      }

      public Icon GetIcon(string iconname)
      {
         if (string.IsNullOrWhiteSpace(iconname) || availableIcons == null)
            return null;

         return availableIcons.Where(x => x.Name.ToLower() == iconname.ToLower()).FirstOrDefault();
      }


      #region Load Icons
      public void LoadIcons(Uri urijson)
      {
         var fileContent = string.Empty;
         StreamResourceInfo streamInfo = Application.GetContentStream(urijson);

         if (streamInfo != null && streamInfo.Stream != null)
         {
            using (StreamReader reader = new StreamReader(streamInfo.Stream))
            {
               fileContent = reader.ReadToEnd();
            }
         }

         if(!string.IsNullOrWhiteSpace(fileContent))
         {
            var result = JsonConvert.DeserializeObject<CustomIcons>(fileContent);
            availableIcons = result.icons;
         }
      }
      #endregion Load Icons
   }

}
