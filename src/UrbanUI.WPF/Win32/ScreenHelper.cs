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

               // Adjust for auto-hide taskbar (optional enhancement)
               mmi = AdjustWorkingAreaForAutoHide(monitor, mmi);
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



      private static MINMAXINFO AdjustWorkingAreaForAutoHide(IntPtr monitorContainingApplication, MINMAXINFO mmi)
      {
         IntPtr hwnd = InteropMethods.FindWindow("Shell_TrayWnd", null);
         if (hwnd == null) return mmi;
         IntPtr monitorWithTaskbarOnIt = InteropMethods.MonitorFromWindow(hwnd, InteropValues.MONITOR_DEFAULTTONEAREST);
         if (!monitorContainingApplication.Equals(monitorWithTaskbarOnIt)) return mmi;
         APPBARDATA abd = new APPBARDATA();
         abd.cbSize = Marshal.SizeOf(abd);
         abd.hWnd = hwnd;
         InteropMethods.SHAppBarMessage((int)ABMsg.ABM_GETTASKBARPOS, ref abd);
         int uEdge = GetEdge(abd.rc);
         bool autoHide = System.Convert.ToBoolean(InteropMethods.SHAppBarMessage((int)ABMsg.ABM_GETSTATE, ref abd));

         if (!autoHide) return mmi;

         switch (uEdge)
         {
            case (int)ABEdge.ABE_LEFT:
               mmi.ptMaxPosition.X += 2;
               mmi.ptMaxTrackSize.X -= 2;
               mmi.ptMaxSize.X -= 2;
               break;
            case (int)ABEdge.ABE_RIGHT:
               mmi.ptMaxSize.X -= 2;
               mmi.ptMaxTrackSize.X -= 2;
               break;
            case (int)ABEdge.ABE_TOP:
               mmi.ptMaxPosition.Y += 2;
               mmi.ptMaxTrackSize.Y -= 2;
               mmi.ptMaxSize.Y -= 2;
               break;
            case (int)ABEdge.ABE_BOTTOM:
               mmi.ptMaxSize.Y -= 2;
               mmi.ptMaxTrackSize.Y -= 2;
               break;
            default:
               return mmi;
         }
         return mmi;
      }

      private static int GetEdge(RECT rc)
      {
         int uEdge = -1;
         if (rc.Top == rc.Left && rc.Bottom > rc.Right)
            uEdge = (int)ABEdge.ABE_LEFT;
         else if (rc.Top == rc.Left && rc.Bottom < rc.Right)
            uEdge = (int)ABEdge.ABE_TOP;
         else if (rc.Top > rc.Left)
            uEdge = (int)ABEdge.ABE_BOTTOM;
         else
            uEdge = (int)ABEdge.ABE_RIGHT;
         return uEdge;
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
