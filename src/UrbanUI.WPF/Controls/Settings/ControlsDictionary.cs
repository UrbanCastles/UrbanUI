using System;
using System.Windows;

namespace UrbanUI.WPF.Controls
{
   [Localizability(LocalizationCategory.Ignore)]
   public class ControlsDictionary : ResourceDictionary
   {
      //private const string DictionaryUri = "pack://application:,,,/UrbanoUI/UrbanUI.xaml";
      private const string DictionaryUri = "pack://application:,,,/UrbanUI.WPF;component/Themes/Generic.xaml";

      public ControlsDictionary()
      {
         Source = new Uri(DictionaryUri, UriKind.Absolute);
      }
   }

}
