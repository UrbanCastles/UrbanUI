using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using UrbanUI.WPF.Win32.Helper;
using UrbanUI.WPF.Win32.Interop.Methods;
using UrbanUI.WPF.Win32.Interop.Values;
using static UrbanUI.WPF.Win32.Interop.Structs.InteropStructs;

namespace UrbanUI.WPF.Win32
{
   internal static class ScreenHelper
   {
      public static Rect GetCurrentMonitorBounds(Window window)
      {
         var hwnd = WindowHelper.GetHandle(window);
         IntPtr monitor = InteropMethods.MonitorFromWindow(hwnd, InteropValues.MONITOR_DEFAULTTONEAREST);

         MONITORINFO monitorInfo = new MONITORINFO();
         monitorInfo.cbSize = Marshal.SizeOf(typeof(MONITORINFO));

         if (InteropMethods.GetMonitorInfo(monitor, ref monitorInfo))
         {
            RECT r = monitorInfo.rcMonitor;
            return new Rect(r.Left, r.Top, r.Right - r.Left, r.Bottom - r.Top);
         }

         return new Rect(0, 0, SystemParameters.PrimaryScreenWidth, SystemParameters.PrimaryScreenHeight);
      }

      public static Rect GetWorkingArea(Window window)
      {
         var hwnd = WindowHelper.GetHandle(window);
         IntPtr monitor = InteropMethods.MonitorFromWindow(hwnd, InteropValues.MONITOR_DEFAULTTONEAREST);

         MONITORINFO info = new MONITORINFO();
         info.cbSize = Marshal.SizeOf(typeof(MONITORINFO));

         if (InteropMethods.GetMonitorInfo(monitor, ref info))
         {
            var r = info.rcWork;
            return new Rect(r.Left, r.Top, r.Right - r.Left, r.Bottom - r.Top);
         }

         return new Rect(0, 0, SystemParameters.PrimaryScreenWidth, SystemParameters.PrimaryScreenHeight);
      }


   }
}
