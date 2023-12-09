using System.Runtime.InteropServices;
using System.Windows.Media;

namespace UrbanUI.WPF.Win32.Interop.Values
{

   internal static class InteropValues
   {
      internal static class ExternDll
      {
         public const string
             User32 = "user32.dll",
             Kernel32 = "kernel32.dll",
             Shell32 = "shell32.dll",
             NTdll = "ntdll.dll";
      }

      public static double DPI_SCALE => SystemDPI.GetCurrentDPIScaleFactor();
   }
}
