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

      //public static Thickness GetDefaultMarginForDpi()
      //{
      //   int currentDPI = GetCurrentDPI();
      //   Thickness thickness = new Thickness(8, 8, 7, 8);
      //   if (currentDPI == 120)
      //   {
      //      thickness = new Thickness(7, 7, 4, 5);
      //   }
      //   else if (currentDPI == 144)
      //   {
      //      thickness = new Thickness(7, 7, 3, 1);
      //   }
      //   else if (currentDPI == 168)
      //   {
      //      thickness = new Thickness(6, 6, 2, 0);
      //   }
      //   else if (currentDPI == 192)
      //   {
      //      thickness = new Thickness(6, 6, 0, 0);
      //   }
      //   else if (currentDPI == 240)
      //   {
      //      thickness = new Thickness(6, 6, 0, 0);
      //   }
      //   return thickness;
      //}

      public static Thickness GetFromMinimizedMarginForDpi()
      {
         int currentDPI = GetCurrentDPI();
         Thickness thickness = new Thickness(7, 7, 5, 7);
         if (currentDPI == 120)
         {
            thickness = new Thickness(6, 6, 4, 6);
         }
         else if (currentDPI == 144)
         {
            thickness = new Thickness(7, 7, 4, 4);
         }
         else if (currentDPI == 168)
         {
            thickness = new Thickness(6, 6, 2, 2);
         }
         else if (currentDPI == 192)
         {
            thickness = new Thickness(6, 6, 2, 2);
         }
         else if (currentDPI == 240)
         {
            thickness = new Thickness(6, 6, 0, 0);
         }
         return thickness;
      }
   }
}
