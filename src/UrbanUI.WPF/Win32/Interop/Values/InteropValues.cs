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
             Dwmapi = "dwmapi.dll",
             Gdi32 = "gdi32.dll";
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

      internal const int SC_RESTORE = 0xF120;
      internal const int SC_MOVE = 0xF010;
      internal const int SC_SIZE = 0xF000;
      internal const int SC_MINIMIZE = 0xF020;
      internal const int SC_MAXIMIZE = 0xF030;
      internal const int SC_CLOSE = 0xF060;
      #endregion Window Menus

      #region Window Size Procs
      internal const int MONITOR_DEFAULTTONULL = 0x00000000;
      internal const int MONITOR_DEFAULTTOPRIMARY = 0x00000001;
      internal const int MONITOR_DEFAULTTONEAREST = 0x00000002;
      internal const int WM_GETMINMAXINFO = 0x0024;

      internal const int WM_EXITSIZEMOVE = 0x0232;
      internal const int WM_ENTERSIZEMOVE = 0x0231;
      internal const int WM_SIZE = 0x0005;
      internal const int WM_NCCALCSIZE = 0x0083;
      internal const int WM_WINDOWPOSCHANGED = 0x0047;

      internal const int SIZE_MAXHIDE = 4;
      internal const int SIZE_MAXIMIZED = 2;
      internal const int SIZE_MAXSHOW = 3;
      internal const int SIZE_MINIMIZED = 1;
      internal const int SIZE_RESTORED = 0;
      internal static double DPI_SCALE => SystemDPI.GetCurrentDPIScaleFactor();
      #endregion Window Size Procs

      #region Window UI
      internal const int WM_ERASEBKGND = 0x0014;
      internal const int WM_NCPAINT = 0x0085;

      internal const int RDW_INVALIDATE = 0x0001;
      internal const int RDW_INTERNALPAINT = 0x0002;
      internal const int RDW_ERASE = 0x0004;

      internal const int RDW_VALIDATE = 0x0008;
      internal const int RDW_NOINTERNALPAINT = 0x0010;
      internal const int RDW_NOERASE = 0x0020;

      internal const int RDW_NOCHILDREN = 0x0040;
      internal const int RDW_ALLCHILDREN = 0x0080;

      internal const int RDW_UPDATENOW = 0x0100;
      internal const int RDW_ERASENOW = 0x0200;

      internal const int RDW_FRAME = 0x0400;
      internal const int RDW_NOFRAME = 0x0800;


      internal const int GWL_STYLE = -16;
      internal const int GWL_EXSTYLE = -20;

      internal const int WS_CAPTION = 0x00C00000;
      internal const int WS_THICKFRAME = 0x00040000;
      internal const int WS_MINIMIZEBOX = 0x00020000;
      internal const int WS_MAXIMIZEBOX = 0x00010000;
      internal const int WS_SYSMENU = 0x00080000;
      internal const int WM_PAINT = 0x000F;

      internal const uint SWP_NOSIZE = 0x0001;
      internal const uint SWP_NOMOVE = 0x0002;
      internal const uint SWP_NOZORDER = 0x0004;
      internal const uint SWP_NOREDRAW = 0x0008;
      internal const uint SWP_NOACTIVATE = 0x0010;
      internal const uint SWP_FRAMECHANGED = 0x0020;
      internal const uint SWP_SHOWWINDOW = 0x0040;
      internal const uint SWP_HIDEWINDOW = 0x0080;
      internal const uint SWP_NOCOPYBITS = 0x0100;
      internal const uint SWP_NOOWNERZORDER = 0x0200;
      internal const uint SWP_NOSENDCHANGING = 0x0400;
      internal const uint SWP_DRAWFRAME = SWP_FRAMECHANGED;
      internal const uint SWP_DEFERERASE = 0x2000;
      internal const uint SWP_ASYNCWINDOWPOS = 0x4000;

      internal const int GCLP_HBRBACKGROUND = -10;
      internal const int PS_SOLID = 0;

      internal const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;   // Windows 10 1809+
      internal const int DWMWA_MICA_EFFECT = 1029;             // Windows 11
      internal const int DWMWA_SYSTEMBACKDROP_TYPE = 38;       // Windows 11 22H2+
      internal const int DWMWA_CAPTION_COLOR = 35;             // Windows 11
      internal const int DWMWA_TEXT_COLOR = 36;                // Windows 11
      internal const int DWMWA_VISIBLE_FRAME_BORDER_THICKNESS = 37;
      internal const int DWMWA_WINDOW_CORNER_PREFERENCE = 33;
      internal const int DWMWA_WINDOW_CORNER_ATTRIBUTE = 34;

      #endregion Window UI
   }
}
