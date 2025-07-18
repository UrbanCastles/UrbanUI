using System;
using System.Runtime.InteropServices;
using UrbanUI.WPF.Win32.Interop.Values;
using static UrbanUI.WPF.Win32.Interop.Structs.InteropStructs;

namespace UrbanUI.WPF.Win32.Interop.Methods
{
   internal static class InteropMethods
   {
      #region User32 APIs

      [DllImport(InteropValues.ExternDll.User32)]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool GetCursorPos(out POINT lpPoint);

      [DllImport(InteropValues.ExternDll.User32, SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);

      [DllImport(InteropValues.ExternDll.User32, CharSet = CharSet.Auto)]
      internal static extern IntPtr DefWindowProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

      [DllImport(InteropValues.ExternDll.User32)]
      internal static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

      [DllImport(InteropValues.ExternDll.User32, SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

      [DllImport(InteropValues.ExternDll.User32, SetLastError = true)]
      internal static extern IntPtr GetDC(IntPtr hWnd);

      [DllImport(InteropValues.ExternDll.User32, SetLastError = true)]
      internal static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

      [DllImport(InteropValues.ExternDll.User32, SetLastError = true)]
      public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

      [DllImport(InteropValues.ExternDll.User32, SetLastError = true)]
      internal static extern int FillRect(IntPtr hDC, [In] ref RECT lprc, IntPtr hbr);

      [DllImport(InteropValues.ExternDll.User32, SetLastError = true)]
      internal static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

      [DllImport(InteropValues.ExternDll.User32, EntryPoint = "SetClassLongPtr", SetLastError = true)]
      internal static extern IntPtr SetClassLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

      [DllImport(InteropValues.ExternDll.User32, SetLastError = true)]
      internal static extern IntPtr GetWindowDC(IntPtr hWnd);

      [DllImport(InteropValues.ExternDll.User32, SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, uint flags);

      [DllImport(InteropValues.ExternDll.User32, SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool SetWindowPos(
          IntPtr hWnd,
          IntPtr hWndInsertAfter,
          int X,
          int Y,
          int cx,
          int cy,
          uint uFlags);

      [DllImport(InteropValues.ExternDll.User32)]
      internal static extern IntPtr BeginPaint(IntPtr hWnd, out PAINTSTRUCT lpPaint);

      [DllImport(InteropValues.ExternDll.User32)]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool EndPaint(IntPtr hWnd, [In] ref PAINTSTRUCT lpPaint);

      #endregion

      #region Ntdll APIs

      [DllImport(InteropValues.ExternDll.NTdll)]
      internal static extern int RtlGetVersion(out RTL_OSVERSIONINFOEX lpVersionInformation);

      #endregion

      #region Dwmapi APIs

      [DllImport(InteropValues.ExternDll.Dwmapi, PreserveSig = true)]
      internal static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);

      [DllImport(InteropValues.ExternDll.Dwmapi, PreserveSig = true)]
      internal static extern int DwmIsCompositionEnabled([MarshalAs(UnmanagedType.Bool)] out bool pfEnabled);

      [DllImport(InteropValues.ExternDll.Dwmapi, PreserveSig = true)]
      internal static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref uint attrValue, int attrSize);

      [DllImport(InteropValues.ExternDll.Dwmapi, PreserveSig = true)]
      internal static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out DwmWindowCornerPreference pvAttribute, int cbAttribute);


      #endregion

      #region Gdi32 APIs

      [DllImport(InteropValues.ExternDll.Gdi32, SetLastError = true)]
      internal static extern IntPtr CreateSolidBrush(uint crColor);

      [DllImport(InteropValues.ExternDll.Gdi32, SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool DeleteObject(IntPtr hObject);

      #endregion

      #region Shell32 APIs

      [DllImport(InteropValues.ExternDll.Shell32, SetLastError = true)]
      public static extern uint SHAppBarMessage(uint dwMessage, ref APPBARDATA pData);

      #endregion
   }
}
