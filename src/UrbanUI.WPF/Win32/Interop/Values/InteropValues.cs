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
             NTdll = "ntdll.dll",
             Dwmapi = "dwmapi.dll";
      }

      #region HT Buttons
      internal const int HTMAXBUTTON = 9;
      internal const int HTMINBUTTON = 8;
      internal const int HTCLOSE = 20;
      internal const int HTCLIENT = 1;
      #endregion HT Buttons

      #region Window Menus
      internal const int WM_NCHITTEST = 0x0084;
      internal const int WM_NCLBUTTONDOWN = 0x00A1;
      internal const int WM_NCLBUTTONUP = 0x00A2;
      #endregion Window Menus

      #region Window Size Procs
      internal const int MONITOR_DEFAULTTONULL = 0x00000000;
      internal const int MONITOR_DEFAULTTOPRIMARY = 0x00000001;
      internal const int MONITOR_DEFAULTTONEAREST = 0x00000002;
      internal const int WM_GETMINMAXINFO = 0x0024;
      internal static double DPI_SCALE => SystemDPI.GetCurrentDPIScaleFactor();
      #endregion Window Size Procs
   }
}
