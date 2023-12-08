using System.Runtime.InteropServices;
using System.Windows.Media;

namespace UrbanUI.WPF.Win32.Interop.Values
{

   internal static class InteropValues
   {
      internal static class ExternDll
      {
         public const string
             User32 = "user32.dll",
             Kernel32 = "kernel32.dll",
             Shell32 = "shell32.dll",
             NTdll = "ntdll.dll";
      }

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

      public static double DPI_SCALE => SystemDPI.GetCurrentDPI();
   }
}
