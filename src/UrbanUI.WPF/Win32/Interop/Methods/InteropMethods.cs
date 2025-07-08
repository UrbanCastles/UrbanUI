using System;
using System.Runtime.InteropServices;
using UrbanUI.WPF.Win32.Interop.Values;
using static UrbanUI.WPF.Win32.Interop.Structs.InteropStructs;

namespace UrbanUI.WPF.Win32.Interop.Methods
{
   internal static class InteropMethods
   {

      [DllImport(InteropValues.ExternDll.User32)]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool GetCursorPos(out POINT lpPoint);

      [DllImport(InteropValues.ExternDll.User32)]
      internal static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

      [DllImport(InteropValues.ExternDll.User32, SetLastError = true)]
      internal static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);

      [DllImport(InteropValues.ExternDll.NTdll)]
      internal static extern int RtlGetVersion(out RTL_OSVERSIONINFOEX lpVersionInformation);

      [DllImport(InteropValues.ExternDll.User32)]
      internal static extern IntPtr DefWindowProc(IntPtr hWnd, int uMsg, IntPtr wParam, IntPtr lParam);

      [DllImport(InteropValues.ExternDll.Dwmapi, PreserveSig = true)]
      internal static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);

      [DllImport(InteropValues.ExternDll.Dwmapi)]
      internal static extern bool DwmIsCompositionEnabled();

      [DllImport(InteropValues.ExternDll.User32, SetLastError = true)]
      internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

      [DllImport(InteropValues.ExternDll.Shell32, SetLastError = true)]
      internal static extern int SHAppBarMessage(int dwMessage, ref APPBARDATA pData);

      [DllImport(InteropValues.ExternDll.User32)]
      internal static extern IntPtr MonitorFromWindow(IntPtr hwnd, int dwFlags);

      [DllImport(InteropValues.ExternDll.User32, SetLastError = true)]
      internal static extern IntPtr MonitorFromPoint(POINT pt, int dwFlags);




   }
}
