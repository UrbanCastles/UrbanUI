using System.Runtime.InteropServices;

namespace UrbanUI.WPF.Win32.Interop.Structs
{
   internal static class InteropStructs
   {
      [StructLayout(LayoutKind.Sequential)]
      public struct RECT
      {
         public int Left, Top, Right, Bottom;
      }

      [StructLayout(LayoutKind.Sequential)]
      public struct MONITORINFO
      {
         public int cbSize;
         public RECT rcMonitor;
         public RECT rcWork;
         public uint dwFlags;
      }
   }
}
