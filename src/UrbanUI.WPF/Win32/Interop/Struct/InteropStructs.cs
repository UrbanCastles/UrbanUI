using System;
using System.Runtime.InteropServices;

namespace UrbanUI.WPF.Win32.Interop.Structs
{
   internal static class InteropStructs
   {
      [StructLayout(LayoutKind.Sequential, Pack = 0)]
      public struct RECT
      {
         public int Left, Top, Right, Bottom;
      }

      [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
      public struct MONITORINFO
      {
         public int cbSize;
         public RECT rcMonitor;
         public RECT rcWork;
         public int dwFlags;
      }

      [StructLayout(LayoutKind.Sequential)]
      public struct POINT
      {
         public int X;
         public int Y;

         public POINT(int x, int y)
         {
            this.X = x;
            this.Y = y;
         }
      }

      [StructLayout(LayoutKind.Sequential)]
      public struct MINMAXINFO
      {
         public POINT ptReserved;
         public POINT ptMaxSize;
         public POINT ptMaxPosition;
         public POINT ptMinTrackSize;
         public POINT ptMaxTrackSize;
      };

      [StructLayout(LayoutKind.Sequential)]
      public struct MARGINS
      {
         public int cxLeftWidth;
         public int cxRightWidth;
         public int cyTopHeight;
         public int cyBottomHeight;
      }


      [StructLayout(LayoutKind.Sequential)]
      public struct APPBARDATA
      {
         public int cbSize;
         public IntPtr hWnd;
         public int uCallbackMessage;
         public int uEdge;
         public RECT rc;
         public bool lParam;
      }

      public enum ABMsg
      {
         ABM_NEW = 0,
         ABM_REMOVE = 1,
         ABM_QUERYPOS = 2,
         ABM_SETPOS = 3,
         ABM_GETSTATE = 4,
         ABM_GETTASKBARPOS = 5,
         ABM_ACTIVATE = 6,
         ABM_GETAUTOHIDEBAR = 7,
         ABM_SETAUTOHIDEBAR = 8,
         ABM_WINDOWPOSCHANGED = 9,
         ABM_SETSTATE = 10
      }

      public enum ABEdge
      {
         ABE_LEFT = 0,
         ABE_TOP = 1,
         ABE_RIGHT = 2,
         ABE_BOTTOM = 3
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
   }
}
