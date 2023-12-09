using System;
using System.Runtime.InteropServices;
using UrbanUI.WPF.Win32.Interop.Values;

namespace UrbanUI.WPF.Win32.Interop.Methods
{
   internal static class InteropMethods
   {

      [DllImport(InteropValues.ExternDll.User32)]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool GetCursorPos(out Win32Point lpPoint);
   }
}
