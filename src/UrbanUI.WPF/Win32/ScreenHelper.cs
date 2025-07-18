using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Shell;
using UrbanUI.WPF.Win32.Helper;
using UrbanUI.WPF.Win32.Interop.Methods;
using UrbanUI.WPF.Win32.Interop.Values;
using static UrbanUI.WPF.Win32.Interop.Structs.InteropStructs;

namespace UrbanUI.WPF.Win32
{
   internal static class ScreenHelper
   {
      internal static Rect GetCurrentMonitorBounds(Window window)
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

      internal static Rect GetWorkingArea(Window window)
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

      internal static void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam, Window window)
      {
         MINMAXINFO mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));
         IntPtr monitor = InteropMethods.MonitorFromWindow(hwnd, InteropValues.MONITOR_DEFAULTTONEAREST);

         if (monitor != IntPtr.Zero)
         {
            MONITORINFO monitorInfo = new MONITORINFO();
            monitorInfo.cbSize = Marshal.SizeOf(typeof(MONITORINFO));
            if (InteropMethods.GetMonitorInfo(monitor, ref monitorInfo))
            {
               RECT rcWork = monitorInfo.rcWork;
               RECT rcMonitor = monitorInfo.rcMonitor;

               mmi.ptMaxPosition.X = rcWork.Left - rcMonitor.Left;
               mmi.ptMaxPosition.Y = rcWork.Top - rcMonitor.Top;
               mmi.ptMaxSize.X = Math.Abs(rcWork.Right - rcWork.Left);
               mmi.ptMaxSize.Y = Math.Abs(rcWork.Bottom - rcWork.Top);

               mmi.ptMaxTrackSize.X = mmi.ptMaxSize.X;
               mmi.ptMaxTrackSize.Y = mmi.ptMaxSize.Y;

               // Apply WPF MaxWidth/MaxHeight constraint
               var source = PresentationSource.FromVisual(window);
               double dpiX = 1.0, dpiY = 1.0;
               if (source?.CompositionTarget != null)
               {
                  dpiX = source.CompositionTarget.TransformToDevice.M11;
                  dpiY = source.CompositionTarget.TransformToDevice.M22;
               }

               if (!double.IsInfinity(window.MaxWidth))
               {
                  int maxWidth = (int)(window.MaxWidth * dpiX);
                  mmi.ptMaxSize.X = Math.Min(mmi.ptMaxSize.X, maxWidth);
                  mmi.ptMaxTrackSize.X = Math.Min(mmi.ptMaxTrackSize.X, maxWidth);
               }

               if (!double.IsInfinity(window.MaxHeight))
               {
                  int maxHeight = (int)(window.MaxHeight * dpiY);
                  mmi.ptMaxSize.Y = Math.Min(mmi.ptMaxSize.Y, maxHeight);
                  mmi.ptMaxTrackSize.Y = Math.Min(mmi.ptMaxTrackSize.Y, maxHeight);
               }

               // Handle min tracking sizes
               double minWidth = window.MinWidth > 0 ? window.MinWidth : mmi.ptReserved.X;
               double minHeight = window.MinHeight > 0 ? window.MinHeight : mmi.ptReserved.Y;


               mmi.ptMinTrackSize.X = (int)(minWidth * dpiX);
               mmi.ptMinTrackSize.Y = (int)(minHeight * dpiY);
            }
         }

         Marshal.StructureToPtr(mmi, lParam, true);
      }

      internal static unsafe int ExtendFrameIntoClientArea(IntPtr hWnd, Window window)
      {
         Thickness margins = WindowChrome.GetWindowChrome(window).GlassFrameThickness;
         var nativeMargins = new MARGINS { cxLeftWidth = (int)margins.Left, cyTopHeight = (int)margins.Top, cxRightWidth = (int)margins.Right, cyBottomHeight = (int)margins.Bottom };
         var result = InteropMethods.DwmExtendFrameIntoClientArea(hWnd, ref nativeMargins);
         return result;
      }


   }
}
