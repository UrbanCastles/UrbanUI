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
      internal static extern bool GetCursorPos(out Win32Point lpPoint);

      [DllImport(InteropValues.ExternDll.User32)]
      internal static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

      [DllImport(InteropValues.ExternDll.User32, SetLastError = true)]
      internal static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);

      [DllImport(InteropValues.ExternDll.NTdll)]
      internal static extern int RtlGetVersion(out RTL_OSVERSIONINFOEX lpVersionInformation);

      [DllImport(InteropValues.ExternDll.User32)]
      internal static extern IntPtr DefWindowProc(IntPtr hWnd, int uMsg, IntPtr wParam, IntPtr lParam);


      [StructLayout(LayoutKind.Sequential)]
      internal struct RTL_OSVERSIONINFOEX
      {
#if NET6_0_OR_GREATER
         public RTL_OSVERSIONINFOEX(uint dwMajorVersion, uint dwMinorVersion, uint dwBuildNumber, uint dwRevision, uint dwPlatformId, string szCSDVersion) : this()
         {
            this.dwMajorVersion = dwMajorVersion;
            this.dwMinorVersion = dwMinorVersion;
            this.dwBuildNumber = dwBuildNumber;
            this.dwRevision = dwRevision;
            this.dwPlatformId = dwPlatformId;
            this.szCSDVersion = szCSDVersion;
         }
         public readonly uint dwOSVersionInfoSize { get; init; } = (uint)Marshal.SizeOf<RTL_OSVERSIONINFOEX>();
#else
         public uint dwOSVersionInfoSize;
#endif
         public uint dwMajorVersion;
         public uint dwMinorVersion;
         public uint dwBuildNumber;
         public uint dwRevision;
         public uint dwPlatformId;
         [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
         public string szCSDVersion;
      }
   }
}
