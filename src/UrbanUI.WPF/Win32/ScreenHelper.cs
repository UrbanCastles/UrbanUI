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
            }

            // Determine min width/height
            double minWidth = window.MinWidth > 0 ? window.MinWidth : 180;
            double minHeight = window.MinHeight > 0 ? window.MinHeight : 190;

            // Apply DPI scaling
            var source = PresentationSource.FromVisual(window);
            if (source?.CompositionTarget != null)
            {
               var m = source.CompositionTarget.TransformToDevice;
               mmi.ptMinTrackSize.X = (int)(minWidth * m.M11);
               mmi.ptMinTrackSize.Y = (int)(minHeight * m.M22);
            }
            else
            {
               mmi.ptMinTrackSize.X = (int)minWidth;
               mmi.ptMinTrackSize.Y = (int)minHeight;
            }
         }

         Marshal.StructureToPtr(mmi, lParam, false);
      }

      internal static void ExtendGlassFrame(IntPtr hwnd)
      {
         if (!InteropMethods.DwmIsCompositionEnabled()) return;

         // Add a 1px transparent margin to all sides
         var margins = new MARGINS { cxLeftWidth = 1, cxRightWidth = 1, cyTopHeight = 1, cyBottomHeight = 1 };
         InteropMethods.DwmExtendFrameIntoClientArea(hwnd, ref margins);
      }

   }
}
