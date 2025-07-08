using System;
using System.Runtime.InteropServices;
using UrbanUI.WPF.Win32.Interop.Methods;
using UrbanUI.WPF.Win32.Interop.Structs;
using UrbanUI.WPF.Win32.Interop.Values;

namespace UrbanUI.WPF.Win32.Helper
{
   internal static class OSVersionHelper
   {

      internal static readonly Version OSVersion = GetOSVersion();
      public static bool IsWindowsNT { get; } = Environment.OSVersion.Platform == PlatformID.Win32NT;
      public static bool IsWindows11_OrGreater { get; } = IsWindowsNT && OSVersion >= new Version(10, 0, 22000);
      public static Version GetOSVersion()
      {
         var osv = new InteropStructs.RTL_OSVERSIONINFOEX();
#if !NET5_0_OR_GREATER
         osv.dwOSVersionInfoSize = (uint)Marshal.SizeOf(osv);
#endif
         InteropMethods.RtlGetVersion(out osv);
         return new Version((int)osv.dwMajorVersion, (int)osv.dwMinorVersion, (int)osv.dwBuildNumber, (int)osv.dwRevision);
      }
   }
}
