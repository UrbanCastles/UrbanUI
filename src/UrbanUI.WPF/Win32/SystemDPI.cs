using System.Reflection;
using System.Windows;

namespace UrbanUI.WPF.Win32
{
   internal static class SystemDPI
   {

      public static int GetCurrentDPI()
      {
         return (int)typeof(SystemParameters).GetProperty("Dpi", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null, null);
      }

      public static double GetCurrentDPIScaleFactor()
      {
         return (double)GetCurrentDPI() / 96;
      }

   }
}
