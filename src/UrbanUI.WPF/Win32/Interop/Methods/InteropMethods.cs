using System;
using System.Runtime.InteropServices;
using UrbanUI.WPF.Win32.Interop.Values;

namespace UrbanUI.WPF.Win32.Interop.Methods
{
   internal static class InteropMethods
   {

      [DllImport(InteropValues.ExternDll.NTdll)]
      internal static extern int RtlGetVersion(out InteropValues.RTL_OSVERSIONINFOEX lpVersionInformation);


      [DllImport(InteropValues.ExternDll.User32, CharSet = CharSet.None, ExactSpelling = false)]
      internal static extern IntPtr PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);


      [DllImport(InteropValues.ExternDll.User32, CharSet = CharSet.None, ExactSpelling = false)]
      internal static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);


      [DllImport(InteropValues.ExternDll.User32, CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
      internal static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);


      [DllImport(InteropValues.ExternDll.User32, CharSet = CharSet.None, ExactSpelling = false)]
      internal static extern bool EnableMenuItem (IntPtr hMenu, uint uIDEnableItem, uint uEnable);


      [DllImport(InteropValues.ExternDll.User32, CharSet = CharSet.None, ExactSpelling = false)]
      internal static extern int TrackPopupMenuEx (IntPtr hmenu, uint fuFlags, int x, int y, IntPtr hwnd, IntPtr lptpm);

      [DllImport(InteropValues.ExternDll.User32)]
      internal static extern IntPtr DefWindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);


      [DllImport(InteropValues.ExternDll.User32)]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool GetCursorPos(out Win32Point lpPoint);
   }
}
