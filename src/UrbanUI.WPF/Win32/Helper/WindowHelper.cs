using System;
using System.Windows;
using System.Windows.Interop;

namespace UrbanUI.WPF.Win32.Helper
{
   internal static class WindowHelper
   {
      internal static IntPtr GetHandle(this Window window) => new WindowInteropHelper(window).Handle;
      internal static IntPtr EnsureHandle(this Window window) => new WindowInteropHelper(window).EnsureHandle();
      internal static HwndSource GetHwndSource(this Window window) => HwndSource.FromHwnd(window.EnsureHandle());
      internal static HwndSource GetHwndSource(this Window window, IntPtr Handle) => HwndSource.FromHwnd(Handle);

      internal enum ButtonType
      {
         NONE = -1,
         MINIMIZEBUTTON = 0,
         MAXIMIZEBUTTON = 1,
         RESTOREBUTTON = 2,
         CLOSEBUTTON = 3
      };
   }
}
